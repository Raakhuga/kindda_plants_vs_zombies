using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMov : MonoBehaviour
{
	private Stats s;
	private Vector3 PosIni;

	void Start()
	{
		s = GetComponent<Stats>();
		PosIni = transform.position;
	}
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * s.MS * Time.deltaTime);
        if (Vector3.Distance(transform.position, PosIni) > 13) Destroy(gameObject);
    }

    void OnTriggerEnter (Collider col)
    {
    	if (col.gameObject.CompareTag("enemy"))
    	{
    		col.gameObject.GetComponent<Health>().health -= s.dmg;
    		Destroy(gameObject);
    	}    	
    }
}
