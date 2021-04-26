using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharStats : MonoBehaviour
{
    public HealthBar healthBar;
    public int maxStat = 100;
    public int playerMana{get;private set;}
    public int playerHealth{get;private set;}
    public bool damageTaken = false;
    public Stats damage;
    public Stats armor;
    [Min(0)]
    public int baseArmor = 0;
    public int baseDamage = 1;
    private float remainingTime;
    private float delayTime = 3;



    void Awake(){
        healthBar.SetMaxHealth(maxStat);
        healthBar.SetMaxMana(maxStat);
        playerHealth = maxStat;
        playerMana = maxStat;
    }

    void Start(){ //lägger till 1 hp
        InvokeRepeating("RegenerateHP", 0, 1);
        InvokeRepeating("RegenerateMana", 0, 1);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.T)) //testa ta damage
            TakeDamage(10);
        if (damageTaken){
            remainingTime -= Time.deltaTime;
            if(remainingTime < 0)
                damageTaken = false;
        }
    }


    public void TakeDamage(int dps){


        int currentArmor =  armor.GetValue(baseArmor);
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
            healthBar.SetMana(playerMana);
            Debug.Log("added mana");
        }
            
            
        
    }



    public virtual void Die(){
        //play death animation
    }

}
