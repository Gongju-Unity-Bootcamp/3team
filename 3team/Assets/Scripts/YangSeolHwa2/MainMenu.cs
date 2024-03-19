using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEditor;

public class MainMenu : UI
{
    [SerializeField] private Button FindRoadButton;
    [SerializeField] private Button DocentButton;
    [SerializeField] private Button RoadViewButton;
    [SerializeField] private Button ExitButton;

    public void Start()
    {
        FindRoadButton = transform.Find("FindRoadButton").GetComponent<Button>();
        DocentButton = transform.Find("DocentButton").GetComponent<Button>();
        RoadViewButton = transform.Find("RoadViewButton").GetComponent<Button>();
        ExitButton = transform.Find("ExitButton").GetComponent<Button>();
        SetButton();
    }

    void SetButton()
    {
        var buttonClicks = Observable.Merge(
            FindRoadButton.OnClickAsObservable().Select(_ => FindRoadButton),
            DocentButton.OnClickAsObservable().Select(_ => DocentButton),
            RoadViewButton.OnClickAsObservable().Select(_ => RoadViewButton),
            ExitButton.OnClickAsObservable().Select(_ => ExitButton)
        );

        buttonClicks.Subscribe(uiName => ClickCheck(uiName));
        Manager.UI.backButton.OnClickAsObservable().Subscribe(_ => GoBack());
    }

    void GoBack()
    {
        base.BackPage();
    }
    protected override void ClickCheck(Button bt)
    {
        if (bt == ExitButton) 
        { 
            ExitApplication();
            return;
        }

        GameObject go = bt switch
        {
            _ when bt == FindRoadButton => Manager.UI.FindView,
            _ when bt == DocentButton   => Manager.UI.Docent,
            _ when bt == RoadViewButton => Manager.UI.RoadView,
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
