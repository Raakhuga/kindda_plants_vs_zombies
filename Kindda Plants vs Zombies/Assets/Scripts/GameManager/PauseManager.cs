using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour
{
    Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = GameManager.instance.pause;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        GameManager.instance.pauseGame();
        canvas.enabled = GameManager.instance.pause;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuStartGame");
    }

    public void Skip()
    {
        GameManager.instance.currentLvl = (GameManager.instance.currentLvl + 1) % 7;
        Debug.Log("Skip to Lvl " + GameManager.instance.currentLvl);
        SceneManager.LoadScene("Level");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
