using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class DocentButton : UI
{
    MapID mapID;
    [SerializeField] private MapData mapData;
    [SerializeField] private Button docentInfo;

    [SerializeField] private Image doImage;
    [SerializeField] private Text doName;
    [SerializeField] private Text doInfo;

    private void Awake()
    {
        docentInfo = transform.Find("DocentButton").GetComponent<Button>();
        doImage = transform.Find("DocentImage").GetComponent<Image>();
        doName = transform.Find("DocentTextName").GetComponent<Text>();
        doInfo = transform.Find("DocentTextInfo").GetComponent<Text>();

        docentInfo.OnClickAsObservable().Subscribe(_ => ClickCheck());
    }
    public void Init(MapID id)
    {
        mapID = id;
        mapData = Manager.Data.Map[mapID];
        doName.text = mapData.Name;
        doInfo.text = mapData.Information;
        doImage.sprite = Manager.Resources.LoadSprite(mapData.Sprite);
    }

    protected override void ClickCheck()
    {
        GameObject go = Manager.UI.DocentMenu;
        DocentMenu menu = go.GetComponent<DocentMenu>();
        go = Manager.UI.DocentMenu.transform.Find("Info").gameObject;
        menu.SetInfo(mapID);

        base.ForwardPage(go);
    }
}
