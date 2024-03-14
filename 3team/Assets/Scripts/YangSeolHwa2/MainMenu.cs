using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button FindRoad;
    [SerializeField] private Button Docent;
    [SerializeField] private Button RoadView;
    [SerializeField] private Button Exit;

    public void Awake()
    {
        FindRoad = transform.Find("FindRoadButton").GetComponent<Button>();
        Docent = transform.Find("DocentButton").GetComponent<Button>();
        RoadView = transform.Find("RoadViewButton").GetComponent<Button>();
        Exit = transform.Find("ExitButton").GetComponent<Button>();
    }

    public void Start()
    {
        var buttonClicks = Observable.Merge(
            FindRoad.OnClickAsObservable().Select(_ => "FindRoad"),
            Docent.OnClickAsObservable().Select(_ => "Docent"),
            RoadView.OnClickAsObservable().Select(_ => "RoadView"),
            Exit.OnClickAsObservable().Select(_ => "Exit")
        );

        buttonClicks.Subscribe(uiName => ChangeUI(uiName));
    }

    void ChangeUI(string uiName)
    {
        Manager.UI.MainMenu.SetActive(false);

        switch (uiName)
        {
            case "FindRoad":
                Manager.UI.FindView.SetActive(true);
                break;
            case "Docent":
                Manager.UI.Docent.SetActive(true);
                break;
            case "RoadView":
                Manager.UI.RoadView.SetActive(true);
                break;
            case "Exit":
                ExitApplication();
                break;
        }
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
