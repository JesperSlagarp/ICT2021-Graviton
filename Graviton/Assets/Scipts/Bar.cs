using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
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

    public void SetMaxExp(int exp)
    {
        slider.maxValue = exp;
        slider.value = 0;
    }

    public void SetExp(int exp)
    {
        slider.value = exp;
    }

    public void SetMaxCooldown(float cooldown)
    {
        slider.maxValue = cooldown;
    }

    public void SetCooldown(float cooldown)
    {
        slider.value = cooldown;
    }

    public void SetMaxCooldown2(float cooldown)
    {
        slider.maxValue = cooldown;
        slider.value = cooldown;
    }

    public void SetCooldown2(float cooldown)
    {
        slider.value = cooldown;
    }
}
