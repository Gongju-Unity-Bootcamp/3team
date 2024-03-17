using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class UI : MonoBehaviour
{
    [SerializeField] protected Stack<GameObject> BStack = new Stack<GameObject>();
    protected Button backButton;

    protected const int THIS_MAIN_MANU = 2;
    protected void Init(Button button)
    {
        this.backButton = button;
    }
    protected GameObject BackPage()
    {
        if (BStack.Count == THIS_MAIN_MANU) 
        {
            backButton.gameObject.SetActive(false);
        }
        else
        {
            backButton.gameObject.SetActive(true);
        }
        return BStack.Pop();
    }

    protected void ForwardPage(GameObject go)
    {
        go.SetActive(true);
        GameObject peek = BStack.Peek();
        peek.SetActive(false);
        BStack.Push(go);
        if (!backButton.gameObject.activeSelf)
        {
            backButton.gameObject.SetActive(true);
        }
        if (go.name == "ARNavi")
        {
            backButton.gameObject.SetActive(false);
        }
    }

    protected virtual void ClickCheck() { }
    protected virtual void ClickCheck(Button bt) { }
}