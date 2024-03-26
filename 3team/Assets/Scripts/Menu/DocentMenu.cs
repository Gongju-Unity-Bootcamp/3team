using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;
using System.Linq;

public class DocentMenu : UI
{
    [SerializeField] private Transform content; //프리팹에 직접 어싸인
    [SerializeField] private GameObject explanation; //프리팹에 직접 어싸인
    public GameObject Info;

    private Button mapButton;
    private Button docentButton;

    private List<MapID> mapIDs;
    private MapData mapData;

    private Image docentInfoImage;
    private Text docentInfoName;
    private Text docentInfoText;

    private void Awake()
    {
        Info = transform.Find("Info").gameObject;
        docentInfoImage = Info.transform.Find("DocentImage").GetComponent<Image>();
        docentInfoName = Info.transform.Find("DocentInfoName").GetComponent<Text>();
        docentInfoText = Info.transform.Find("DocentInfoText").GetComponent<Text>();
        mapButton = Info.transform.Find("MapButton").GetComponent<Button>();
        docentButton = Info.transform.Find("DocentButton").GetComponent<Button>();
    }

    void OnEnable()
    {
        Init();
        SetButton();
    }
    /// <summary>
    /// 개선방향: 유저의 좌표 정보를 UI매니져를 통해 받아옴
    ///          ㄴ유저좌표기준범위를 MAthf.Abs로 -,+거리로 위도와 경도값을 각각계산.
    ///          ㄴDataManager > Map(딕셔너리, 키는 위도와 경도)안에서 위에 계산한 범위안에 있는 데이터를 호출
    ///          ㄴ호출되는 데이터 갯수만큼 메뉴를 생성시키기.
    /// </summary>
    public void Init()
    {
        mapIDs = new List<MapID>();
        for (int i = 0; i < 8; ++i) 
        {
            mapIDs.Add((MapID)i);
        }
        SetMenu();
    }
    
    void SetButton()
    {
        Observable.Merge(mapButton.OnClickAsObservable(),
                            docentButton.OnClickAsObservable()
            ).Subscribe(_ => ClickCheck());
    }

    /// <summary>
    /// 현재는 데이터테이블에 있는 정보를 모두 불러옴.
    /// Awake위에있는 주석처럼 범위와 갯수를 제한해야됨.
    /// </summary>
    void SetMenu()
    {
        for(int id = 1; id < 8 + 1; ++id)
        {
            MapData data = Manager.Data.Map[(MapID)id];
            GameObject go = Manager.Resources.Instantiate("Docents", content);
            go.name = data.Name;
            DocentButton docentButton = go.GetComponent<DocentButton>();
            docentButton.Init((MapID)id);
        }
    }

    public void SetInfo(MapID id)
    {
        mapData = Manager.Data.Map[id];
        docentInfoImage.sprite = Manager.Resources.LoadSprite(mapData.Sprite);
        docentInfoName.text = mapData.Name;
        docentInfoText.text = mapData.Information;
    }

    protected override void ClickCheck()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;

        switch (go.name)
        {
            case "MapButton":
                base.BackPage();
                base.BackPage();                
                base.ForwardPage(Manager.UI.FindView);
                break;
            case "DocentButton":
                Manager.ARInit();
                break;
        }

    }

    private void OnDisable()
    {
        mapIDs.Clear();
    }

}
