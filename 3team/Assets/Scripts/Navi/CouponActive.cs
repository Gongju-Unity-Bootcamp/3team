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
        if(couponActive == false && Manager.UI.distance < Manager.UI.criteria)
        {
            Debug.Log("쿠폰 활성화");
            couponActive = !couponActive;
            coupon.SetActive(couponActive);
        }
        else if(couponActive && Manager.UI.distance > Manager.UI.criteria)
        {
            Debug.Log("쿠폰 비활성화");
            couponActive = !couponActive;
            coupon.SetActive(couponActive);
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
