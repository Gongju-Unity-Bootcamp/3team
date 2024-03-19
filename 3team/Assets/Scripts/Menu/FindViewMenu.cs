
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class FindViewMenu : UI
{
    public GameObject Map;
    public GameObject Infos;
    public GameObject NaviSearch;
    public GameObject ARNavi;

    private MarkerInfo docentInfo;

    private Button roadInfoButton;
    private Button naviSearchButton;
    private Button naviEndButton;
    //private Button[] markerButtons;

    private MapProcessor1 _mapProcessor;

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
        _mapProcessor = transform.Find("Map").GetComponent<MapProcessor1>();
        //Marker -> Info
        //이건 Marker오브젝트 따로 관리

        //Info -> RoadInfo
        roadInfoButton   = Infos.transform.Find("RoadInfoButton").gameObject.GetComponent<Button>();

        //navi -> popup
        naviSearchButton = NaviSearch.transform.Find("NaviSearchButton").gameObject.GetComponent<Button>();


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
            GameObject go = Manager.Resources.Instantiate("Marker", Map.transform);
            Marker mk = go.GetComponent<Marker>();
            mk.Init((MapID)i+1);
            Debug.Log((MapID)i + 1);
;
        }

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
            case "NaviEndButton": //AR안내 종료
                EndARNavi();
                break;
            default:
                break;
        }
    }

    void GoRoadInfo()
    {
         base.BackPage();
        Debug.Log(Manager.UI.userPosition);
        _mapProcessor.Init(Manager.UI.userPosition, GetDestination());
        base.ForwardPage(NaviSearch);
    }

    void GoNaviSearch(GameObject go = null)
    {
        base.ForwardPage(ARNavi);
    }


    void EndARNavi(GameObject go = null)
    {
        if (Manager.UI.BStack.Count == 1) 
        {
            base.ForwardPage(Manager.UI.FindView);
            return; 
        }
        else 
        {
            GameObject ggo = Manager.UI.BStack.Peek();
            base.BackPage();
            EndARNavi(ggo); 

        }
        Debug.Log(Manager.UI.BStack.Count);
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
