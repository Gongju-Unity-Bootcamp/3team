using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CouponActive : MonoBehaviour
{
    private bool couponActive;
    public GameObject coupon;
    private void Start()
    {
        couponActive = true;
        StartCoroutine(CalculateDistanceCoroutine());
    }
    private void Update()
    {
        if(couponActive! && Manager.UI.distance < Manager.UI.criteria)
        {
            couponActive = true;
            UpdateCouponActive();
        }
        else if(couponActive && Manager.UI.distance > Manager.UI.criteria)
        {
            couponActive = false;
            UpdateCouponActive();
        }
    }
    private void UpdateCouponActive()
    {
        if(couponActive)
        {
            coupon.SetActive(true);
        }
        else
        {
            coupon.SetActive(false);
        }
    }

    IEnumerator CalculateDistanceCoroutine()
    {
        while(true)
        {
            float distance = Manager.UI.CalculateDistance();
            Manager.UI.distance = distance;
            yield return new WaitForSeconds(1.5f);
        }
    }
}
