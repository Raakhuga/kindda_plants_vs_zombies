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
    public bool pause;

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
        initGame();

        Time.timeScale = 0;
        pause = true;
        pauseGame();
    }

    private void pauseGame()
    {
        board.enabled = !pause;
        resources.enabled = !pause;
        gameInteraction.enabled = !pause;
        enemyGenerator.enabled = !pause;
        goldGenerator.enabled = !pause;
    }

    public void restart()
    {
        SceneManager.LoadScene("Level1");
        initGame();

        Time.timeScale = 0;
        pause = true;
        pauseGame();
    }

    public void Update()
    {
        pauseGame();
    }

    void initGame()
    {
        board.initBoard();
        gameInteraction.initGameInteraction();
        enemyGenerator.initWaves();
        goldGenerator.initGold();
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
        initGame();
    }
}
