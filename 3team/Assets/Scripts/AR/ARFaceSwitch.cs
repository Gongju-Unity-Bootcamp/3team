using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARFaceSwitch : MonoBehaviour
{

    ARFaceManager aRFaceManager;

    public Material[] materials;

    private int switchCount = 0;
    void Start()
    {
        aRFaceManager = GetComponent<ARFaceManager>();
        aRFaceManager.facePrefab.GetComponent<MeshRenderer>().material = materials[0];
    }

    void SwitchFaces()
    {
        foreach(ARFace face in aRFaceManager.trackables)
        {
            face.GetComponent<MeshRenderer>().material = materials[switchCount];
        }

        switchCount = (switchCount + 1) % materials.Length;
    }

    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SwitchFaces();
        }
    }
}