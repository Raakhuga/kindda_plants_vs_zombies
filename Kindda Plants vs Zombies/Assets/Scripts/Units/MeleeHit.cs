using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    private string target;
    void Start()
    {
        target = transform.parent.gameObject.tag == "ally" ? "enemy" : "ally";
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject != null && col.gameObject.CompareTag(target))
        {
            if (transform.parent.gameObject.GetComponent<ZombieController>() != null)
            {
                transform.parent.gameObject.GetComponent<ZombieController>().meleeAttack(col);
            }
        }
    }
}
