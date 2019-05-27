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
    public bool lostGame = false;
    public int currentLvl;
    public string nameLvl;

    public bool lastGame = false;

    public AudioSource music;

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

        currentLvl = 0;
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

    void initGame()
    {
        board.initBoard();
        resources.initResources();
        gameInteraction.initGameInteraction();
        enemyGenerator.initWaves();
        goldGenerator.initGold();
        pause = true;
        pauseGame();
    }

    public void GameLost()
    {
        lostGame = true;
        pause = false;

        Debug.Log("Start Audio Lost");
        music.clip = Resources.Load("Battle_Lost__6s") as AudioClip;
        music.Play();
        music.loop = false;

        board.enabled = false;
        resources.enabled = false;
        gameInteraction.enabled = false;
        enemyGenerator.enabled = false;
        goldGenerator.enabled = false;
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
        goldGenerator.stopCoroutines();
        enemyGenerator.stopCoroutines();
        lostGame = false;
        if (scene.name == "MenuStartGame")
        {
            pause = false;
            pauseGame();
            Time.timeScale = 1;
            currentLvl = 0;
        }
        else
        {
            initGame();
        }
        initLevel();
        playMusic();
    }

    private void initLevel()
    {
        lastGame = false;
        switch (currentLvl)
        {
            case 1:
                Debug.Log("Audio Lvl 1");
                nameLvl = "Day: The Noon";
                music.clip = Resources.Load("Fantasy_Celtic_Music_-_Spirit_of_the_Wild") as AudioClip;
                break;
            case 2:
                Debug.Log("Audio Lvl 2");
                nameLvl = "Day: The Afteroon";
                music.clip = Resources.Load("Epic_Celtic_Battle_Music_-_Battle_For_Camelot_Tartalo_Music") as AudioClip;
                break;
            case 3:
                Debug.Log("Audio Lvl 3");
                nameLvl = "Day: The Dusk";
                music.clip = Resources.Load("Celtic_battle_music_-_The_King_of_The_Highlands") as AudioClip;
                break;
            case 4:
                Debug.Log("Audio Lvl 4");
                nameLvl = "Night: The Night";
                music.clip = Resources.Load("Two_Steps_From_Hell_-_Victory") as AudioClip;
                break;
            case 5:
                Debug.Log("Audio Lvl 5");
                nameLvl = "Night: The Midnight";
                music.clip = Resources.Load("Two_Steps_From_Hell_-_Blackheart_Thomas_Bergersen_-_Epic_Dark_Melancholic_Dramatic") as AudioClip;
                break;
            case 6:
                Debug.Log("Audio Lvl 6");
                lastGame = true;
                nameLvl = "Night: The Next Morning";
                music.clip = Resources.Load("Two_Steps_From_Hell_-_Impossible_EXTENDED_Special_Edition") as AudioClip;
                break;
            default:
                Debug.Log("Audio default");
                music.clip = Resources.Load("MenuMusic") as AudioClip;
                break;
        }
    }

    private void playMusic()
    {
        Debug.Log("Start Audio");
        music.Play();
        music.loop = true;
    }

    public void playTriumph()
    {
        if (currentLvl == 6)
        {
            music.clip = Resources.Load("Final_Glorious_Triumph__10s") as AudioClip;
        }
        else
        {
            music.clip = Resources.Load("Final_Glorious_Triumph__10s") as AudioClip;
        }
        Debug.Log("Start Audio Triumph");
        music.Play();
        music.loop = false;
    }
}
