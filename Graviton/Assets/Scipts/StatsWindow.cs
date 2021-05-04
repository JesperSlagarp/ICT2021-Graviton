using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsWindow : MonoBehaviour
{
    public Text statPoint;
    public Text health;
    public Text mana;
    public Text exp;
    public Text damage;
    public Text armor;

    public Text skillPoint;
    public Text aoe;
    public Text shield;

    private CharStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharStats>();
    }

    // Update is called once per frame
    void Update()
    {
        statPoint.text = "Stat Point: " + stats.statPoint;
        exp.text = "Exp: " + stats.exp + "/" + stats.maxExp;
        health.text = "Health: " + stats.playerHealth + "/" + stats.maxHealth;
        mana.text = "Mana: " + stats.playerMana + "/" + stats.maxMana;
        damage.text = "Damage: " + stats.baseDamage;
        armor.text = "Armor: " + stats.baseArmor;

        skillPoint.text = "Skill Point: " + stats.skillPoint;
        aoe.text = "AOE: Level " + stats.aoeLevel;
        shield.text = "Shield: Level " + stats.shieldLevel;
    }

    public void MaxHealth()
    {
        stats.MaxHealthIncreasement();
    }
    public void MaxMana()
    {
        stats.MaxManaIncreasement();
    }
    public void Damage()
    {
        stats.DamageIncreasement();
    }
    public void Armor()
    {
        stats.ArmorIncreasement();
    }

    public void AoeLevelUp()
    {
        stats.AoeLevelUp();
    }

    public void ShieldLevelUp()
    {
        stats.ShieldLevelUp();
    }
}
