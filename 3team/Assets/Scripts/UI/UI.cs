using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI : MonoBehaviour
{
    protected GameObject BackPage()
    {
        Manager.UI.BackButtonCheak();
        return Manager.UI.BStack.Pop();
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