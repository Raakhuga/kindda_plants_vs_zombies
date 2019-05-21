using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurabilityController : MonoBehaviour
{
    public float durability = 100;
    public float maxDurability = 100;

    public float startGenerating = 10;
    public float coolDown = 5;
    public float restoreHealth = 3;

    void Start()
    {
        StartCoroutine(addResources());
    }

    IEnumerator addResources()
    {
        yield return new WaitForSeconds(startGenerating);
        while (true)
        {
            durability += restoreHealth;
            durability = durability < maxDurability ? durability : maxDurability;
            yield return new WaitForSeconds(coolDown);
        }
    }
}
