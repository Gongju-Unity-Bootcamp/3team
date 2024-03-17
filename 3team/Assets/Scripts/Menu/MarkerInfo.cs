using UnityEngine;
using UnityEngine.UI;

public class MarkerInfo : MonoBehaviour
{
    private MapData mapData;
    private Image image;
    private Text address;
    private Text name;
    private Text infomation;

    private void Awake()
    {
        SetComponent();
    }
    void SetComponent()
    {
        image      = transform.Find("Image").GetComponent<Image>();
        address    = transform.Find("Address").GetComponent<Text>();
        name       = transform.Find("Name").GetComponent<Text>();
        infomation = transform.Find("Infomation").GetComponent<Text>();
    }

    public void Init(MapData data)
    {
        mapData = data;
        SetData();
    }

    void SetData()
    {
        image.sprite = Manager.Resources.LoadSprite(mapData.Sprite);
        address.text = mapData.Address;
        name.text = mapData.Name;
        infomation.text = mapData.Information;
    }

    public MapData GetMapID()
    {
        return mapData;
    }
}
