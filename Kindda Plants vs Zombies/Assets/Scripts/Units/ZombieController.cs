using System.Collections;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Stats sts;
    private Animator anim;

    private bool canMove;

    private float attackCooldown;
    private string target;
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
        if (attackTarget == null)
        {
            canMove = true;
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
        yield return new WaitForSecondsRealtime(attackCooldown);
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
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject != null && col.gameObject.CompareTag(target))
        {
            meleeAttack(col);
        }
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
