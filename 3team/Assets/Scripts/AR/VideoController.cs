using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Android;
using System.Collections;
using VoxelBusters.ReplayKit.Internal;

public class VideoController : MonoBehaviour, INativeCallbackListener
{
    public Camera arCamera;
    public Camera objectCamera;
    private Button videoButton;
    private Image image;
    private bool isRecording = false;
    public ReplayKitDefaultPlatform recorder;
    //public GameObject videoCapturePrefab;
    //private GameObject videoCaptureObject;

    void Start()
    {

        videoButton = GetComponent<Button>();
        //Kamcord.Init("YOUR_DEVELOPER_KEY", "YOUR_DEVELOPER_SECRET", "com.unity.template.ar_mobile", Kamcord.VideoQuality.Standard);
        image = GetComponent<Image>();
        recorder = new ReplayKitDefaultPlatform();
        recorder.Initialise(this);
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        Init();
    }

    void Init()
    {
        videoButton.OnClickAsObservable().Subscribe(_ => ButtonCheck());
    }


    void ButtonCheck()
    {
        isRecording = !isRecording; 

        if (!isRecording)
        {
            StartCoroutine(StartRecording());
            //recordingIndicator.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(StopRecording());
            //recordingIndicator.gameObject.SetActive(false);
        }
    }

    private IEnumerator StartRecording()
    {
        yield return new WaitForEndOfFrame();
        recorder.StartRecording();
        //objectCamera.gameObject.SetActive(true);
        //image.sprite = Manager.Resources.LoadSprite("VideoStop"); 
        
        //isRecording = true;
    }

    //private IEnumerator StopRecording()
    //{
    //    // ��ȭ ����
    //    recorder.StopRecording();
    //    objectCamera.gameObject.SetActive(true);
    //    string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".mp4";
    //    image.sprite = Manager.Resources.LoadSprite("VideoStart");

    //    // ����� ������ ���� ���
    //    string videoPath = Path.Combine(Application.persistentDataPath, fileName);

    //    yield return new WaitUntil(() => File.Exists(videoPath));

    //    // �ܺ� ����� ��η� ����
    //    string externalPath = Path.Combine(Application.persistentDataPath, "Movies");
    //    Directory.CreateDirectory(externalPath);
    //    string destinationPath = Path.Combine(externalPath, fileName);
    //    File.Move(videoPath, destinationPath); // ���� �̵�


    //    // �̵�� ��ĵ ��û
    //    AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment");
    //    string externalStoragePath = environment.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", environment.GetStatic<string>("DIRECTORY_MOVIES")).Call<string>("getAbsolutePath");
    //    AndroidJavaObject mediaScannerConnection = new AndroidJavaObject("android.media.MediaScannerConnection");
    //    mediaScannerConnection.CallStatic("scanFile", new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"), new string[] { destinationPath }, null, null);

    //    isRecording = false;
    //}


    IEnumerator StopRecording()
    {
        recorder.StopRecording();
        yield return null;
        //objectCamera.gameObject.SetActive(true);
        //image.sprite = Manager.Resources.LoadSprite("VideoStart");

        //string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".mp4";
        //string filePath = Path.Combine(Application.persistentDataPath, fileName);

        //yield return new WaitUntil(() => File.Exists(filePath));

        //byte[] videoBytes = File.ReadAllBytes(filePath);
        
        //if (!string.IsNullOrEmpty(fileName))
        //{
        //    NativeGallery.Permission permission = NativeGallery.SaveVideoToGallery(filePath, "AR_Videos", fileName, (success, path) =>
        //    {
        //        if (success)
        //        {
        //            Debug.Log("Video saved to gallery: " + path);
        //        }
        //        else
        //        {
        //            Debug.Log("Failed to save video to gallery");
        //        }
        //    });
        //}
    }

    void LateUpdate()
    {
        RenderTexture.active = objectCamera.targetTexture;
        arCamera.Render();
        RenderTexture.active = null;
    }

    // ��ȭ �Ͻ� ����
    private void PauseRecording()
    {
        if (isRecording)
        {
            Kamcord.Pause();
        }
    }

    // ��ȭ �簳
    private void ResumeRecording()
    {
        if (isRecording)
        {
            Kamcord.Resume();
        }
    }

    // ��ȭ ������ Ȯ��
    private bool IsRecording()
    {
        return isRecording;
    }
    public void OnInitialiseSuccess()
    {
        Debug.Log("ReplayKitDefaultPlatform �ʱ�ȭ ����!");
    }

    public void OnInitialiseFailed(string reason)
    {
        Debug.Log("ReplayKitDefaultPlatform �ʱ�ȭ ����: " + reason);
    }

    public void OnRecordingStarted()
    {
        objectCamera.gameObject.SetActive(true);
        image.sprite = Manager.Resources.LoadSprite("VideoStop");

    }

    public void OnRecordingStopped()
    {
        objectCamera.gameObject.SetActive(true);
        image.sprite = Manager.Resources.LoadSprite("VideoStart");
    }

    public void OnRecordingFailed(string message)
    {
        throw new NotImplementedException();
    }

    public void OnRecordingAvailable()
    {
        throw new NotImplementedException();
    }

    public void OnPreviewOpened()
    {
        throw new NotImplementedException();
    }

    public void OnPreviewClosed()
    {
        throw new NotImplementedException();
    }

    public void OnPreviewShared()
    {
        throw new NotImplementedException();
    }

    public void OnPreviewSaved(string error)
    {
        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".mp4";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        //yield return new WaitUntil(() => File.Exists(filePath));

        byte[] videoBytes = File.ReadAllBytes(filePath);

        if (!string.IsNullOrEmpty(fileName))
        {
            NativeGallery.Permission permission = NativeGallery.SaveVideoToGallery(filePath, "AR_Videos", fileName, (success, path) =>
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
    }

    public void OnRecordingUIStartAction()
    {
        throw new NotImplementedException();
    }

    public void OnRecordingUIStopAction()
    {
        throw new NotImplementedException();
    }
}

