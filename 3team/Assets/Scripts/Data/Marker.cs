using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class Marker : MonoBehaviour
{
    private MapData mapData; 
    private MapID Id { get; set; }
    private string Name { get; set; }
    private string Sound { get; set; }
    private MarkerType Type { get; set; }
    private Image image;
    private Button button;


    public void Init(MapData mapData, Vector3 vector)
    {
        SetComponent();

        Id = mapData.Id;
        mapData = Manager.Data.Map[Id];
        Name = mapData.Name;
        Type = mapData.Type;
        Sound = mapData.Sound;
        image.sprite = Manager.Resources.LoadSprite(Type.ToString());
        transform.position = vector;
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
        info.Init(mapData);
    }
}
