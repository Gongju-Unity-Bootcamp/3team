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
    protected const int THIS_MAIN_MANU = 2;
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

    public void BackButtonCheak(GameObject go = null)
    {
        if (!backButton.gameObject.activeSelf)
        {
            backButton.gameObject.SetActive(true);
        }
        if (go != null && go.name == "ARNavi")
        {
            backButton.gameObject.SetActive(false);
        }
        if (BStack.Count == THIS_MAIN_MANU)
        {
            backButton.gameObject.SetActive(false);
        }
        else
        {
            backButton.gameObject.SetActive(true);
        }
    }


}
