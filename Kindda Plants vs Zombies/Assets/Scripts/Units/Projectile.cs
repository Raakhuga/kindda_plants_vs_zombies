using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float vel;
    private string target;

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

    public void setTarget(string target)
    {
        transform.Find("HitBox").GetComponent<ProjectileHit>().setTarget(target);
    }
}
