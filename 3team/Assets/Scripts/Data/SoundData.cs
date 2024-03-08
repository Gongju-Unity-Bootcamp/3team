using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundID
{
    None = 0
}
public class SoundData
{
    public SoundID Id { get; set; }
    public string Name { get; set; }
    public float Volume { get; set; }
}
