using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileContainer : MonoBehaviour
{
    public GameObject baseTilePrefab;
    public TerrainDef terrainDef;
    public int height = 0;
    public float heightUnit = 0.5f;
    public float baseHeight = 0.5f;

    public int maxHeight = 6;
    public int minHeight = 0;

    private GameObject terrainObject;
    private List<GameObject> baseObjects;

    private void Awake()
    {
        baseObjects = new List<GameObject>();
    }

    public void Setup()
    {
        GenerateTerrain();
        GenerateElevation();
    }

    private void GenerateBase()
    {
        foreach (var obj in baseObjects)
        {
            Destroy(obj);
        }

        baseObjects.Clear();

        int baseObjectsNeeded = Mathf.RoundToInt((height * heightUnit) / baseHeight);
        
        for (int i = 0; i < baseObjectsNeeded; i++)
        {
            GameObject baseObject = (GameObject)Instantiate(baseTilePrefab, transform, false);
            baseObject.transform.Translate(new Vector3(0, i * baseHeight, 0));
            baseObjects.Add(baseObject);
        }
    }

    private void GenerateTerrain()
    {
        terrainObject = (GameObject)Instantiate(terrainDef.prefab, transform, false);
        terrainObject.GetComponent<MeshRenderer>().material = terrainDef.material;
        terrainObject.GetComponent<TerrainTile>().hexTileContainer = this;
    }

    private void GenerateElevation()
    {
        GenerateBase();
        float heightY = height * heightUnit;

        terrainObject.transform.Translate(new Vector3(0, heightY - terrainObject.transform.position.y, 0));
    }

    public void RaiseTerrain()
    {
        SetTerrainHeight(height + 1);
    }

    public void LowerTerrain()
    {
        SetTerrainHeight(height - 1);
    }

    public void SetTerrainHeight(int newHeight)
    {
        int clampedHeight = Mathf.Clamp(newHeight, minHeight, maxHeight);
        if (height == clampedHeight)
        {
            return;
        }

        height = clampedHeight;
        GenerateElevation();
    }

}
