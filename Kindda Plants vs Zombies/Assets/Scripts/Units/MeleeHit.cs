using System.Collections;
using System.Collections.Generic;
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
            transform.parent.gameObject.GetComponent<UnitController>().meleeAttack(col);
        }
    }
}
