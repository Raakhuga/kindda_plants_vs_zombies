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
        if (!started)
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
                case 6:
                    Debug.Log("Wave Lvl 6");
                    StartCoroutine(wavesLvl3());
                    break;
                default:
                    Debug.Log("Wave default");
                    break;
            }
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
        int secondsWaitLoad = 6;
        if (GameManager.instance.currentLvl == 0 || GameManager.instance.currentLvl == 6) secondsWaitLoad += 14;
        yield return new WaitForSeconds(6);
        GameManager.instance.currentLvl = (GameManager.instance.currentLvl + 1) % 7;
        Debug.Log("Level " + GameManager.instance.currentLvl);
        if (GameManager.instance.currentLvl == 0 || GameManager.instance.currentLvl == 4)
        {
            SceneManager.LoadScene("MenuStartGame");
        }
        else
        {
            SceneManager.LoadScene("Level");
        }
    }

    IEnumerator wavesLvl1()
    {
        ncols = GameManager.instance.board.ncols;
        startingCol = GameManager.instance.board.startingCol;
        float startCoolDown = 15;
        float unitCoolDown = 5;
        float waveCoolDown = 12;
        int numWaves = 3;
        int numUnits = 6;
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
        float waveCoolDown = 10;
        int numWaves = 4;
        int numUnits = 12;
        int lastColVal = -1;

        numEnemiesWave = numUnits * numWaves;

        int probDragon = -1;
        int probTank = 20;

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

                if (w == 1)
                {
                    probTank = 30;
                }
                else if (w == 2)
                {
                    probTank = 40;
                }
                else if (w == 3)
                {
                    probTank = 50;
                }
                instancePerPercentage(probDragon, probTank, newCol);

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

        numEnemiesWave = numUnits * numWaves + 10;

        int probDragon = 5;
        int probTank = 40;

        yield return new WaitForSeconds(startCoolDown);
        for (int w = 0; w < numWaves; w++)
        {
            Debug.Log("Num Wave: " + w + " " + Time.time);
            if (w == 5)
            {
                numUnits += 10;
                unitCoolDown = 2;
            }
            for (int i = 0; i < numUnits; i++)
            {
                Debug.Log("New enemy unit: " + i + " " + Time.time);
                int newCol = Random.Range(0, ncols) + startingCol;
                while (ncols > 2 && newCol == lastColVal)
                {
                    newCol = Random.Range(0, ncols) + startingCol;
                }
                lastColVal = newCol;
                if (w == 1)
                {
                    probDragon = 15;
                    probTank = 45;
                }
                else if (w == 3)
                {
                    probDragon = 25;
                    probTank = 55;
                }
                else if (w == 4)
                {
                    probDragon = 35;
                    probTank = 60;
                }
                else if (w == 5)
                {
                    probDragon = 45;
                    probTank = 60;
                }
                instancePerPercentage(probDragon, probTank, newCol);
                yield return new WaitForSeconds(unitCoolDown);
            }
            yield return new WaitForSeconds(waveCoolDown);
        }
    }

    private void instancePerPercentage(int probDrag, int probTank, int column)
    {
        int unitGenerated = Random.Range(0, 100);
        if (unitGenerated > (100 - probDrag))
        {
            instantiateMonster(column, Dragon);
        }
        else if (unitGenerated > (100 - probDrag - probTank))
        {
            instantiateMonster(column, Tank);
        }
        else
        {
            instantiateMonster(column, Zombie);
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
