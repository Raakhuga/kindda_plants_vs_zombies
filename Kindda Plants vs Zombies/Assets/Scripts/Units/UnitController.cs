using System.Collections;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private Stats sts;
    private Animator anim;

    private bool movileUnit; // if Stats vel > 0 true
    private bool canMove;

    private float attackCooldown;

    private Ray vision;
    private Vector3 rayDirection;
    private string target;

    // Only if ranger unit
    public bool ranger;
    public GameObject projectile;
    private GameObject newProjectile;
    private Transform rightHand; // TODO: change to public gameobject (general)

    // Only if non ranger unit
    private GameObject attackTarget;


    void Start()
    {
        sts = GetComponent<Stats>();
        anim = GetComponent<Animator>();

        movileUnit = sts.vel > 0;
        canMove = true;
        attackCooldown = sts.AttackSpeed;

        rightHand = transform.Find("Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand").gameObject.transform;

        rayDirection = Vector3.right; // ally
        target = "enemy";
        if (tag == "enemy")
        {
            rayDirection = -rayDirection;
            target = "ally";
        }
    }

    void Update()
    {
        if (ranger)
        {
            // See if there is a target at attack range.
            Vector3 rayOrigin = transform.position;
            rayOrigin.x -= 0.5F;
            rayOrigin.y = 1.5f;
            vision = new Ray(rayOrigin, rayDirection);
            Debug.DrawRay(vision.origin, vision.direction * sts.range, Color.green);

            RaycastHit[] hits = Physics.RaycastAll(vision.origin, vision.direction, sts.range);

            // Search if we hit a target. If not and movileUnit, canMove
            if (movileUnit)
            {
                canMove = true;
            }
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.tag == target)
                {
                    canMove = false;
                    StartCoroutine(startAttack());
                    break;
                }
            }
        }

        if (attackTarget == null && movileUnit)
        {
            canMove = true;
            anim.SetBool("CanMove", true);
        }

        if (movileUnit && canMove)
        {
            anim.SetBool("CanMove", true);
            transform.Translate(Vector3.forward * sts.vel * Time.deltaTime);
        }

    }

    // melee units
    public void meleeAttack(Collider col)
    {
        if (!ranger)
        {
            attackTarget = col.gameObject.transform.parent.gameObject;
            StartCoroutine(startAttack());
        }
        else
        {
            attackTarget = null;
        }
    }

    IEnumerator startAttack()
    {
        if (movileUnit)
        {
            canMove = false;
            anim.SetBool("CanMove", false);
        }
        anim.SetBool("Attack", true);
        yield return new WaitForSecondsRealtime(attackCooldown);
    }

    void ResetAttack()
    {
        anim.SetBool("Attack", false);
        if (ranger)
        {
            newProjectile = Instantiate(projectile, rightHand.position, rightHand.rotation);
            newProjectile.transform.parent = rightHand.gameObject.transform;
            newProjectile.GetComponent<Projectile>().setDmg(sts.dmg);
            newProjectile.GetComponent<Projectile>().setTarget(target);
        }
        else // melee
        {
            if (attackTarget != null)
            {
                attackTarget.GetComponent<Stats>().health -= sts.dmg;
            }
            // Kill unit if health <= 0
            if (attackTarget != null && attackTarget.GetComponent<Stats>().health <= 0)
            {
                attackTarget.GetComponent<Death>().UnitDeath();
                attackTarget = null;
            }
        }
    }

    // Only for rangers
    void Attack()
    {
        if (ranger && newProjectile != null)
        {
            newProjectile.GetComponent<Projectile>().releaseProjectile();
            newProjectile = null;
        }
    }

    public void setCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
