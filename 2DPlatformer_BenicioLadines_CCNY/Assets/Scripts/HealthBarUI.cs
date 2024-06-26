using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{

    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        SetHealth(health);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
