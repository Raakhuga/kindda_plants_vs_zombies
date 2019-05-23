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

    private int ncols;
    private int startingCol;

    private Transform enemies;

    public void initWaves()
    {
        StopAllCoroutines();
        enemies = new GameObject("enemies").transform;

        StartCoroutine(spawnEnemy());
    }

    public void stopCoroutines()
    {
        StopAllCoroutines();
    }

    IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(timeStartWave);
        int lastColVal = -1;
        ncols = GameManager.instance.board.ncols;
        startingCol = GameManager.instance.board.startingCol;
        for (int w = 0; w < numWaves; w++)
        {
            Debug.Log("Num Wave: " + w + " " + Time.time);
            for (int i = 0; i < enemiesWave; i++)
            {
                yield return new WaitForSeconds(unitCoolDown);
                Debug.Log("New enemy unit: " + i + " " + Time.time);
                int newCol = Random.Range(0, ncols) + startingCol;
                while (ncols > 2 && newCol == lastColVal)
                {
                    newCol = Random.Range(0, ncols) + startingCol;
                }
                lastColVal = newCol;
                GameObject aux = Instantiate(enemy, new Vector3(Random.Range(14, 16), 0.06F, newCol), transform.rotation, enemies);
                aux.transform.Rotate(0F, -90F, 0);
                aux.GetComponent<Stats>().vel += Random.Range(-0.1f, 0.1f); // Little random enemy velocity
            }
            unitCoolDown -= 3;
            unitCoolDown = unitCoolDown > 2 ? unitCoolDown : 2;
            enemiesWave = (int)(enemiesWave * 2f);
            yield return new WaitForSeconds(waveCoolDown);
        }
    }
}
