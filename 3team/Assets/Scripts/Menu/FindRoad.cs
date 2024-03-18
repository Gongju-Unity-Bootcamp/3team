
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
    [SerializeField] private GameObject Popup; //���� �����?

    private MarkerInfo docentInfo;

    private Button roadInfoButton;
    private Button naviSearchButton;
    private Button allowanceButton;
    private Button notAllowanceButton;
    private Button naviEndButton;
    //private Button[] markerButtons;

    private MapProcessor _mapProcessor;

    private void Awake()
    {
        SetComponent();
        Init();
        _mapProcessor = GetComponent<MapProcessor>();

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
        //�̰� Marker������Ʈ ���� ����

        //Info -> RoadInfo
        roadInfoButton   = Infos.transform.Find("RoadInfoButton").gameObject.GetComponent<Button>();

        //navi -> popup
        naviSearchButton = NaviSearch.transform.Find("NaviSearchButton").gameObject.GetComponent<Button>();

        //popup -> ARNavi
        allowanceButton = Popup.transform.Find("AllowanceButton").gameObject.GetComponent<Button>();
        notAllowanceButton = Popup.transform.Find("NotAllowanceButton").gameObject.GetComponent<Button>();

        //RoadView -> Map
        naviEndButton    = ARNavi.transform.Find("NaviEndButton").gameObject.GetComponent<Button>();

        Observable.Merge(naviSearchButton.OnClickAsObservable(),
                 roadInfoButton.OnClickAsObservable(),
                 naviEndButton.OnClickAsObservable()).Subscribe(go => ClickCheck());
    }


    void Init()
    {
        int markerCount = Manager.Data.Map.Count;
        for (int i = 0; i < markerCount; ++i)
        {
            GameObject go = Manager.Resources.LoadPrefab("Marker");
            Marker mk = go.AddComponent<Marker>();
            mk.transform.parent = Map.transform;
            mk.Init((MapID)i);
        }

    }

    protected override void ClickCheck()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        
        switch (go.name)
        {
            case "RoadInfoButton": //Info�ȿ� �ִ� ��ư
                GoRoadInfo();
                break;
            case "NaviSearchButton": //NaviSearch�ȿ� �ִ� ��ư
                GoNaviSearch();
                break;
            case "AllowanceButton": //��ġ ����
                WhatAllowance(go);
                break;
            case "NotAllowanceButton": //��ġ ���� �ź�
                WhatAllowance(go);
                break;
            case "NaviEndButton": //AR�ȳ� ����
                EndARNavi();
                break;
            default:
                break;
        }
    }

    void GoRoadInfo()
    {
        //NaviSearch������Ʈ ��ﶧ ǥ��� ���� �޼ҵ�
        //���� ������ ���̴� mapData������
        _mapProcessor.Init(Manager.UI.userPosition, GetDestination());
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

    Vector2 GetDestination()
    {
        MarkerInfo go = Infos.gameObject.GetComponent<MarkerInfo>();
        float Lati = Manager.Data.Map[go.mapID].DoorLati;
        float Long = Manager.Data.Map[go.mapID].DoorLong;
        Vector2 locatioDoor = new Vector2(Lati, Long);
        return locatioDoor;
    }

}
