using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
	public float CoolDown;
	public float radius;
	public GameObject anim;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(explode());
    }

    IEnumerator explode() 
    {
    	yield return new WaitForSeconds(CoolDown);
    	GameObject exp = Instantiate(anim, transform.position, transform.rotation);

    	Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

    	foreach (Collider col in colliders) 
    	{
    		if (col.gameObject.name == "HitBox") col.gameObject.transform.parent.gameObject.GetComponent<Death>().UnitDeath();
    	}

    	Destroy(gameObject);
    }
}
