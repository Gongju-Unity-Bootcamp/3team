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
        isRecording = !isRecording; // ��ȭ ���� ���

        if (isRecording)
        {
            StartCoroutine(StartRecording());
            //recordingIndicator.gameObject.SetActive(true); // ��ȭ ǥ�� �̹��� Ȱ��ȭ
        }
        else
        {
            StartCoroutine(StopRecording());
            //recordingIndicator.gameObject.SetActive(false); // ��ȭ ǥ�� �̹��� ��Ȱ��ȭ
        }
    }
    // ��ȭ ����
    private IEnumerator StartRecording()
    {
        isRecording = true;
        yield return new WaitForEndOfFrame();
        ReplayKitManager.PrepareRecording();
        ReplayKitManager.StartRecording();

        objectCamera.gameObject.SetActive(true);
        image.sprite = Manager.Resources.LoadSprite("VideoStop");
    }

    // ��ȭ ����
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

        // ����� �������� �������� ����
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
        // ���� ī�޶��� �������� AR ī�޶� �ռ�
        RenderTexture.active = arCamera.targetTexture;
        objectCamera.Render();
        RenderTexture.active = null;
    }


}
