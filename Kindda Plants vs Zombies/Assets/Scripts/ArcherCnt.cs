using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherCnt : MonoBehaviour
{
	private Animator anim;
	private ArcherStats asts;
	private float time;
    // Start is called before the first frame update
    void Start()
    {
        asts = GetComponent<ArcherStats>();
        anim = GetComponent<Animator>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0) {
        	anim.SetBool("Shoot", true);
        	time = asts.AttackSpeed;
        }
        //time -= 1/60.f;
    }

    void ResetShoot()
    {
    	anim.SetBool("Shoot", false);
    }
}
