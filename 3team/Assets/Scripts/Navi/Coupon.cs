using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coupon : MonoBehaviour
{
    public string couponName { get; set; }
    private Text text;
    public void CouponUpdate()
    {
        text = transform.Find("Name").GetComponent<Text>();
        text.text = couponName;
    }
}
