using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    // Start is called before the first frame update
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
    		col.gameObject.GetComponent<Health>().health -= 100;
    		Destroy(gameObject);
    	}    	
    }
}
