using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherCnt : MonoBehaviour
{
	private Animator anim;
	private ArcherStats asts;
	private float nextShoot;
	public GameObject Arrow;
	private GameObject newArrow;

    // Start is called before the first frame update
    void Start()
    {
        asts = GetComponent<ArcherStats>();
        anim = GetComponent<Animator>();
        nextShoot = Time.time + asts.AttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShoot) {
        	anim.SetBool("Shoot", true);
        	nextShoot = Time.time + asts.AttackSpeed + 100;
        }
    }

    void ResetShoot()
    {
    	anim.SetBool("Shoot", false);
    	Transform t = transform.Find("Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand").gameObject.transform;
    	newArrow = Instantiate(Arrow, t.position, t.rotation);
    	newArrow.transform.parent = t.gameObject.transform;
    	newArrow.GetComponent<ArrowScrip>().setVel(asts.vel);
    	newArrow.GetComponent<ArrowScrip>().setDmg(asts.dmg);
    }

    void Shoot()
    {
    	nextShoot = Time.time + asts.AttackSpeed;
    	newArrow.GetComponent<ArrowScrip>().releaseArrow();
   } 

}
