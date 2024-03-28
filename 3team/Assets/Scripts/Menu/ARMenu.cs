using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class ARMenu : UI
{
    #region ARī�޶� ������ ���ΰ� ������Ʈ�� ����� ����� ������Ʈ
    ARTrackedImageManager tracked;
    ARPointCloudManager pointCloud;
    ARFaceManager face;
    ARPlaneManager plane;
    #endregion

    MapData mapData;
    GameObject ARComponent;
    GameObject doGameObject;
    Button backButton;
    ARCameraManager arCamera;
    bool isCameraPosition;
    [SerializeField] private Button cameraSwipeButton;

    private void Awake()
    {
        backButton = Manager.UI.backButton.GetComponent<Button>();
        cameraSwipeButton.GetComponent<Button>();
    }

    private void OnEnable()
    {
        Init();
        SetButton();

        #region ������Ʈ �ʱ�ȭ (����� �Ⱦ�)
        //tracked = ARComponent.transform.Find("XR Origin (XR Rig)").GetComponent<ARTrackedImageManager>();
        //pointCloud = ARComponent.transform.Find("XR Origin (XR Rig)").GetComponent<ARPointCloudManager>();
        //face = ARComponent.transform.Find("XR Origin (XR Rig)").GetComponent<ARFaceManager>();
        //plane = ARComponent.transform.Find("XR Origin (XR Rig)").GetComponent<ARPlaneManager>();
        #endregion
    }
    private void Init()
    {
        if(Manager.UI.ARCamera.gameObject.activeSelf)
        {
            Manager.UI.ARCamera.gameObject.transform.Find("XR Origin (XR Rig)").gameObject.SetActive(false);
        }
        mapData = Manager.Data.Map[Manager.UI.mapID];
        string doName = string.Concat(Define.Docent.DOCENT, mapData.Name);
        ARComponent = Manager.Resources.Instantiate(doName, transform);
        doGameObject = Manager.UI.ARMenu.transform.Find(doName).gameObject;
        arCamera = doGameObject.transform.Find("AR Camera").GetComponent<ARCameraManager>();


    }

    void SetButton()
    {
        backButton.OnPointerClickAsObservable().Subscribe(_ => ReturnMain());
        cameraSwipeButton.OnPointerClickAsObservable().Subscribe(_ => SwipeCamera());

    }
    #region �� Ÿ�Կ� ���� ������ Ÿ���� ������ (����� �Ⱦ�)
    public Type ReturnObject(MapType type)
    {
        Type arObject = type switch
        {
            MapType.Park => typeof(GameObject),
            MapType.Parking => typeof(XRReferenceImageLibrary),
            MapType.Guide => typeof(GameObject),
            MapType.Hospital => typeof(GameObject),
            MapType.Camera => typeof(GameObject),
            MapType.Subway => typeof(GameObject),
            MapType.Restroom => typeof(GameObject),
            _ => null
        };

        return arObject;
    }
    #endregion

    void ReturnMain()
    {
        base.BackPage();
        base.ForwardPage(Manager.UI.MainMenu);
    }

    void SwipeCamera()
    {
        isCameraPosition = !isCameraPosition;

        if (isCameraPosition)
        {
            arCamera.requestedFacingDirection = CameraFacingDirection.User;
        }
        else
        {
            arCamera.requestedFacingDirection = CameraFacingDirection.World;
        }

    }

    private void OnDisable()
    {
        Destroy(ARComponent);
    }

}
