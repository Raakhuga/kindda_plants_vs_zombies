using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZombieM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay (Collider col) 
    {
		if (col.gameObject.CompareTag("ally"))
		{
			transform.parent.gameObject.GetComponent<ZombieMCtrl>().attack(col.gameObject.transform.parent.gameObject);
		}    	
    }
}
