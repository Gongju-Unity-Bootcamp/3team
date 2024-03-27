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
    public GameObject DocentMenu;
    public GameObject RoadView;
    public GameObject Exit;
    public GameObject ARMenu;
    public GameObject ARCamera; // ??
    private Transform map;
    private NaverMapAPI _naverMapApi;
    
    public Button backButton;

    public MapID mapID;

    public Vector2 userPosition;
    public Vector2 markerPosition;
    
    protected const int THIS_MAIN_MANU = 1;
    
    protected bool isBackButton;

    public Stack<GameObject> BStack;

    public void Init()
    {
        SetObject();

        BStack = new Stack<GameObject>();
        BStack.Push(MainMenu);
    }

    void SetObject()
    {
        Manager.Resources.Instantiate("UIController");
        ARCamera = Manager.Resources.Instantiate("ARCamera");
        UIController = GameObject.Find("UIController");
        MainMenu = UIController.transform.Find("MainMenu").gameObject;
        FindView = UIController.transform.Find("FindView").gameObject;
        DocentMenu = UIController.transform.Find("DocentMenu").gameObject;
        RoadView = UIController.transform.Find("RoadView").gameObject;
        ARMenu = UIController.transform.Find("ARController").gameObject;
        backButton = UIController.transform.Find("BackButton").GetComponent<Button>();
        map = FindView.transform.GetChild(0);
        _naverMapApi = map.GetComponent<NaverMapAPI>();
    }


    public void BackButtonCheak()
    {
        if (!ARCamera.gameObject.activeSelf)
        {
            ARCamera.gameObject.SetActive(true);
        }
        if(BStack.Count != 0) 
        { 
            GameObject go = Manager.UI.BStack.Peek();
 
            if (go.name == "ARNavi" || go.name == "MainMenu")
            {
                isBackButton = false;
            }

            else
            {
                isBackButton = true;
            }
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


    }

    //true: 유저가 마커 범위안에 있는거
    //false: 유저가 마커 범위 밖에 있는거
    public float distance { get; set; }
    public float criteria { get; set; }
    public bool IsUserPosition()
    {
        distance = CalculateDistance();
        criteria = 0.000349f;
        bool isWithinRange = distance < criteria;
        Debug.Log(isWithinRange);
        return isWithinRange;
    }

    public float CalculateDistance()
    {
        float disX = markerPosition.x - _naverMapApi.userLongitude;
        float disY = markerPosition.y - _naverMapApi.userLatitude;
        float distance = Mathf.Sqrt(disX * disX + disY * disY);
        return distance;
    }
}
