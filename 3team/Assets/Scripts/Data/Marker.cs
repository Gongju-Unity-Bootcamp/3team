using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class Marker : MonoBehaviour
{
    private MapData mapData;
    private MapID Id;
    private string Name;
    private float Latitude;
    private float Longitude;
    private string Sound;
    private MarkerType Type;
    private Image image;
    private Button button;


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
        image.sprite = Manager.Resources.LoadSprite(Type.ToString());
        transform.position = new Vector3(Latitude, Longitude, 0);
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
