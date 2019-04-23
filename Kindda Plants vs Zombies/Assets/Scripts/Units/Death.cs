using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
	private Stats sts;
	private Resources r;
    // Start is called before the first frame update
    void Start()
    {
        sts = GetComponent<Stats>();
        r = GameObject.Find("GameController").GetComponent<Resources>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sts.health <= 0) {
            r.resources += sts.gold;
            Destroy(gameObject);
        }
    }
}