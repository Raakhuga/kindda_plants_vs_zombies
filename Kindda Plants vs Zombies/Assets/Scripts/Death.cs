using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
	private Health h;
	private Res r;
	private Stats s;
    // Start is called before the first frame update
    void Start()
    {
        h = GetComponent<Health>();
        r = GameObject.Find("GameController").GetComponent<Res>();
        if (CompareTag("enemy")) s = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (h.health <= 0) Destroy(gameObject);
        if (CompareTag("enemy")) r.resources += s.worth;//dinerico
    }
}