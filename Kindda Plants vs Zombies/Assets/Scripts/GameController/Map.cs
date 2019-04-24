using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject Tile1, Tile2;
    public int nrows, ncols;
    private GameObject[] map;
    // Start is called before the first frame update
    void Start()
    {
        map = new GameObject[nrows*ncols];
        for (int i = 0; i < nrows; i++)
        {
            for (int j = 0; j < ncols; j++)
            {
                GameObject TyleType = Tile1;
                if ((i + j) % 2 == 0) TyleType = Tile2;
                map[i * 5 + j] = Instantiate(TyleType, new Vector3(i, 0.0f, j), Quaternion.identity) as GameObject;
                map[i * 5 + j].name = "tile " + i + " " + j;
                TileParams tileParamsTmp = map[i * 5 + j].GetComponent<TileParams>();
                tileParamsTmp.x = i;
                tileParamsTmp.y = j;
                tileParamsTmp.activeUnit = false;
            }
        }
    }
}
