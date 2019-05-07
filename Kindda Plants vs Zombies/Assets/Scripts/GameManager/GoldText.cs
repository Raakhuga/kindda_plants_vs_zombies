using UnityEngine;
using UnityEngine.UI;

public class GoldText : MonoBehaviour
{

    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    void Start()
    {
        text.text = ((int)GameManager.instance.resources.resources).ToString();
    }

    void Update()
    {
        text.text = ((int)GameManager.instance.resources.resources).ToString();
    }
}
