using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TileScriptableObject", order = 1)]
public class TileType : ScriptableObject
{
    public string displayName;
    public GameObject prefab;
    public Material material;
    
}
