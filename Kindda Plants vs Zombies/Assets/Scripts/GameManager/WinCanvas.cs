using UnityEngine;

public class WinCanvas : MonoBehaviour
{
    Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    void Update()
    {
        bool wasEnabled = canvas.enabled;
        canvas.enabled = !GameManager.instance.enemyGenerator.started && GameManager.instance.enemyGenerator.numEnemiesWave < 1;
        if (canvas.enabled && !wasEnabled)
        {
            GameManager.instance.playTriumph();
        }
    }
}
