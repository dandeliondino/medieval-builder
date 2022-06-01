using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class HexMapSaver : MonoBehaviour
{
    //private TileManager tilemanager;

    private void Start()
    {
        //tilemanager = TileManager.instance;
    }

    public void serialize()
    {
        //GameObject[] hexobjects = GameObject.FindGameObjectsWithTag("Hex");
        //HexData[] hexdatas = new HexData[hexobjects.Length];

        //for (int i = 0; i < hexobjects.Length; i++)
        //{
        //    Hex hex = hexobjects[i].GetComponent<Hex>();
        //    hexdatas[i] = new HexData
        //    {
        //        x = hex.xcoord,
        //        z = hex.zcoord,
        //        height = hex.height,
        //        tileType = hex.getTileTypeName(),
        //    };
        //}

        //HexMap hexmap = new HexMap();
        //hexmap.xsize = tilemanager.xTiles;
        //hexmap.zsize = tilemanager.zTiles;
        //hexmap.hexes = hexdatas;

        //string path = Application.persistentDataPath + "/" + "hexmap.xml";

        //XMLOp.Serialize(hexmap, path);
    }
}
