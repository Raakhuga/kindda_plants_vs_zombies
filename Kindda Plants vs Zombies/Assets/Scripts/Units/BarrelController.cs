using System.Collections;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public float CoolDown;
    public float radius;
    public GameObject anim;

    void Start()
    {
        StartCoroutine(explode());
    }

    IEnumerator explode()
    {
        yield return new WaitForSeconds(CoolDown);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in colliders)
        {
            if (col.gameObject.name == "HitBox")
            {
                if (col.gameObject.transform.parent.GetComponent<HealthBar>() != null)
                    col.gameObject.transform.parent.GetComponent<HealthBar>().destroyBar();
                if (col.gameObject.transform.parent.GetComponent<Death>() != null)
                    col.gameObject.transform.parent.gameObject.GetComponent<Death>().UnitDeath();
            }
        }

        GameObject exp = Instantiate(anim, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
