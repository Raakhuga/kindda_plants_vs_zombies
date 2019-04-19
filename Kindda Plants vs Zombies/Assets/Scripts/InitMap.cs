using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMap : MonoBehaviour
{
    public GameObject Tile1;
    public GameObject Tile2;
    private GameObject[] map;
    // Start is called before the first frame update
    void Start()
    {
        map = new GameObject[45];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject TyleType = Tile1;
                if ((i + j) % 2 == 0) TyleType = Tile2;
                map[i * 5 + j] = Instantiate(TyleType, new Vector3(i, 0.0f, j), Quaternion.identity) as GameObject;
                map[i * 5 + j].tag = "Tile";
                map[i * 5 + j].name = "tile " + i + " " + j;
            }
        }
    }
}
