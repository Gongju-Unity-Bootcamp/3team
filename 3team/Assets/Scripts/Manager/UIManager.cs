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
    private bool isSceneChange;

    public Stack<GameObject> BStack;

    public void Init()
    {
        Manager.Resources.Instantiate("UIController");
        UIController = GameObject.Find("UIController");
        MainMenu = UIController.transform.Find("MainMenu").gameObject;
        FindView = UIController.transform.Find("FindView").gameObject;
        DocentMenu = UIController.transform.Find("DocentMenu").gameObject;
        RoadView = UIController.transform.Find("RoadView").gameObject;
        DocentFeature = UIController.transform.Find("DocentFeature").gameObject;
        backButton = UIController.transform.Find("BackButton").GetComponent<Button>();
        BStack = new Stack<GameObject>();
        BStack.Push(MainMenu);
    }

    protected virtual void ClickCheck() { }
    protected virtual void ClickCheck(Button bt) { }

    public void BackButtonCheak()
    {
        //ARScene에서 뒤로가기 눌렀을때
        if (isSceneChange)
        {
            SceneManager.LoadScene("MainScene");
            isSceneChange = false;
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
    public bool IsUserPosition()
    {
        float disX = markerPosition.x - userPosition.x;
        float disY = markerPosition.y - userPosition.y;
        float distance = Mathf.Sqrt(disX * disX + disY * disY);
        float criteria = 0.00000000000000005f;
        bool isWithinRange = distance < criteria;

        return isWithinRange;
    }

}
