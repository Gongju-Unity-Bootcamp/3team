using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    

    public void ClickFindRoadButton()
    {
        Debug.Log("��ưŬ��");
        //�˾�����
        Manager.Sound.EffectPlay("Clickbutton");
    }
    public void ClickARZoneButton()
    {
        //�˾�����
        Manager.Sound.EffectPlay("Clickbutton");
    }
    public void ClickRoadViewButton()
    {
        //�˾�����
        Manager.Sound.EffectPlay("Clickbutton");
    }
    public void ClickExitButtonn()
    {
        //�˾�����
        Manager.Sound.EffectPlay("Clickbutton");
    }
}
