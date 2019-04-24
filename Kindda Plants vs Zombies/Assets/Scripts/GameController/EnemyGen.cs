using System.Collections;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{
    public GameObject enemy;
    public int numWaves;
    public int enemiesWave;
    public float timeStartWave;
    public float waveCoolDown;
    public float unitCoolDown;

    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy()
    {
        yield return new WaitForSecondsRealtime(timeStartWave);
        int lastColVal = -1;
        for (int w = 0; w < numWaves; w++)
        {
            Debug.Log("Num Wave: " + w);
            for (int i = 0; i < enemiesWave; i++)
            {
                int newCol = Random.Range(0, 5);
                while (newCol == lastColVal)
                {
                    newCol = Random.Range(0, 5);
                }
                lastColVal = newCol;
                GameObject aux = Instantiate(enemy, new Vector3(Random.Range(12, 15), 0.06F, newCol), transform.rotation);
                aux.transform.Rotate(0F, -90F, 0);
                aux.GetComponent<Stats>().vel += Random.Range(-0.1f, 0.1f); // Little random enemy velocity
                yield return new WaitForSecondsRealtime(unitCoolDown);
            }
            if (unitCoolDown > 2)
            {
                unitCoolDown -= 3;
            }
            else
            {
                unitCoolDown = 2;
            }
            enemiesWave = (int)(enemiesWave * 2f);
            yield return new WaitForSecondsRealtime(waveCoolDown);
        }
    }
}
