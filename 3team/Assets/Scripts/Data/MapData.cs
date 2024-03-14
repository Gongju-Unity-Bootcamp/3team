using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapID
{
    none,
    Jungmak = 1000,
    Geumhak = 2000,
    Gongsan = 3000,
    Shingwan = 4000,
    Beonyeong = 4500,
    Namhyeon = 5000,
    Yongpung = 5500,
    Ungjin = 6000,
    Geumsung = 7500,
    Sagok = 8000,
    Yonghak = 8500,
    Wuseong = 9000,
    Okgol = 9500
}

public class MapData
{
    public MapID Id {  get; set; }
    public string Name { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public string Type { get; set; }
    public string Sprite { get; set; }
    public string Sound { get; set; }
    public string Address { get; set; }
    public string Information { get; set; }

}
