using System.Collections;
using UnityEngine;

public class ResourcesController : MonoBehaviour
{
    public float resources;

    public float startGenerating = 10;
    public float coolDown = 5;
    public float addGold = 3;

    void Start()
    {
        StartCoroutine(addResources());
    }

    IEnumerator addResources()
    {
        yield return new WaitForSeconds(startGenerating);
        while (true)
        {
            resources += addGold;
            yield return new WaitForSeconds(coolDown);
        }
    }
}
