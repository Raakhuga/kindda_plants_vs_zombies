using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{
    public GameObject enemy;
    public float StartWave;
    public float CoolDown;

    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy()
    {
        yield return new WaitForSecondsRealtime(StartWave);
        while (true)
        {
            GameObject aux = Instantiate(enemy, new Vector3(Random.Range(12, 14), 0.06F, Random.Range(0, 5)), transform.rotation);
            aux.transform.Rotate(0F, -90F, 0);
            aux.GetComponent<Stats>().vel += Random.Range(-0.1f, 0.1f); // Little random enemy velocity
            yield return new WaitForSecondsRealtime(CoolDown);
        }
    }
}