using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharStats : MonoBehaviour
{

    private Bar healthBar;
    private Bar manaBar;
    private Bar expBar;

    public int maxHealth = 100;
    public int maxMana = 100;

    public int playerMana { get; private set; }
    public int playerHealth { get; private set; }
    public bool damageTaken = false;
    public bool shield = false;

    public Stats damage;
    public Stats armor;

    public int statPoint = 0;
    public int skillPoint = 0;

    [Min(0)]
    public int baseArmor = 0;
    public int baseDamage { get; private set; } = 25;

    private float remainingTime;
    private float delayTime = 3;
    public float damageCooldown;
    private float nextDamagetaken = 0f;

    public int exp;
    public int maxExp = 100;
    public int level = 1;

    public int aoeLevel;
    public int shieldLevel;

    private GameObject canvas;

    /*public void Save()
    {
        PlayerPrefs.SetInt("Health", playerHealth);
        PlayerPrefs.SetInt("Mana", playerMana);
    }

    public void Load()
    {
        playerHealth = PlayerPrefs.GetInt("Health");
        playerMana = PlayerPrefs.GetInt("Mana");
    }*/

    void Awake(){
        canvas = GameObject.Find("Canvas");

        healthBar = canvas.transform.Find("HealthBar").GetComponent<Bar>();
        manaBar = canvas.transform.Find("ManaBar").GetComponent<Bar>();
        expBar = canvas.transform.Find("ExpBar").GetComponent<Bar>();

        playerHealth = maxHealth;
        playerMana = maxMana;
        exp = 80;

        aoeLevel = 1;
        shieldLevel = 1;

        healthBar.SetMaxHealth(maxHealth);
        manaBar.SetMaxMana(maxMana) ;
        expBar.SetMaxExp(maxExp);
        UpdateStats();
    }

    void Start(){
        InvokeRepeating("RegenerateHP", 0, 1);
        InvokeRepeating("RegenerateMana", 0, 1);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.T))
            TakeDamage(10);
        if (Input.GetKeyDown(KeyCode.Y))
            DepleteMana(10);
        if (damageTaken)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0)
                damageTaken = false;
        }
    }


    public void TakeDamage(int dps){


        int currentArmor = armor.GetValue(baseArmor);
        dps = dps - currentArmor;
        dps = Mathf.Clamp(dps, 0, int.MaxValue);

        playerHealth = playerHealth - dps;
        damageTaken = true;
        remainingTime = delayTime;
        Debug.Log(transform.name + " damage taken: " + dps);
        healthBar.SetHealth(playerHealth);

        if (playerHealth <= 0){
            Debug.Log("health: "+ playerHealth);
            Die();
            Destroy(this.gameObject);
        }
    }

    void RegenerateHP(){
        if(!damageTaken){
        if(playerHealth < maxHealth)
            playerHealth += 2;
            healthBar.SetHealth(playerHealth);
        }
    }
    void RegenerateMana(){
        if(playerMana < maxMana)
        {
            playerMana += 2;
            manaBar.SetMana(playerMana);
            Debug.Log("added mana");
        }
        
    }

    public void DepleteMana(int mana)
    {
        if (playerMana >= 0 && mana <= playerMana)
        {
            playerMana = playerMana - mana;
            manaBar.SetMana(playerMana);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && shield == false)
        {
            if (nextDamagetaken < Time.time)
            {
                EnemyStats enemystats = collision.gameObject.GetComponent<EnemyStats>();
                TakeDamage(enemystats.damage);
                nextDamagetaken = Time.time + damageCooldown;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemybullet" && shield == false)
        {
            EnemyBullet enemybullet = collision.gameObject.GetComponent<EnemyBullet>();
            TakeDamage(enemybullet.damage);
        }
    }


    public void Heal(int quantity)
    {
        playerHealth += quantity;
        if(playerHealth >= maxHealth)
        {
            playerHealth = maxHealth;
        }
        UpdateStats();
    }

    public void getExp(int quantity)
    {
        exp += quantity;
        if (exp >= maxExp)
        {
            levelUp();
        }
        UpdateStats();
    }

    public void levelUp()
    {
        level += 1;
        exp = 0;
        maxExp += 50;
        expBar.SetMaxExp(maxExp);
        statPoint++;
        skillPoint++;
    }

    public void MaxHealthIncreasement()
    {
        if(statPoint > 0)
        {
            maxHealth += 20;
            playerHealth += 20;
            statPoint--;
        }
        UpdateStats();
    }

    public void MaxManaIncreasement()
    {
        if (statPoint > 0)
        {
            maxMana += 10;
            playerMana += 10;
            statPoint--;
        }
        UpdateStats();
    }

    public void DamageIncreasement()
    {
        if (statPoint > 0)
        {
            baseDamage += 5;
            statPoint--;
        }
    }

    public void ArmorIncreasement()
    {
        if (statPoint > 0)
        {
            baseArmor += 1;
            statPoint--;
        }
    }

    public void GetArmor(int armor)
    {
        baseArmor += armor;
    }

    public void AoeLevelUp()
    {
        if(skillPoint > 0)
        {
            aoeLevel += 1;
            skillPoint--;
        }
    }

    public void ShieldLevelUp()
    {
        if(skillPoint > 0)
        {
            shieldLevel += 1;
            skillPoint--;
        }
    }

    public void UpdateStats()
    {

        healthBar.SetHealth(playerHealth);
        manaBar.SetMana(playerMana);
        expBar.SetExp(exp);


    }

    public virtual void Die(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //play death animation
    }

}
