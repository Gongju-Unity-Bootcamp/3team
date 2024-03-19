using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI : MonoBehaviour
{
    protected void BackPage()
    {
        GameObject go = Manager.UI.BStack.Pop();
        go.SetActive(false);
        Manager.UI.BackButtonCheak();
        
    }

    protected void ForwardPage(GameObject go)
    {
        go.SetActive(true);
        Manager.UI.BStack.Push(go);
        Manager.UI.BackButtonCheak();
    }

    protected virtual void ClickCheck() { }
    protected virtual void ClickCheck(Button bt) { }
}