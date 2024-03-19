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

    public Button backButton;
    
    public Vector2 userPosition;
    
    protected const int THIS_MAIN_MANU = 1;
    
    protected bool isBackButton;
    
    public Stack<GameObject> BStack = new Stack<GameObject>();

    public void Init()
    {
        UIController = GameObject.Find("UIController");
        MainMenu = UIController.transform.Find("MainMenu").gameObject;
        FindView = UIController.transform.Find("FindView").gameObject;
        Docent = UIController.transform.Find("Docent").gameObject;
        RoadView = UIController.transform.Find("RoadView").gameObject;
        backButton = UIController.transform.Find("BackButton").GetComponent<Button>();
        BStack.Push(gameObject);
    }

    protected virtual void ClickCheck() { }
    protected virtual void ClickCheck(Button bt) { }

    public void BackButtonCheak()
    {
        GameObject go = Manager.UI.BStack.Peek();
        if (go.name == "ARNavi"|| go.name == "MainMenu")
        {
            isBackButton = false;
        }

        else if (BStack.Count == THIS_MAIN_MANU)
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
    }


}
