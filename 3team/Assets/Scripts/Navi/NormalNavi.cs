using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalNavi : MonoBehaviour
{
    private MapProcessor1 _mapProcessor;
    // Start is called before the first frame update
    void Start()
    {
        Transform FindView = transform.parent.parent;
        Transform map = FindView.GetChild(0);
        _mapProcessor = map.GetComponent<MapProcessor1>();
        _mapProcessor.InitStartProcessImage(_mapProcessor.userPos, _mapProcessor.buttonPos);
    }

}
