using UnityEngine;
using UnityEngine.UI;

public class MarkerInfo : MonoBehaviour
{
    public MapID mapID;
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

    public void Init(MapID id)
    {
        mapID = id;
        mapData = Manager.Data.Map[mapID];
        SetData();
    }

    void SetData()
    {
        image.sprite = Manager.Resources.LoadSprite(mapData.Sprite);
        address.text = mapData.Address;
        name.text = mapData.Name;
        infomation.text = mapData.Information;
    }

    public MapID GetMapID()
    {
        return mapID;
    }
}
