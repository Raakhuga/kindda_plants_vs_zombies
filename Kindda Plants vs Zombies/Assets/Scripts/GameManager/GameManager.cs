using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardController board;
    public ResourcesController resources;
    public DurabilityController durability;
    public GameInteractionController gameInteraction;
    public EnemyGen enemyGenerator;
    public GoldGen goldGenerator;

    void Awake()
    {
        if(instance == null)
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
        durability = GetComponent<DurabilityController>();
        gameInteraction = GetComponent<GameInteractionController>();
        enemyGenerator = GetComponent<EnemyGen>();
        goldGenerator = GetComponent<GoldGen>();
        initGame();
    }

    void initGame()
    {
        board.initBoard();
        gameInteraction.initGameInteraction();
        enemyGenerator.initWaves();
        goldGenerator.initGold();
    }
}
