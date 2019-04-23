using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    private float dmg;

    void OnTriggerEnter(Collider col)
    {
        GameObject weapon = transform.parent.gameObject;
        if (col.gameObject.CompareTag("enemy") && weapon.transform.position.x < 9)
        {
            col.gameObject.transform.parent.gameObject.GetComponent<Stats>().health -= dmg;
            Destroy(transform.parent.gameObject);
        }
    }

    public void setDmg(float dmg)
    {
        this.dmg = dmg;
    }
}
