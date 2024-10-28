using System.Collections;
using System.Collections.Generic;
using Michael.Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/Data")]
public class Data : ScriptableObject
{
    public float MusicVolume;
    public float SfxVolume;
    public int Highscore;
    public int DiscNumber;
    public ItemData CurrentBlade;
    public List<ItemData> Items = new List<ItemData>();
   
}
