using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxMana(int mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }

    public void SetMana(int mana)
    {
        slider.value = mana;
    }
}
