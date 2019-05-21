using System.Collections;
using UnityEngine;

public class PriestController : MonoBehaviour
{
    public float coolDown = 2;
    public float currentGold = 0;
    public float addGold = 5;
    public float maxGold = 25;
    public GameObject MoneyBag;

    private GameObject MB;
    private int ratio;
    private Stats sts;
    private Animator anim;
    private bool generating = false;

    void Start()
    {
        sts = GetComponent<Stats>();
        anim = GetComponent<Animator>();
        anim.SetBool("FullGold", false);

        ratio = (int)(maxGold / addGold);
        ratio = ratio < 2 ? 2 : ratio;

        Vector3 pos = new Vector3(transform.position.x, 1.75f, transform.position.z);
        //Vector3 camPos = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        MB = Instantiate(MoneyBag, pos, transform.rotation);
        MB.transform.parent = transform;
        //MB.transform.LookAt(camPos);
        //MB.transform.Rotate(-90, 0, 0);
        MB.transform.localScale *=0.75f;
        MB.SetActive(false);
    }

    void Update()
    {
        if (currentGold < maxGold)
        {
            anim.SetBool("FullGold", false);
            if (!generating)
            {
                StartCoroutine(generateGold());
            }
        }
        else
        {
            anim.SetBool("FullGold", true);
            if(!MB.activeSelf) MB.SetActive(true);

        }
    }

    IEnumerator generateGold()
    {
        generating = true;
        yield return new WaitForSeconds(coolDown);
        currentGold += addGold;
        currentGold = currentGold < maxGold ? currentGold : maxGold;
        generating = false;
    }

    public void takeGold()
    {
        GameManager.instance.resources.resources += currentGold;
        currentGold = 0;
        addGold += 1;
        maxGold = addGold * ratio;
        MB.SetActive(false);
    }
}
