using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UniRx.Triggers;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.IO;

public class CameraController : UI
{
    [SerializeField] private RectTransform content;
    [SerializeField] private Button camerago;
    [SerializeField] private Button videogo;

    GameObject buttons;

    [SerializeField] private bool isCameraButton = true;
    [SerializeField] private bool isDragging = false;

    private Vector2 centorPosition;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 cameraPosition = new Vector2(100f, 380f);
    private Vector2 videoPosition = new Vector2(-200f, 380f);

    private float moveSpeed = 5f;


    private void Start()
    {
        buttons = transform.Find("Viewport").gameObject;
        centorPosition = content.transform.position;
        camerago.GetComponent<Button>();
        videogo.GetComponent<Button>();
        Init();
    }

    private void Init()
    {
        //드래그 시작
        Observable.Merge(camerago.OnBeginDragAsObservable(),
            videogo.OnBeginDragAsObservable()).Subscribe(_ => MovingCheck());

        //드래그 중
        Observable.Merge(camerago.OnDragAsObservable(),
            videogo.OnDragAsObservable()).Subscribe(_ => ButtonMove());
        
        //드래그 종료
        Observable.Merge(camerago.OnEndDragAsObservable(),
            videogo.OnEndDragAsObservable()).Subscribe(_ => SetButtonPosition());

        //클릭
        Observable.Merge(camerago.OnClickAsObservable(),
            videogo.OnClickAsObservable()).Subscribe(_ => TakeTouch());
    }

    private void TakeTouch()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;

        switch (go.name)
        {
            case "CameraButton" :
                TakePhoto();
                break;
            case "VideoButton" :
                //비디오 실행 메소드
                break;
            default :
                break;
        }
    }

    private void MovingCheck()
    {
        isDragging = true;
#if UNITY_EDITOR
        startPosition = Input.mousePosition;
#else
        startPosition = Input.touches[0].position;
#endif
    }

    private void ButtonMove()
    {
        Vector2 currentTouchPosition;

#if UNITY_EDITOR
        currentTouchPosition = Input.mousePosition;
#else
        if (Input.touchCount >0)
        {
            currentTouchPosition = Input.touches[0].position;
        }
        else { return; }
#endif

        Vector2 offset = currentTouchPosition - startPosition;
        content.transform.position += new Vector3(offset.x, 0f, 0f) * Time.deltaTime * moveSpeed;
        endPosition = content.transform.position;

        if (endPosition.x > centorPosition.x)
        {
            isCameraButton = true;
        }

        else
        {
            isCameraButton = false;
        }

    }

    private void SetButtonPosition()
    {
        if (isCameraButton)
        {
            content.transform.position = cameraPosition;
        }

        else
        {
            content.transform.position = videoPosition;
        }
        isDragging = false;
    }


    private void TakePhoto()
    {
        if (!isDragging)
        {
            StartCoroutine(TakeScreenshotCoroutine());
        }
    }

    private IEnumerator TakeScreenshotCoroutine()
    {
        buttons.SetActive(false);
        Manager.UI.backButton.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();

        // 스크린샷 찍기
        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        ScreenCapture.CaptureScreenshot(fileName);

        // 스크린샷이 저장될 때까지 기다림
        yield return new WaitUntil(() => File.Exists(filePath));

        // 갤러리에 이미지 저장
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(filePath, "Gallery", fileName, (success, imagePath) =>
        {
            if (success)
            {
                Debug.Log("Image saved to gallery: " + imagePath);
            }
            else
            {
                Debug.Log("Failed to save image to gallery");
            }
        });
        buttons.SetActive(true);
        Manager.UI.backButton.gameObject.SetActive(true);
    }

}
