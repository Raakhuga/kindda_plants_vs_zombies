using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    private float dmg;
    private string target;

    void OnTriggerEnter(Collider col)
    {
        GameObject weapon = transform.parent.gameObject;
        if (col.gameObject.CompareTag(target)  && weapon.transform.position.x < 9)
        {
            GameObject attktarget = col.gameObject.transform.parent.gameObject;
            attktarget.GetComponent<Stats>().health -= dmg;
            attktarget.GetComponent<HealthBar>().actHBar();

            // Kill unit if health <= 0
            if (attktarget.GetComponent<Stats>().health <= 0)
            {
                attktarget.GetComponent<HealthBar>().destroyBar();
                attktarget.GetComponent<Death>().UnitDeath();
            }

            Destroy(transform.parent.gameObject);
        }
    }

    public void setDmg(float dmg)
    {
        this.dmg = dmg;
    }

    public void setTarget(string target)
    {
        this.target = target;
    }
}
