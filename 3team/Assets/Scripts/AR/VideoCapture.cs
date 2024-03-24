using System;
using System.Collections;
using System.IO;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.ReplayKit;

public class VideoCapture : MonoBehaviour
{
    public Camera arCamera;
    public Camera objectCamera;
    private Button videoButton;
    private Image image;
    private bool isRecording = false;

    [SerializeField]
    private Text m_statusText = null;

    void Start()
    {
        videoButton = GetComponent<Button>();
        image = GetComponent<Image>();
        Init();
    }

    void Init()
    {
        videoButton.OnClickAsObservable().Subscribe(_ => ButtonCheck());
    }


    void ButtonCheck()
    {
        isRecording = !isRecording; // 녹화 상태 토글

        if (isRecording)
        {
            StartCoroutine(StartRecording());
            //recordingIndicator.gameObject.SetActive(true); // 녹화 표시 이미지 활성화
        }
        else
        {
            StartCoroutine(StopRecording());
            //recordingIndicator.gameObject.SetActive(false); // 녹화 표시 이미지 비활성화
        }
    }
    // 녹화 시작
    private IEnumerator StartRecording()
    {
        isRecording = true;
        yield return new WaitForEndOfFrame();
        ReplayKitManager.PrepareRecording();
        ReplayKitManager.StartRecording();

        objectCamera.gameObject.SetActive(true);
        image.sprite = Manager.Resources.LoadSprite("VideoStop");
    }

    // 녹화 중지
    private IEnumerator StopRecording()
    {
        ReplayKitManager.StopRecording((filePath, error) => {
            Debug.Log("File path available : " + ReplayKitManager.GetPreviewFilePath());
        });
        objectCamera.gameObject.SetActive(true);
        image.sprite = Manager.Resources.LoadSprite("VideoStart");

        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".mp4";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        yield return new WaitUntil(() => File.Exists(filePath));

        // 저장된 동영상을 갤러리에 저장
        if (!string.IsNullOrEmpty(fileName))
        {
            NativeGallery.Permission permission = NativeGallery.SaveVideoToGallery(filePath, "Videos", fileName, (success, path) =>
            {
                if (success)
                {
                    Debug.Log("Video saved to gallery: " + path);
                }
                else
                {
                    Debug.Log("Failed to save video to gallery");
                }
            });
        }
        isRecording = false;
    }

    void LateUpdate()
    {
        // 게임 카메라의 렌더링을 AR 카메라에 합성
        RenderTexture.active = arCamera.targetTexture;
        objectCamera.Render();
        RenderTexture.active = null;
    }


}
