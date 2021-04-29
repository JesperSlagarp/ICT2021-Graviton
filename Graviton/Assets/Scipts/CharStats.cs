using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharStats : MonoBehaviour
{

    public Bar healthBar;
    public Bar manaBar;
    public Bar expBar;
    public int maxStat = 100;
    public int playerMana { get; private set; }
    public int playerHealth { get; private set; }
    public bool damageTaken = false;

    public Stats damage;
    public Stats armor;

    [Min(0)]
    public int baseArmor = 0;
    public int baseDamage = 1;

    private float remainingTime;
    private float delayTime = 3;

    public int exp;
    public int maxExp = 100;
    public int level = 1;

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
        playerHealth = maxStat;
        playerMana = maxStat;
        exp = 80;

        healthBar.SetMaxHealth(maxStat);
        manaBar.SetMaxMana(maxStat);
        expBar.SetMaxExp(maxExp);
        UpdateStats();
    }

    void Start(){
        InvokeRepeating("RegenerateHP", 0, 1);
        InvokeRepeating("RegenerateMana", 0, 1);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.T))
            TakeDamage(10);
        if (damageTaken){
            remainingTime -= Time.deltaTime;
            if(remainingTime < 0)
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
        if(playerHealth < maxStat)
            playerHealth += 2;
            healthBar.SetHealth(playerHealth);
            Debug.Log("added hp");
        }
    }
    void RegenerateMana(){
        if(playerMana < maxStat)
        {
            playerMana += 2;
            manaBar.SetMana(playerMana);
            Debug.Log("added mana");
        }
            
            
        
    }

    public void Heal(int quantity)
    {
        playerHealth += quantity;
        if(playerHealth >= maxStat)
        {
            playerHealth = maxStat;
        }
    }

    public void getExp(int quantity)
    {
        exp += quantity;
        if(exp >= maxExp)
        {
            levelUp();
        }
    }

    public void levelUp()
    {
        level += 1;
        exp = 0;
        maxExp += 50;
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
