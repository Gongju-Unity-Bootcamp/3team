using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEditor;
using System;

public class MainMenu : UI
{
    [SerializeField] private Button FindRoad;
    [SerializeField] private Button Docent;
    [SerializeField] private Button RoadView;
    [SerializeField] private Button Exit;

    public void Init()
    {
        FindRoad = transform.Find("FindRoadButton").GetComponent<Button>();
        Docent = transform.Find("RoadViewButton").GetComponent<Button>();
        RoadView = transform.Find("DocentButton").GetComponent<Button>();
        Exit = transform.Find("ExitButton").GetComponent<Button>();
        SetButton();
    }

    private void SetButton()
    {
        base.Init(Manager.UI.UIController.transform.Find("BackButton").GetComponent<Button>());

        var buttonClicks = Observable.Merge(
            FindRoad.OnClickAsObservable().Select(_ => FindRoad),
            Docent.OnClickAsObservable().Select(_ => Docent),
            RoadView.OnClickAsObservable().Select(_ => RoadView),
            Exit.OnClickAsObservable().Select(_ => Exit)
        );

        buttonClicks.Subscribe(uiName => ClickCheck(uiName));
        backButton.OnClickAsObservable().Subscribe(_ => GoBack());
        base.BStack.Push(gameObject);
    }

    void GoBack()
    {
        GameObject pop = base.BackPage();
        GameObject peek = base.BStack.Peek();
        peek.SetActive(true);
        pop.SetActive(false);
    }
    protected override void ClickCheck(Button bt)
    {
        if(bt == Exit || bt == null) 
        { 
            ExitApplication();
            return;
        }

        GameObject go = bt switch
        {
            _ when bt == FindRoad => Manager.UI.FindView,
            _ when bt == Docent   => Manager.UI.Docent,
            _ when bt == RoadView => Manager.UI.RoadView,
            _ => null
        };

        Debug.Log(go.name);
        base.ForwardPage(go);
    }




void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
