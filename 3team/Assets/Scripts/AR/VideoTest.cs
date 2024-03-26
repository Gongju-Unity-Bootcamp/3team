using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Android;

public class CameraCapture : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    Button videoButton;
    bool isVideo;


    void Start()
    {
        // ī�޶� �����Ͽ� WebCamTexture �ʱ�ȭ
        webcamTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = webcamTexture;
        webcamTexture.Play();
        Init();
    }

    void Init()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }
        }

        videoButton = GetComponent<Button>();
        videoButton.OnClickAsObservable().Subscribe(_ => ClickCheck());
    }

    void ClickCheck()
    {
        isVideo = !isVideo;
        if (!isVideo)
        {
            StartVideo();
        }
        else
        {
            StopVideo();
        }

    }


    void StartVideo()
    {
        // ���� ������ ĸó
        Texture2D texture = new Texture2D(webcamTexture.width, webcamTexture.height);
        texture.SetPixels(webcamTexture.GetPixels());
        texture.Apply();

        // ĸó�� �������� ������ ���Ϸ� ����
        byte[] bytes = texture.EncodeToJPG();
        string filePath = Application.persistentDataPath + "/capturedVideo.jpg";
        System.IO.File.WriteAllBytes(filePath, bytes);
    }

    private void StopVideo()
    {
        webcamTexture.Stop();
    }
}
