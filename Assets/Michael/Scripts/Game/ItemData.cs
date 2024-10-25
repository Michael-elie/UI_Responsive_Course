using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public int BladeCost;
    public string BladeName;
    public Image BladeIcon;
    public Material BladeMaterial;
    
}
