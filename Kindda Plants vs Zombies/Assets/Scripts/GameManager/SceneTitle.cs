using UnityEngine;
using UnityEngine.UI;
public class SceneTitle : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    void Start()
    {
        text.text = GameManager.instance.nameLvl;
    }

    void Update()
    {
        text.text = text.text = GameManager.instance.nameLvl;
    }
}
