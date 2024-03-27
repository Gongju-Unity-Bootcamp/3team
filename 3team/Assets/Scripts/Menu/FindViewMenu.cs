using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;
using UnityEngine.Android;


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

    private MapProcessor1 _mapProcessor;

    private ARNavi _navi;
    private Direction _direction;
    private void Awake()
    {
        SetComponent();
        Init();
        ARNavi.SetActive(false);
    }

    void SetComponent()
    {
        Map = transform.Find("Map").gameObject;
        Infos = transform.Find("Info").gameObject;
        NaviSearch = transform.Find("NaviSearch").gameObject;
        ARNavi = transform.Find("ARNavi").gameObject;

        _mapProcessor = transform.Find("Map").GetComponent<MapProcessor1>();

        roadInfoButton = Infos.transform.Find("RoadInfoButton").gameObject.GetComponent<Button>();
        naviSearchButton = NaviSearch.transform.Find("NaviSearchButton").gameObject.GetComponent<Button>();
        naviEndButton = ARNavi.transform.Find("NaviEndButton").gameObject.GetComponent<Button>();

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
            mk.Init((MapID)i + 1);
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
            case "NaviEndButton": //AR�ȳ� ����
                EndARNavi();
                break;
            default:
                break;
        }
    }

    void GoRoadInfo()
    {
        base.BackPage();
        if (Manager.UI.IsUserPosition())
        {
            if(Manager.UI.BStack.Count != 0)
            {
                base.ForwardPage(Manager.UI.ARMenu);
            }
        }
        else
        {
            _mapProcessor.Init(Manager.UI.userPosition, GetDestination());
            base.ForwardPage(NaviSearch);
        }
    }

    void GoNaviSearch(GameObject go = null)
    {
        base.ForwardPage(ARNavi);
        GameObject arSession = GameObject.FindWithTag("ARSession");
        arSession.transform.GetChild(0).gameObject.SetActive(true);
        Transform coupon = ARNavi.transform.Find("Coupon");
        Coupon _coupon = coupon.GetComponent<Coupon>();
        _coupon.CouponUpdate();
        
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
            base.BackPage();
            EndARNavi(go);

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
