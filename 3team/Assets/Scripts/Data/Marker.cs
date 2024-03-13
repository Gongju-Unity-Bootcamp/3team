using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class Marker : MonoBehaviour
{
    public MapData mapData; 
    public MapID Id { get; set; }
    public string Name { get; set; }
    public string Sprite { get; set; }
    public string Sound { get; set; }
    public string Information { get; set; }
    public string Description { get; set; }

    private GameObject Infos;
    private GameObject Map;
    private GameObject Navi;
    private GameObject ARNavi;

    private Button backButton;
    private Button mapButton;
    private Button naviButton;
    private Button arNaviButton;
    private Button naviEndButton;


    private Image imageRenderer;
    private Text nameText;
    private Text informationText;
    private Text AddressText;

    private Stack<GameObject> TrueFalse = new Stack<GameObject>();
    private bool isNavi;
    private void Awake()
    {
        SetComopnent();
        Init();
    }
    public void Init()
    {
        SetObserver();
        //SetMarker();

        //this.Id = mapData.Id;
        //mapData = Manager.Data.Map[Id];
        //this.Name = mapData.Name;
        //this.Sprite = mapData.Sprite;
        //this.Sound = mapData.Sound;
        //this.Description = mapData.Address;
        //this.Information = mapData.Information;

        Map.SetActive(false);
        Navi.SetActive(false);
        ARNavi.SetActive(false);

    }
    void SetComopnent()
    {
        Infos           = transform.Find("Info").gameObject;
        Map             = transform.Find("Map").gameObject;
        Navi            = transform.Find("Navi").gameObject;
        ARNavi          = transform.Find("ARNavi").gameObject;
        backButton      = transform.Find("Back").GetComponent<Button>();
        imageRenderer   = Infos.transform.Find("Image").gameObject.GetComponent<Image>();
        nameText        = Infos.transform.Find("Name").gameObject.GetComponent<Text>();
        informationText = Infos.transform.Find("Information").gameObject.GetComponent<Text>();
        AddressText     = Infos.transform.Find("Address").gameObject.GetComponent<Text>();
        mapButton       = Infos.transform.Find("MapButton").gameObject.GetComponent<Button>();
        naviButton      = Map.transform.Find("NaviButton").gameObject.GetComponent<Button>();
        arNaviButton    = Navi.transform.Find("ARNaviButton").gameObject.GetComponent<Button>();
        naviEndButton   = ARNavi.transform.Find("NaviEndButton").gameObject.GetComponent<Button>();
    }

    //NaviButtonµµ ³Ö¾î¾ßµÊ.
    void SetObserver()
    {
        //var back = backButton.OnClickAsObservable().Select(_ => backButton.gameObject);
        //var navi = naviButton.OnClickAsObservable().Select(_ => naviButton.gameObject);
        //var arNavi = arNaviButton.OnClickAsObservable().Select(_ => arNaviButton.gameObject);
        //var map = mapButton.OnClickAsObservable().Select(_ => mapButton.gameObject);
        //var naviEnd = naviEndButton.OnClickAsObservable().Select(_ => naviEndButton.gameObject);

        Observable.Merge(backButton.OnClickAsObservable(),
                         naviButton.OnClickAsObservable(), 
                         arNaviButton.OnClickAsObservable(),
                         mapButton.OnClickAsObservable(),
                         naviEndButton.OnClickAsObservable()).Subscribe(go => ClickCheck());
    }

    void SetMarker()
    {
        imageRenderer.sprite = Manager.Resources.LoadSprite(Sprite);
        nameText.text = this.Name;
        informationText.text = this.Information;
        AddressText.text = this.Description;
    }


    void TrueObject(GameObject go)
    {
        TrueFalse.Push(go);
        go.SetActive(true);
    }

    void FalseObject()
    {

        if (TrueFalse.Count <= 0) { gameObject.SetActive(false); }
        GameObject go = TrueFalse.Pop();
        go.SetActive(false);
    }


    void ClickCheck()
    {
        
        GameObject go = EventSystem.current.currentSelectedGameObject;
        Debug.Log(go.name);
        switch (go)
        {
            case var button when button == mapButton.gameObject:
                TrueObject(Map);
                break;
            case var button when button == naviButton.gameObject:
                TrueObject(Navi);
                break;
            case var button when button == arNaviButton.gameObject:
                TrueObject(ARNavi);
                break;
            case var button when button == naviButton.gameObject || backButton.gameObject:
                FalseObject();
                break;
            default:
                break;
        }
    }
}
