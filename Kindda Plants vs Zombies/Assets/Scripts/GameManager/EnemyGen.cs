using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyGen : MonoBehaviour
{
    public GameObject Zombie;
    public GameObject Tank;
    public GameObject Dragon;

    public int numEnemiesWave = 0;

    public int tmpEn = 0;

    private int ncols;
    private int startingCol;

    private Transform enemies;

    public bool started = false;

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
                StartCoroutine(wavesLvl3());
                break;
            case 4:
                Debug.Log("Wave Lvl 4");
                StartCoroutine(wavesLvl1());
                break;
            case 5:
                Debug.Log("Wave Lvl 5");
                StartCoroutine(wavesLvl2());
                break;
            default:
                Debug.Log("Wave default");
                StartCoroutine(wavesLvl3());
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
        if (GameManager.instance.currentLvl != 0 && numEnemiesWave == 0 && started)
        {
            started = false;
            StartCoroutine(levelSucced());
        }
    }

    IEnumerator levelSucced()
    {
        started = false;
        yield return new WaitForSeconds(6);
        GameManager.instance.currentLvl = (GameManager.instance.currentLvl + 1) % 7;
        Debug.Log("Level " + GameManager.instance.currentLvl);
        if (GameManager.instance.currentLvl > 0)
        {
            SceneManager.LoadScene("Level");
        }
        else
        {
            SceneManager.LoadScene("MenuStartGame");
        }
    }

    IEnumerator waveProva()
    {
        ncols = GameManager.instance.board.ncols;
        startingCol = GameManager.instance.board.startingCol;
        float startCoolDown = 1;
        float unitCoolDown = 3;
        float waveCoolDown = 3;
        int numWaves = 2;
        int numUnits = 3;
        tmpEn = 0;
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
                if (tmpEn < numWaves * numUnits)
                {
                    instantiateMonster(newCol, Zombie);
                }
                tmpEn++;
                yield return new WaitForSeconds(unitCoolDown);
            }
            yield return new WaitForSeconds(waveCoolDown);
        }
    }

    IEnumerator wavesLvl1()
    {
        ncols = GameManager.instance.board.ncols;
        startingCol = GameManager.instance.board.startingCol;
        float startCoolDown = 15;
        float unitCoolDown = 4;
        float waveCoolDown = 12;
        int numWaves = 3;
        int numUnits = 8;
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
                instantiateMonster(newCol, Zombie);
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
        float waveCoolDown = 12;
        int numWaves = 4;
        int numUnits = 15;
        int lastColVal = -1;

        numEnemiesWave = numUnits * numWaves;

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
                if (unitGenerated > 90)
                {
                    instantiateMonster(newCol, Tank);
                }
                else if (w > 1 && unitGenerated > 80)
                {
                    instantiateMonster(newCol, Tank);
                }
                else if (w > 2 && unitGenerated > 60)
                {
                    instantiateMonster(newCol, Tank);
                }
                else
                {
                    instantiateMonster(newCol, Zombie);
                }
                yield return new WaitForSeconds(unitCoolDown);
            }
            yield return new WaitForSeconds(waveCoolDown);
        }
    }

    IEnumerator wavesLvl3()
    {
        started = true;
        ncols = GameManager.instance.board.ncols;
        startingCol = GameManager.instance.board.startingCol;
        float startCoolDown = 15;
        float unitCoolDown = 4;
        float waveCoolDown = 15;
        int numWaves = 6;
        int numUnits = 15;
        int lastColVal = -1;

        numEnemiesWave = numUnits * numWaves;

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
                if(w > 2 && unitGenerated > 90)
                {
                    instantiateMonster(newCol, Dragon);
                }
                else if (w > 3 && unitGenerated > 80)
                {
                    instantiateMonster(newCol, Dragon);
                }
                else if (w > 4 && unitGenerated > 70)
                {
                    instantiateMonster(newCol, Dragon);
                }
                else if (unitGenerated > 60)
                {
                    instantiateMonster(newCol, Tank);
                }
                else if (w > 1 && unitGenerated > 40)
                {
                    instantiateMonster(newCol, Tank);
                }
                else if (w > 2 && unitGenerated > 30)
                {
                    instantiateMonster(newCol, Tank);
                }
                else
                {
                    instantiateMonster(newCol, Zombie);
                }
                yield return new WaitForSeconds(unitCoolDown);
            }
            yield return new WaitForSeconds(waveCoolDown);
        }
    }

    private void instantiateMonster(int col, GameObject unit)
    {
        Debug.Log("Zombie instance");
        GameObject aux = Instantiate(unit, new Vector3(Random.Range(12, 14), 0.06F, col), transform.rotation, enemies);
        aux.transform.Rotate(0F, -90F, 0);
        aux.GetComponent<Stats>().vel += Random.Range(-0.25f, 0.05f); // Little random enemy velocity
    }
}
