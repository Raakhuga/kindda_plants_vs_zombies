using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinController : MonoBehaviour
{
    public AudioClip Hit;
    public AudioClip Death;
    public AudioSource Source;
	
    private float cooldownAttack;
	private Animator anim;
	private Stats sts;
	private GameObject attackTarget;
	private bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sts = GetComponent<Stats>();
        cooldownAttack = sts.AttackSpeed;
        attacking = false;
        Source.clip = Hit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetAttack() {
        Source.Play();
    	anim.SetBool("Attack", false);
    	if (attackTarget != null)
        {
            attackTarget.GetComponent<Stats>().health -= sts.dmg;
            //HealthBar = attackTarget.GetComponent<HealthBar>()
            attackTarget.GetComponent<HealthBar>().actHBar();
            // Kill unit if health <= 0
            if (attackTarget.GetComponent<Stats>().health <= 0)
            {
                attackTarget.GetComponent<HealthBar>().destroyBar();
                attackTarget.GetComponent<Death>().UnitDeath();
                attackTarget = null;
            }
        }
    }

    IEnumerator startAttack() {
    	anim.SetBool("Attack", true);
    	attacking = true;
    	yield return new WaitForSeconds(cooldownAttack);
        attacking = false;
    }

    public void meleeAttack(Collider col)
    {
        if (!attacking)
        {
            attackTarget = col.gameObject.transform.parent.gameObject;
            StartCoroutine(startAttack());
        }
    }

    public void  deathSound() 
    {
        Source.clip = Death;
        Source.Play();
    } 
}
