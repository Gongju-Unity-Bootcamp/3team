using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class FindRoad : MonoBehaviour
{
    private GameObject Infos;
    private GameObject Map;
    private GameObject ARNavi;

    private Button backButton;
    private Button mapButton;
    private Button naviButton;
    private Button naviEndButton;

    private Image imageRenderer;
    private Text nameText;
    private Text informationText;
    private Text AddressText;

    private Stack<GameObject> backPage = new Stack<GameObject>();

    private void Awake()
    {
        SetComopnent();
        Init();

        Map.SetActive(false);
        ARNavi.SetActive(false);
    }
    void SetComopnent()
    {
        Infos           = transform.Find("Info").gameObject;
        Map             = transform.Find("Map").gameObject;
        ARNavi          = transform.Find("ARNavi").gameObject;
        backButton      = transform.Find("Back").GetComponent<Button>();
        imageRenderer   = Infos.transform.Find("Image").gameObject.GetComponent<Image>();
        nameText        = Infos.transform.Find("Name").gameObject.GetComponent<Text>();
        informationText = Infos.transform.Find("Information").gameObject.GetComponent<Text>();
        AddressText     = Infos.transform.Find("Address").gameObject.GetComponent<Text>();
        mapButton       = Infos.transform.Find("MapButton").gameObject.GetComponent<Button>();
        naviButton      = Map.transform.Find("NaviButton").gameObject.GetComponent<Button>();
        naviEndButton   = ARNavi.transform.Find("NaviEndButton").gameObject.GetComponent<Button>();
    }

    void Init()
    {
        Observable.Merge(backButton.OnClickAsObservable(),
                         naviButton.OnClickAsObservable(),
                         mapButton.OnClickAsObservable(),
                         naviEndButton.OnClickAsObservable()).Subscribe(go => ClickCheck());
    }

    private void TrueObject(GameObject go)
    {
        backPage.Push(go);
        go.SetActive(true);
    }

    private void FalseObject()
    {

        if (backPage.Count <= 0) { gameObject.SetActive(false); }
        GameObject go = backPage.Pop();
        go.SetActive(false);
    }


    private void ClickCheck()
    {

        GameObject go = EventSystem.current.currentSelectedGameObject;
        switch (go)
        {
            case var button when button == mapButton.gameObject:
                TrueObject(Map);
                break;
            case var button when button == naviButton.gameObject:
                TrueObject(ARNavi);
                break;
            case var button when button == naviEndButton.gameObject || backButton.gameObject:
                FalseObject();
                break;
            default:
                break;
        }
    }

}
