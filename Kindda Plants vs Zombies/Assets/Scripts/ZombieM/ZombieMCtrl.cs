using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMCtrl : MonoBehaviour
{
	private Stats s;
	private bool canMove;
	private Animator anim;
	private float nextAttack;
	private GameObject col;

    // Start is called before the first frame update
    void Start()
    {
    	canMove = true;
        s = GetComponent<Stats>();
        anim = GetComponent<Animator>();
    	anim.SetBool("CanMove", true);
    	nextAttack = Time.time + s.AttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove) transform.Translate(Vector3.forward * s.vel * Time.deltaTime);
    }

    public void attack(GameObject col) 
    {
    	if (Time.time > nextAttack) {
				canMove=false;
				anim.SetBool("CanMove", false);
				anim.SetBool("Attack", true);
				this.col = col;
				nextAttack = Time.time + s.AttackSpeed + 100;  	
	    }
	}

    void resetAttck() 
    {
    	anim.SetBool("Attack", false);
    	nextAttack = Time.time + s.AttackSpeed;
    	col.GetComponent<Health>().health -= s.dmg;
		if (col.GetComponent<Health>().health <= 0) canMove = true;
		this.col = null;
    }
}
