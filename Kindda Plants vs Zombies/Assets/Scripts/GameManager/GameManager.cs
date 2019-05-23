using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardController board;
    public ResourcesController resources;
    public GameInteractionController gameInteraction;
    public EnemyGen enemyGenerator;
    public GoldGen goldGenerator;
    public bool pause = false;

    public int sceneIdx = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        board = GetComponent<BoardController>();
        resources = GetComponent<ResourcesController>();
        gameInteraction = GetComponent<GameInteractionController>();
        enemyGenerator = GetComponent<EnemyGen>();
        goldGenerator = GetComponent<GoldGen>();

        //sceneIdx = 0;
        sceneIdx = 1;
    }

    public void pauseGame()
    {
        board.enabled = pause;
        resources.enabled = pause;
        gameInteraction.enabled = pause;
        enemyGenerator.enabled = pause;
        goldGenerator.enabled = pause;
        pause = !pause;
        Time.timeScale = pause ? 0 : 1;
    }

    public void restart()
    {
        SceneManager.LoadScene("Level");
    }

    void initGame()
    {
        board.initBoard();
        resources.initResources();
        gameInteraction.initGameInteraction();
        enemyGenerator.initWaves();
        goldGenerator.initGold();
        pause = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllCoroutines();
        if (sceneIdx != 0)
        {
            initGame();
            pauseGame();
        }
    }
}
