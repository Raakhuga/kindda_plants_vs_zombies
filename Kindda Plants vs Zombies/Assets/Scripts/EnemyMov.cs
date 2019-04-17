using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
	private Stats s;
	private bool canMove;

	void Start() 
	{
		s = GetComponent<Stats>();
		canMove = true;
	}
    // Update is called once per frame
    void Update()
    {
        if(canMove)transform.Translate(Vector3.left * s.MS * Time.deltaTime);
    }

    void OnTriggerStay (Collider col) 
    {
		if (col.gameObject.CompareTag("tower"))
		{
			canMove=false;
			col.gameObject.GetComponent<Health>().health -= s.dmg;
			if (col.gameObject.GetComponent<Health>().health <= 0) canMove = true;
		}    	
    }
}
