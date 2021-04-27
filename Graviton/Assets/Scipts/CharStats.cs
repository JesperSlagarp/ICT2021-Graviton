using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharStats : MonoBehaviour
{
    public HealthBar healthBar;
    public int maxStat = 100;
    public int playerMana{get;private set;}
    public int playerHealth{get;private set;}
    public bool damageTaken = false;
    public Stats damage;
    private float remainingTime;
    private float delayTime = 3;

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
        healthBar.SetMaxHealth(maxStat);
        healthBar.SetMaxMana(maxStat);
        playerHealth = maxStat;
        playerMana = maxStat;
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


    public void TakeDamage(int damage){

        playerHealth = playerHealth - damage;
        damageTaken = true;
        remainingTime = delayTime;
        Debug.Log(transform.name + " damage taken: " + damage);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //play death animation
    }

}
