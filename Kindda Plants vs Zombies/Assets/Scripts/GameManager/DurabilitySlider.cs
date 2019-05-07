using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurabilitySlider : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        slider.value = GameManager.instance.durability.durability;
    }

    void Update()
    {
        slider.value = GameManager.instance.durability.durability;
    }
}
