using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject UIController;

    public GameObject MainMenu;
    public GameObject FindView;
    public GameObject Docent;
    public GameObject RoadView;
    public GameObject Exit;

    public Vector2 userPosition;

    public void Init()
    {
        UIController = GameObject.Find("UIController");
        MainMenu = UIController.transform.Find("MainMenu").gameObject;
        FindView = UIController.transform.Find("FindView").gameObject;
        Docent = UIController.transform.Find("Docent").gameObject;
        RoadView = UIController.transform.Find("RoadView").gameObject;
        MainMenu go = MainMenu.AddComponent<MainMenu>();
        go.Init();
    }

    
}
