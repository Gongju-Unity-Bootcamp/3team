using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI : MonoBehaviour
{
    protected void BackPage()
    {
        //if (Manager.UI.BStack.Count == 1) { return; }
        GameObject go = Manager.UI.BStack.Pop();
        go.SetActive(false);
        Manager.UI.BackButtonCheak();
        if(!Manager.UI.ARCamera.transform.Find("XR Origin (XR Rig)").gameObject.activeSelf)
        {
            Manager.UI.ARCamera.transform.Find("XR Origin (XR Rig)").gameObject.SetActive(true);
        }
        
    }

    protected void ForwardPage(GameObject go)
    {
        Manager.UI.BStack.Push(go);
        go.SetActive(true);
        Manager.UI.BackButtonCheak();
    }

    protected virtual void ClickCheck() { }
    protected virtual void ClickCheck(Button bt) { }
}