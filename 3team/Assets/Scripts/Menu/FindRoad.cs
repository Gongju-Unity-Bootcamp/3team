
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class FindRoad : UI
{
    public GameObject Map;
    public GameObject Infos;
    public GameObject NaviSearch;
    public GameObject ARNavi;
    [SerializeField] private GameObject Popup; //직접 어싸인?

    private MarkerInfo docentInfo;

    private Button roadInfoButton;
    private Button naviSearchButton;
    private Button allowanceButton;
    private Button notAllowanceButton;
    private Button naviEndButton;
    //private Button[] markerButtons;

    private void Awake()
    {
        SetComponent();
        Init();

        ARNavi.SetActive(false);
    }
    void SetComponent()
    {
        Map              = transform.Find("Map").gameObject;
        //Markers          = Map.transform.Find("Markers").gameObject;
        Infos            = transform.Find("Info").gameObject;
        NaviSearch       = transform.Find("NaviSearch").gameObject;
        ARNavi           = transform.Find("ARNavi").gameObject;

        docentInfo = Infos.GetComponent<MarkerInfo>();

        //Marker -> Info
        //이건 Marker오브젝트 따로 관리

        //Info -> RoadInfo
        roadInfoButton   = Infos.transform.Find("RoadInfoButton").gameObject.GetComponent<Button>();

        //navi -> popup
        naviSearchButton = NaviSearch.transform.Find("NaviSearchButton").gameObject.GetComponent<Button>();

        //popup -> ARNavi
        allowanceButton = Popup.transform.Find("AllowanceButton").gameObject.GetComponent<Button>();
        notAllowanceButton = Popup.transform.Find("NotAllowanceButton").gameObject.GetComponent<Button>();

        //RoadView -> Map
        naviEndButton    = ARNavi.transform.Find("NaviEndButton").gameObject.GetComponent<Button>();


    }

    void Init()
    {
        Observable.Merge(naviSearchButton.OnClickAsObservable(),
                         roadInfoButton.OnClickAsObservable(),
                         naviEndButton.OnClickAsObservable()).Subscribe(go => ClickCheck());
    }

    protected override void ClickCheck()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        
        switch (go.name)
        {
            case "RoadInfoButton": //Info안에 있는 버튼
                GoRoadInfo();
                break;
            case "NaviSearchButton": //NaviSearch안에 있는 버튼
                GoNaviSearch();
                break;
            case "AllowanceButton": //위치 동의
                WhatAllowance(go);
                break;
            case "NotAllowanceButton": //위치 동의 거부
                WhatAllowance(go);
                break;
            case "NaviEndButton": //AR안내 종료
                EndARNavi();
                break;
            default:
                break;
        }
    }

    void GoRoadInfo()
    {
        //NaviSearch오브젝트 띄울때 표기될 정보 메소드
        //현재 인포에 보이는 mapData가져옴
        //ex: NaviTureFalse(docentInfo());
        base.ForwardPage(NaviSearch);
    }

    void GoNaviSearch(GameObject go = null)
    {
        Popup.SetActive(true);
        naviSearchButton.gameObject.SetActive(false);
    }

    void WhatAllowance(GameObject bt)
    {
        if (bt == allowanceButton) 
        { 
            ARNavi.SetActive(true);
            NaviSearch.SetActive(false);
        }
        else 
        { 
            Popup.SetActive(false);
            naviSearchButton.gameObject.SetActive(true);
        }
    }

    void EndARNavi(GameObject go = null)
    {
        if (go == Map || BStack.Count == THIS_MAIN_MANU) 
        { 
            return; 
        }
        else 
        { 
            EndARNavi(BackPage()); 
        }
    }

    bool IsButtonType(GameObject go)
    {
        bool isResult = false;


        return isResult;
    }

}
