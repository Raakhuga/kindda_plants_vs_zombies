using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{
	public GameObject enemy;
	public float CoolDown;
	private float nextEnemy;
    // Start is called before the first frame update
    void Start()
    {
        nextEnemy = Time.time + CoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextEnemy) {

        	GameObject aux = Instantiate(enemy, new Vector3(6F, 0.59F, Random.Range(-2,2)), transform.rotation);
        	aux.transform.Rotate(0F, -90F, 0);
        	nextEnemy = Time.time + CoolDown;
        }
    }
}
