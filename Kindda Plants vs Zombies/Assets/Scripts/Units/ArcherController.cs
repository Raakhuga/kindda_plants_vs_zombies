using System.Collections;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    private Stats sts;
    private Animator anim;

    private float attackCooldown;

    private Ray vision;
    private Vector3 rayDirection;
    private string target;

    // Only if ranger unit
    public GameObject projectile;
    private GameObject newProjectile;
    private Transform rightHand; // TODO: change to public gameobject (general)

    private bool attacking = false;

    void Start()
    {
        sts = GetComponent<Stats>();
        anim = GetComponent<Animator>();

        attackCooldown = sts.AttackSpeed;

        rightHand = transform.Find("Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand").gameObject.transform;

        rayDirection = Vector3.right;
        target = "enemy";
    }

    void Update()
    {

        // See if there is a target at attack range.
        Vector3 rayOrigin = transform.position;
        rayOrigin.x -= 0.5F;
        rayOrigin.y = 1.5f;
        vision = new Ray(rayOrigin, rayDirection);
        float range = sts.range + 1; // x origin tirat mig tile enrere, volem que arrivi al final del ultim tile.
        Debug.DrawRay(vision.origin, vision.direction * range, Color.green);

        RaycastHit[] hits = Physics.RaycastAll(vision.origin, vision.direction, range);

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.tag == target && !attacking)
            {
                StartCoroutine(startAttack());
                break;
            }
        }

    }

    IEnumerator startAttack()
    {
        attacking = true;
        yield return new WaitForSecondsRealtime(attackCooldown);
        anim.SetBool("Attack", true);
    }

    void ResetAttack()
    {
        attacking = false;
        anim.SetBool("Attack", false);
        newProjectile = Instantiate(projectile, rightHand.position, rightHand.rotation);
        newProjectile.transform.parent = rightHand.gameObject.transform;
        newProjectile.GetComponent<Projectile>().setDmg(sts.dmg);
        newProjectile.GetComponent<Projectile>().setTarget(target);
    }

    void Attack()
    {
        if (newProjectile != null)
        {
            newProjectile.GetComponent<Projectile>().releaseProjectile();
            newProjectile = null;
        }
    }
}
