using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurabilityText : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    void Start()
    {
        text.text = ((int)GameManager.instance.durability.durability).ToString();
    }

    void Update()
    {
        text.text = ((int)GameManager.instance.durability.durability).ToString();
    }
}
