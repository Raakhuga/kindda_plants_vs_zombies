using System.Collections;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public float resources;
    public float startGenerating;
    public float coolDown;
    public float generator;

    void Start()
    {
        StartCoroutine(addResources());
    }

    IEnumerator addResources()
    {
        yield return new WaitForSecondsRealtime(startGenerating);
        while (true)
        {
            resources += generator;
            yield return new WaitForSecondsRealtime(coolDown);
        }
    }
}
