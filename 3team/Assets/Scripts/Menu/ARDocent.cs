using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ARDocent : MonoBehaviour
{
    private GameObject explanation;
    private MapData mapData;

    private Text nameText;
    private Text infoText;
    private Image docentImage;

    private void Awake()
    {
        explanation = transform.Find("Explanation").gameObject;
        nameText = explanation.transform.Find("Name").GetComponent<Text>();
        infoText = explanation.transform.Find("Info").GetComponent<Text>();
        docentImage = transform.Find("DocentImage").GetComponent<Image>();

    }

    public void Init(MapID id)
    {
        mapData = Manager.Data.Map[id];
        nameText.text = mapData.Name;
        infoText.text = mapData.Information;
        docentImage.sprite = Manager.Resources.LoadSprite(mapData.Sprite);
    }

}
