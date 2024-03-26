using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Marker : UI
{
    [SerializeField] private MapData mapData;
    [SerializeField] private MapID Id;
    [SerializeField] private string Name;
    [SerializeField] private float Latitude;
    [SerializeField] private float Longitude;
    [SerializeField] private string Sound;
    [SerializeField] private string Type;
    [SerializeField] private Image image;
    [SerializeField] private Button button;


    public void Init(MapID id)
    {
        SetComponent();
        Id = id;
        mapData = Manager.Data.Map[Id];
        Name = mapData.Name;
        Latitude = mapData.Latitude;
        Longitude = mapData.Longitude;
        Type = mapData.Type;
        Sound = mapData.Sound;
        image.sprite = Manager.Resources.LoadSprite(Type);

        NaverMapAPI trans = Manager.UI.FindView.transform.Find("Map").GetComponent<NaverMapAPI>();
        trans.SetLocationMarker(gameObject, Latitude, Longitude);

    }

    void SetComponent()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.OnClickAsObservable().Subscribe(_ => ClickCheck());
    }
    protected override void ClickCheck()
    {
        Manager.UI.markerPosition.x = Longitude;
        Manager.UI.markerPosition.y = Latitude;
        GameObject go = Manager.UI.FindView.transform.Find("Info").gameObject;
        MarkerInfo info = go.GetComponent<MarkerInfo>();
        base.ForwardPage(go);
        info.Init(Id);
        Transform coupon = transform.parent.parent.Find("ARNavi").Find("Coupon");
        Coupon _coupon = coupon.GetComponent<Coupon>();
        Transform destination = transform.parent.parent.Find("NaviSearch").Find("Finding a way PopUp").
            Find("Destination").Find("DestinationLocation");
        Text destinationText = destination.GetComponent<Text>();
        destinationText.text = Name;
        _coupon.couponName = Name;
    }
}
