using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    // Start is called before the first frame update
    private float dmg;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider col)
    {
    	if (col.gameObject.CompareTag("enemy"))
    	{
    		col.gameObject.transform.parent.gameObject.GetComponent<Health>().health -= dmg;
    		Destroy(transform.parent.gameObject);
    	}    	
    }

    public void setDmg(float dmg)
    {
    	this.dmg = dmg;
    }
}
