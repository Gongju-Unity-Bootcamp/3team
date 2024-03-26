using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARManager : MonoBehaviour
{
    #region AR카메라 세팅은 냅두고 컴포넌트만 변경시 사용할 컴포넌트
    ARTrackedImageManager tracked;
    ARPointCloudManager pointCloud;
    ARFaceManager face;
    ARPlaneManager plane;
    #endregion

    MapData mapData;
    GameObject ARComponent;
    ARCameraManager arCamera;

    public void Init(MapID id)
    {
        mapData = Manager.Data.Map[id];
        string doName = string.Concat(Define.Docent.DOCENT, mapData.Name);
        ARComponent = Manager.Resources.Instantiate(doName);
        arCamera = ARComponent.transform.Find("AR Camera").GetComponent<ARCameraManager>();
        #region 컴포넌트 초기화 (현재는 안씀)
        tracked = ARComponent.transform.Find("XR Origin (XR Rig)").GetComponent<ARTrackedImageManager>();
        pointCloud = ARComponent.transform.Find("XR Origin (XR Rig)").GetComponent<ARPointCloudManager>();
        face = ARComponent.transform.Find("XR Origin (XR Rig)").GetComponent<ARFaceManager>();
        plane = ARComponent.transform.Find("XR Origin (XR Rig)").GetComponent<ARPlaneManager>();
        #endregion
    }

    #region 맵 타입에 따라 생성할 타입을 정해줌 (현재는 안씀)
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



}
