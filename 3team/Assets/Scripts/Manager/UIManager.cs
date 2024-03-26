using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject UIController;
    public GameObject MainMenu;
    public GameObject FindView;
    public GameObject DocentMenu;
    public GameObject RoadView;
    public GameObject Exit;
    public GameObject DocentFeature;
    
    public Button backButton;

    public Vector2 userPosition;
    public Vector2 markerPosition;
    
    protected const int THIS_MAIN_MANU = 1;
    
    protected bool isBackButton;
    public bool isSceneChange;

    public Stack<GameObject> BStack;

    private Transform map;
    private NaverMapAPI _naverMapApi;
    public void Init()
    {
        Manager.Resources.Instantiate("UIController");
        Manager.Resources.Instantiate("ARCamera");
        UIController = GameObject.Find("UIController");
        MainMenu = UIController.transform.Find("MainMenu").gameObject;
        FindView = UIController.transform.Find("FindView").gameObject;
        DocentMenu = UIController.transform.Find("DocentMenu").gameObject;
        RoadView = UIController.transform.Find("RoadView").gameObject;
        DocentFeature = UIController.transform.Find("DocentFeature").gameObject;
        backButton = UIController.transform.Find("BackButton").GetComponent<Button>();
        BStack = new Stack<GameObject>();
        BStack.Push(MainMenu);

        map = FindView.transform.GetChild(0);
        _naverMapApi = map.GetComponent<NaverMapAPI>();
    }

    protected virtual void ClickCheck() { }
    protected virtual void ClickCheck(Button bt) { }

    public void BackButtonCheak()
    {
        //ARScene에서 뒤로가기 눌렀을때
        if (isSceneChange)
        {
            Manager.MainInit();
            return;
        }

        //MainScene에서 작동
        GameObject go = Manager.UI.BStack.Peek();

        if (go.name == "ARNavi"|| go.name == "MainMenu")
        {
            isBackButton = false;
        }

        else
        {
            isBackButton = true;
        }

        if (isBackButton) 
        { 
            backButton.gameObject.SetActive(true); 
        }
        else 
        { 
            backButton.gameObject.SetActive(false); 
        }

        if (isSceneChange)
        {
            SceneManager.LoadScene("MainScene");
            isSceneChange = false;
        }

        
    }

    //true: 유저가 마커 범위안에 있는거
    //false: 유저가 마커 범위 밖에 있는거
    public float distance { get; set; }
    public float criteria { get; set; }
    public bool IsUserPosition()
    {
        float disX = markerPosition.x - _naverMapApi.userLongitude;
        float disY = markerPosition.y - _naverMapApi.userLatitude;
        distance = Mathf.Sqrt(disX * disX + disY * disY);
        criteria = 0.000349f;
        bool isWithinRange = distance < criteria;
        return isWithinRange;
    }

}
