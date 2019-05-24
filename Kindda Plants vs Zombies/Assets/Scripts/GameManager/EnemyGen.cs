using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyGen : MonoBehaviour
{
    public GameObject Zombie;
    public GameObject Tank;
    public GameObject Dragon;

    public int numEnemiesWave = 0;

    private int ncols;
    private int startingCol;

    private Transform enemies;

    private bool started = false;

    public void stopCoroutines()
    {
        StopAllCoroutines();
    }

    public void startLvlWaveCoroutine()
    {
        switch (GameManager.instance.currentLvl)
        {
            case 1:
                Debug.Log("Wave Lvl 1");
                StartCoroutine(wavesLvl1());
                break;
            case 2:
                Debug.Log("Wave Lvl 2");
                StartCoroutine(wavesLvl2());
                break;
            case 3:
                Debug.Log("Wave Lvl 3");
                break;
            case 4:
                Debug.Log("Wave Lvl 4");
                break;
            case 5:
                Debug.Log("Wave Lvl 5");
                break;
            default:
                Debug.Log("Wave default");
                break;
        }
        started = true;
    }

    public void initWaves()
    {
        StopAllCoroutines();
        enemies = new GameObject("enemies").transform;
        started = false;
        startLvlWaveCoroutine();
    }

    public void Update()
    {
        if(GameManager.instance.currentLvl != 0 && numEnemiesWave == 0 && started)
        {
            started = false;
            GameManager.instance.currentLvl++;
            Debug.Log("Level " + GameManager.instance.currentLvl);
            SceneManager.LoadScene("Level");
        }
    }

    IEnumerator wavesLvl1()
    {
        ncols = GameManager.instance.board.ncols;
        startingCol = GameManager.instance.board.startingCol;
        float startCoolDown = 15;
        float unitCoolDown = 5;
        float waveCoolDown = 10;
        int numWaves = 5;
        int numUnits = 10;
        int lastColVal = -1;

        numEnemiesWave = numUnits * numWaves;
        started = true;

        yield return new WaitForSeconds(startCoolDown);
        for (int w = 0; w < numWaves; w++)
        {
            Debug.Log("Num Wave: " + w + " " + Time.time);
            for (int i = 0; i < numUnits; i++)
            {
                Debug.Log("New enemy unit: " + i + " " + Time.time);
                int newCol = Random.Range(0, ncols) + startingCol;
                while (ncols > 2 && newCol == lastColVal)
                {
                    newCol = Random.Range(0, ncols) + startingCol;
                }
                lastColVal = newCol;
                instantiateZombie(newCol);
                yield return new WaitForSeconds(unitCoolDown);
            }
            yield return new WaitForSeconds(waveCoolDown);
        }
    }

    IEnumerator wavesLvl2()
    {
        started = true;
        ncols = GameManager.instance.board.ncols;
        startingCol = GameManager.instance.board.startingCol;
        float startCoolDown = 15;
        float unitCoolDown = 4;
        float waveCoolDown = 10;
        int numWaves = 5;
        int numUnits = 10;
        int lastColVal = -1;

        yield return new WaitForSeconds(startCoolDown);
        for (int w = 0; w < numWaves; w++)
        {
            Debug.Log("Num Wave: " + w + " " + Time.time);
            for (int i = 0; i < numUnits; i++)
            {
                Debug.Log("New enemy unit: " + i + " " + Time.time);
                int newCol = Random.Range(0, ncols) + startingCol;
                while (ncols > 2 && newCol == lastColVal)
                {
                    newCol = Random.Range(0, ncols) + startingCol;
                }
                lastColVal = newCol;
                int unitGenerated = Random.Range(0, 100);
                if (unitGenerated < 80)
                {
                    instantiateZombie(newCol);
                }
                else
                {
                    instantiateTank(newCol);
                }
                yield return new WaitForSeconds(unitCoolDown);
            }
            yield return new WaitForSeconds(waveCoolDown);
        }
    }

    private void instantiateZombie(int col)
    {
        Debug.Log("Zombie instance");
        GameObject aux = Instantiate(Zombie, new Vector3(Random.Range(12, 14), 0.06F, col), transform.rotation, enemies);
        aux.transform.Rotate(0F, -90F, 0);
        aux.GetComponent<Stats>().vel += Random.Range(-0.25f, 0.05f); // Little random enemy velocity
    }

    private void instantiateTank(int col)
    {
        Debug.Log("Tank instance");
        GameObject aux = Instantiate(Tank, new Vector3(Random.Range(14, 16), 0.06F, col), transform.rotation, enemies);
        aux.transform.Rotate(0F, -90F, 0);
        aux.GetComponent<Stats>().vel += Random.Range(-0.25f, 0.05f); // Little random enemy velocity
    }
}
