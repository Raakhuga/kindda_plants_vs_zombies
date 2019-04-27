using System.Collections;
using UnityEngine;

public class PriestController : MonoBehaviour
{
    public float coolDown = 2;
    public float currentGold = 0;
    public float addGold = 5;
    public float maxGold = 25;

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
        }
    }

    IEnumerator generateGold()
    {
        generating = true;
        yield return new WaitForSecondsRealtime(coolDown);
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
    }
}
