using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Grid grid = GetComponent<Grid>();
        //Debug.Log("Cell size: " + grid.cellSize);
        //Debug.Log("World to cell: " + grid.WorldToCell(new Vector3(3, 0, 1.75f)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
