using System.Collections;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Stats sts;
    private Animator anim;

    private bool canMove;
    private bool attacking;

    private float attackCooldown;
    private string target; // Used in MeleeHit.cs
    private GameObject attackTarget;


    void Start()
    {
        sts = GetComponent<Stats>();
        anim = GetComponent<Animator>();

        canMove = true;
        attackCooldown = sts.AttackSpeed;

        target = "enemy";
        if (tag == "enemy")
        {
            target = "ally";
        }
    }

    void Update()
    {
        if (transform.position.x < -5)
        {
            GameManager.instance.durability.durability -= 10;
            Destroy(transform.gameObject);
        }
        if (attackTarget == null)
        {
            canMove = true;
        }
        else if (!attacking)
        {
            StartCoroutine(startAttack());
        }

        if (canMove)
        {
            anim.SetBool("CanMove", true);
            transform.Translate(Vector3.forward * sts.vel * Time.deltaTime);
        }

    }

    IEnumerator startAttack()
    {
        canMove = false;
        anim.SetBool("CanMove", false);
        attacking = true;
        yield return new WaitForSeconds(attackCooldown);
        anim.SetBool("Attack", true);
    }

    void ResetAttack()
    {
        anim.SetBool("Attack", false);
        if (attackTarget != null)
        {
            attackTarget.GetComponent<Stats>().health -= sts.dmg;
            // Kill unit if health <= 0
            if (attackTarget.GetComponent<Stats>().health <= 0)
            {
                attackTarget.GetComponent<Death>().UnitDeath();
                attackTarget = null;
            }
        }
        attacking = false;
    }

    public void meleeAttack(Collider col)
    {
        if (canMove)
        {
            attackTarget = col.gameObject.transform.parent.gameObject;
            StartCoroutine(startAttack());
        }
    }
}
