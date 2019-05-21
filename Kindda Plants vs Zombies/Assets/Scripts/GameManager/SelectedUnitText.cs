using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedUnitText : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    void Start()
    {
        text.text = GameManager.instance.gameInteraction.selectedUnitName;
    }

    void Update()
    {
        text.text = GameManager.instance.gameInteraction.selectedUnitName;
    }
}
