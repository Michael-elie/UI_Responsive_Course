using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data")]
public class Data : ScriptableObject
{
    public float MusicVolume;
    public float SfxVolume;
    public int Highscore;
    public int DiscNumber;
    public Material BladeMaterial;
}
