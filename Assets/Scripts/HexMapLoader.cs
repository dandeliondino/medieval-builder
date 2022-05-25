using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMapLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public HexMap deserialize()
    {

        string path = Application.persistentDataPath + "/" + "hexmap.xml";

        HexMap hexMap = XMLOp.Deserialize<HexMap>(path);
        //Debug.Log("HexMap loaded: " + hexmap.xsize + ", " + hexmap.zsize);
        return hexMap;


        //Debug.Log(Application.persistentDataPath);
    }
}
