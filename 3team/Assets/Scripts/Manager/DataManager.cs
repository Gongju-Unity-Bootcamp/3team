using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Linq;
using System.IO;
using System.Globalization;
using System;
using System.Diagnostics.CodeAnalysis;

public class DataManager : MonoBehaviour
{

    public Dictionary<MapID, MapData> Map = new Dictionary<MapID, MapData>();
    public Dictionary<SoundID, SoundData> Sound = new Dictionary<SoundID, SoundData>();

    public void Init()
    {
#if UNITY_EDITOR
        Map = ParseToDict<MapID, MapData>("Assets/Resources/Data/MapData.csv", data => data.Id);
        Sound = ParseToDict<SoundID, SoundData>("Assets/Resources/Data/Sound.csv", data => data.Id);
#else
        TextAsset mapCSV = Resources.Load<TextAsset>("Data/Map");
        Map = ParseToDict<MapID, MapData>(MapCSV.text, data => data.Id);

#endif

    }

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
