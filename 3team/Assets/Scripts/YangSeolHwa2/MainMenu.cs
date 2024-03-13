using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    [SerializeField] private Button button4;

    public void Awake()
    {
        button1 = transform.Find("NaviButton").GetComponent<Button>();
        button2 = transform.Find("DocentButton").GetComponent<Button>();
        button3 = transform.Find("RoadViewButton").GetComponent<Button>();
        button4 = transform.Find("ExitButton").GetComponent<Button>();
    }

    public void Start()
    {
        var buttonClicks = Observable.Merge(
            button1.OnClickAsObservable().Select(_ => "Navi"),
            button2.OnClickAsObservable().Select(_ => "Docent"),
            button3.OnClickAsObservable().Select(_ => "RoadView"),
            button4.OnClickAsObservable().Select(_ => "Exit")
        );

        buttonClicks.Subscribe(uiName => ChangeUI(uiName));
    }

    void ChangeUI(string uiName)
    {
        Manager.UI.MainMenu.SetActive(false);

        switch (uiName)
        {
            case "Navi":
                Manager.UI.Navi.SetActive(true);
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
