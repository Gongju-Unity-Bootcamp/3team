using UnityEngine;
using UnityEngine.UI;

public class ARCamera : MonoBehaviour
{
    public void OutputCamera(RawImage uiImage)
    {
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);

        uiImage.texture = renderTexture;

        GetComponent<Camera>().targetTexture = renderTexture;
    }
}
