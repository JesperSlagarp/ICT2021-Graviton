using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider manaSlider;
    public Slider cooldownSlider;
    public Slider cooldownSlider2;

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    public void SetMaxMana(int mana)
    {
        manaSlider.maxValue = mana;
        manaSlider.value = mana;
    }

    public void SetMana(int mana)
    {
        manaSlider.value = mana;
    }

    public void SetMaxCooldown(float cooldown)
    {
        cooldownSlider.maxValue = cooldown;
    }

    public void SetCooldown(float cooldown)
    {
        cooldownSlider.value = cooldown;
    }

    public void SetMaxCooldown2(float cooldown)
    {
        cooldownSlider2.maxValue = cooldown;
    }

    public void SetCooldown2(float cooldown)
    {
        cooldownSlider2.value = cooldown;
    }
}
