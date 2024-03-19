using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Linq;
using System.IO;
using System.Globalization;
using System;
using System.Diagnostics.CodeAnalysis;
using CsvHelper.Configuration;

public class DataManager : MonoBehaviour
{

    public Dictionary<MapID, MapData> Map = new Dictionary<MapID, MapData>();
    public Dictionary<SoundID, SoundData> Sound = new Dictionary<SoundID, SoundData>();

    public void Awake()
    {
#if UNITY_EDITOR
        Map = ParseToDict<MapID, MapData>("Assets/Resources/Data/MapData.csv", data => data.Id);
        //Sound = ParseToDict<SoundID, SoundData>("Assets/Resources/Data/Sound.csv", data => data.Id);
#else
        TextAsset mapCSV = Resources.Load<TextAsset>("Data/MapData");
        Map = ParseToDict<MapID, MapData>(mapCSV.text, data => data.Id);

#endif
    }
    //private Dictionary<MapID, MapData> ParseToDict<TKey, TItem>([NotNull] string path)
    //{
    //    var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
    //    {
    //        Comment = '#',
    //        AllowComments = true,
    //        Delimiter = ","
    //    };

    //    Dictionary<MapID, MapData> dictionary = new Dictionary<MapID, MapData>();

    //    using (var reader = new StreamReader(path)) 
    //    using (var csv = new CsvReader(reader, csvConfig)) 
    //    {
    //        csv.Read();
    //        csv.ReadHeader();
    //        while (csv.Read()) 
    //        { 
    //            var id = csv.GetField<MapID>("Id");
    //            var name = csv.GetField<string>("Name");
    //            var latitude = csv.GetField<float>("Latitude");
    //            var longitude = csv.GetField<float>("Longitude");
    //            var type = csv.GetField<string>("Type");
    //            var sprite = csv.GetField<string>("Sprite");
    //            var sound = csv.GetField<string>("Sound");
    //            var information = csv.GetField<string>("Information");
    //            var address = csv.GetField<string>("Address");
    //            var doorLati = csv.GetField<float>("DoorLati");
    //            var doorLong = csv.GetField<float>("DoorLong");

    //            var map = new MapData
    //            {
    //                Id = id,
    //                Name = name,
    //                Latitude = latitude,
    //                Longitude = longitude,
    //                Type = type,
    //                Sprite = sprite,
    //                Sound = sound,
    //                Information = information,
    //                Address = address,
    //                DoorLati = doorLati,
    //                DoorLong = doorLong

    //            };
    //            dictionary[id] = map;
    //        }
    //    }

    //    return dictionary;
    //}
    private Dictionary<TKey, TItem> ParseToDict<TKey, TItem>([NotNull] string path, Func<TItem, TKey> KeySelector)
    {
        string fullPath = path;
#if UNITY_EDITOR
        using (var reader = new StreamReader(fullPath))
#else
        using (var reader = new StringReader(fullPath))
#endif
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return csv.GetRecords<TItem>().ToDictionary(KeySelector);
        }
    }

}
