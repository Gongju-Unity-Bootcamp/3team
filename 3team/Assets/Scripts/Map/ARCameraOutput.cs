using UnityEngine;
using UnityEngine.UI;

public class ARCameraOutput : MonoBehaviour
{
    private GameObject arCamera;
    private ARCamera _ARCamera;
    private RawImage rawImage;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
        arCamera = GameObject.FindWithTag("MainCamera");
        _ARCamera = arCamera.GetComponent<ARCamera>();
        _ARCamera.OutputCamera(rawImage);
    }
}
