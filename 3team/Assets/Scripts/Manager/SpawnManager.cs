using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    GameObject Spawn;

    public void Awake()
    {
        GameObject go = new GameObject();
        go.name = "Spawn";
    }
    public void GetMarker(MapID id, Vector3 vector, Transform trans = null)
    {
        GameObject marker = Manager.Resources.LoadPrefab("Marker");
        marker.transform.parent = Spawn.transform;

        Marker markerInit = marker.GetComponent<Marker>();
        //markerInit.Init(id);
        marker.transform.position = vector; 
        //return marker;
    }

    public void ClearSpawn()
    {
        GameObject go = new GameObject();
        Spawn = go;
    }
}
