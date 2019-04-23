using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float vel;

    void Update()
    {
        if (transform.parent == null)
        {
            transform.Translate(Vector3.right * vel * Time.deltaTime);
            if (transform.position.x > 13) Destroy(gameObject);
        }
    }

    public void releaseProjectile()
    {
        transform.parent = null;
    }

    public void setDmg(float dmg)
    {
        transform.Find("HitBox").GetComponent<ProjectileHit>().setDmg(dmg);
    }
}
