using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{

    public bool instructions = false;
    public GameObject insts;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }

        insts.SetActive(instructions);
    }

    public void StartLevel(int levelIdx)
    {
        GameManager.instance.currentLvl = levelIdx;
        SceneManager.LoadScene("Level");
    }

    public void Instructions()
    {
        instructions = !instructions;
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
