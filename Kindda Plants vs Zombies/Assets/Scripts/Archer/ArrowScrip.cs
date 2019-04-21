using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScrip : MonoBehaviour
{
	private Vector3 PosIni;
	private float  vel;
    // Update is called once per frame
    void Update()
    {
        if(transform.parent == null) {
        	transform.Translate(Vector3.right * vel * Time.deltaTime);
        	if (Vector3.Distance(transform.position, PosIni) > 13) Destroy(gameObject);
        }
    }

    public void releaseArrow()
    {
    	transform.parent = null;
    	PosIni = transform.position;
    }

    public void setVel(float vel) 
    {
    	this.vel = vel;
    }

    public void setDmg(float dmg)
    {
    	transform.Find("HitBox").GetComponent<Hit>().setDmg(dmg);
    }
}
