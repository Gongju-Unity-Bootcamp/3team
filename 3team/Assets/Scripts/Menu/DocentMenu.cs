using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class DocentMenu : UI
{
    [SerializeField] private Transform content; //ÇÁ¸®ÆÕ¿¡ Á÷Á¢ ¾î½ÎÀÎ
    [SerializeField] private GameObject explanation;
    private List<Button> Docents;

    private Button mapButton;
    private Button docentButton;

    private void Awake()
    {
        mapButton = explanation.transform.Find("MapButton").GetComponent<Button>();
        docentButton = explanation.transform.Find("DocentButton").GetComponent<Button>();

        SetButton();
    }
    void SetButton()
    {
        Observable.Merge(mapButton.OnClickAsObservable(),
                            docentButton.OnClickAsObservable()
            ).Subscribe(_ => ClickCheck());
    }
    private void Init(MapData[] mapData)
    {

        for (int i = 0; i < content.childCount; ++i)
        {
            Button bt = content.transform.Find($"DocentButton ({i})").GetComponent<Button>();
            Docents.Add(bt);
        }
    }

    protected override void ClickCheck()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;

        switch (go.name)
        {
            case "MapButton":
                base.ForwardPage(Manager.UI.FindView);
                //base.ForwardPage(Manager.UI.FindView.transform.Find("NaviSearch").gameObject);
                break;
            case "DocentButton":
                //µµ½¼Æ® ±â´É
                //
                break;
        }

    }


}
