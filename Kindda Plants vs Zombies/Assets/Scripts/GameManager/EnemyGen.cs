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

    public void initWaves()
    {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy()
    {
        yield return new WaitForSecondsRealtime(timeStartWave);
        int lastColVal = -1;
        ncols = GameManager.instance.board.ncols;
        startingCol = GameManager.instance.board.startingCol;
        for (int w = 0; w < numWaves; w++)
        {
            Debug.Log("Num Wave: " + w);
            for (int i = 0; i < enemiesWave; i++)
            {
                int newCol = Random.Range(0, ncols) + startingCol;
                while (ncols > 2 && newCol == lastColVal)
                {
                    newCol = Random.Range(0, ncols) + startingCol;
                }
                lastColVal = newCol;
                GameObject aux = Instantiate(enemy, new Vector3(Random.Range(13, 17), 0.06F, newCol), transform.rotation);
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
