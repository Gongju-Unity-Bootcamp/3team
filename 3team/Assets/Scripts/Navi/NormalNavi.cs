using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalNavi : MonoBehaviour
{
    private GameObject arCamera;
    private ARCamera _ARCamera;
    private RawImage rawImage;
    // Start is called before the first frame update
    void Start()
    {
        rawImage = GetComponent<RawImage>();
        arCamera = GameObject.FindWithTag("MainCamera");
        _ARCamera = arCamera.GetComponent<ARCamera>();
        _ARCamera.OutputCamera(rawImage);
    }

}
