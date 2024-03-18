using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class Marker : MonoBehaviour
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

        button.OnClickAsObservable().Subscribe(_ => MarkerClick());
    }

    void MarkerClick()
    {
        GameObject go = Manager.UI.FindView.transform.Find("Info").gameObject;
        MarkerInfo info = go.GetComponent<MarkerInfo>();
        info.Init(Id);
    }
}
