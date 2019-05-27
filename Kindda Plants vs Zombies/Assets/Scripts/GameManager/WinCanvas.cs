using UnityEngine;

public class WinCanvas : MonoBehaviour
{
    Canvas canvas;

    public GameObject winPanel;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        winPanel.SetActive(false);
    }

    void Update()
    {
        bool wasEnabled = canvas.enabled;
        canvas.enabled = !GameManager.instance.enemyGenerator.started && GameManager.instance.enemyGenerator.numEnemiesWave < 1;
        winPanel.SetActive(canvas.enabled && GameManager.instance.lastGame);
        if (canvas.enabled && !wasEnabled)
        {
            GameManager.instance.playTriumph();
        }
    }
}
