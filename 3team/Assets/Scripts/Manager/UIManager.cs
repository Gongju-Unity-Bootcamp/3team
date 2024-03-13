using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject UI;

    public GameObject MainMenu;
    public GameObject Navi;
    public GameObject Docent;
    public GameObject RoadView;
    public GameObject Exit;

    public void Init()
    {
        UI = GameObject.Find("UI");
        MainMenu = UI.transform.Find("MainMenu").gameObject;
        Navi = UI.transform.Find("Navi").gameObject;
        Docent = UI.transform.Find("Docent").gameObject;
        RoadView = UI.transform.Find("RoadView").gameObject;
    }

    
}
