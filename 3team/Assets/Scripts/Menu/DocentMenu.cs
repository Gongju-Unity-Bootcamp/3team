using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;
using System.Linq;

public class DocentMenu : UI
{
    [SerializeField] private Transform content; //�����տ� ���� �����
    [SerializeField] private GameObject explanation; //�����տ� ���� �����
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
    /// ��������: ������ ��ǥ ������ UI�Ŵ����� ���� �޾ƿ�
    ///          ��������ǥ���ع����� MAthf.Abs�� -,+�Ÿ��� ������ �浵���� �������.
    ///          ��DataManager > Map(��ųʸ�, Ű�� ������ �浵)�ȿ��� ���� ����� �����ȿ� �ִ� �����͸� ȣ��
    ///          ��ȣ��Ǵ� ������ ������ŭ �޴��� ������Ű��.
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
    /// ����� ���������̺� �ִ� ������ ��� �ҷ���.
    /// Awake�����ִ� �ּ�ó�� ������ ������ �����ؾߵ�.
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
