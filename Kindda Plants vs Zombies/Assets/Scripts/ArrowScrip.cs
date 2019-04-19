using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScrip : MonoBehaviour
{
	private Vector3 PosIni;
    // Start is called before the first frame update
    void Start()
    {
    	/*
    	parentBone = GameObject.Find("LeftHandArrow");
        transform.parent = parentBone.transform;
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent == null) {
        	transform.Translate(Vector3.right * 4 * Time.deltaTime);
        	if (Vector3.Distance(transform.position, PosIni) > 13) Destroy(gameObject);
        }
    }

    public void releaseArrow()
    {
    	transform.parent = null;
    	PosIni = transform.position;
    }

    void OnTriggerEnter (Collider col)
    {
    	if (col.gameObject.CompareTag("enemy"))
    	{
    		col.gameObject.GetComponent<Health>().health -= 100;
    		Destroy(gameObject);
    	}    	
    }
}
