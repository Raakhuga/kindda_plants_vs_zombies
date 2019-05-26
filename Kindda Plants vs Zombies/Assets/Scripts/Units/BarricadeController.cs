using UnityEngine;

public class BarricadeController : MonoBehaviour
{
    private Stats sts;
    public GameObject anim;

    void Start()
    {
        sts = GetComponent<Stats>();
    }

    public void Die()
    {
        GameObject aux = Instantiate(anim, transform.position, transform.rotation);
        aux.transform.parent = transform;
    }
}
