using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{
    public GameObject enemy;
    public float CoolDown;

    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy()
    {
        yield return new WaitForSecondsRealtime(10);
        while (true)
        {
            GameObject aux = Instantiate(enemy, new Vector3(12F, 0.06F, Random.Range(0, 5)), transform.rotation);
            aux.transform.Rotate(0F, -90F, 0);
            yield return new WaitForSecondsRealtime(CoolDown);
        }
    }
}