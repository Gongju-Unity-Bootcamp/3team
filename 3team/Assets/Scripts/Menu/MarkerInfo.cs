using UnityEngine;
using UnityEngine.UI;
using System;

public class MarkerInfo : MonoBehaviour
{
    [HideInInspector] public MapID mapID;
    private MapData mapData;
    private Image image;
    private Text address;
    private Text marker_Name;
    private Text infomation;
    [SerializeField] private Text NextText; // 직접 어싸인

    private bool isdoccent;

    private void Awake()
    {
        SetComponent();
    }
    void SetComponent()
    {
        image       = transform.Find("Image").GetComponent<Image>();
        address     = transform.Find("Address").GetComponent<Text>();
        marker_Name = transform.Find("Name").GetComponent<Text>();
        infomation  = transform.Find("Information").GetComponent<Text>();
        NextText.GetComponent<Text>();
    }

    public void Init(MapID id)
    {
        mapID = id;
        mapData = Manager.Data.Map[mapID];
        
        SetData();
        
        GoDocentQuestion();

    }

    void SetData()
    {
        image.sprite = Manager.Resources.LoadSprite(mapData.Sprite);
        address.text = mapData.Address;
        marker_Name.text = mapData.Name;
        infomation.text = mapData.Information;
        Manager.UI.markerPosition = new Vector2(mapData.Longitude, mapData.Latitude);
    }

    void GoDocentQuestion()
    {
        if(Manager.UI.IsUserPosition())
        {
            NextText.text = "도슨트 시작";
        }
        else 
        {
            //NextText.text = "도슨트 시작";
            NextText.text = "길안내";
        }
    }
}
