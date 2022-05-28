using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileContainer : MonoBehaviour
{
    public GameObject baseTilePrefab;
    public TerrainDef terrainDef;
    public int height = 0;

    private GameObject terrainObject;
    private List<GameObject> baseObjects;

    private void Start()
    {
        //baseObjects = new List<GameObject>();
    }

    public void Setup()
    {
        GenerateBase();
        GenerateTerrain();
    }

    private void GenerateBase()
    {
        baseObjects = new List<GameObject>();

        for (int i = 0; i < height; i++)
        {
            GameObject baseObject = (GameObject)Instantiate(baseTilePrefab, transform, false);
            Debug.Log("baseObjects=" + baseObjects);
            baseObject.transform.Translate(new Vector3(0, i, 0));
            baseObjects.Add(baseObject);
        }
    }

    private void GenerateTerrain()
    {
        terrainObject = (GameObject)Instantiate(terrainDef.prefab, transform, false);
        terrainObject.GetComponent<MeshRenderer>().material = terrainDef.material;
        terrainObject.transform.Translate(new Vector3(0, height, 0));
    }
}
