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
    public bool shield = false;
    public Stats damage;
    private float remainingTime;
    private float delayTime = 3;
    public float damageCooldown;
    private float nextDamagetaken = 0f;

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
        if(Input.GetKeyDown(KeyCode.Y))
            DepleteMana(10);
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

    public void DepleteMana(int mana)
    {
        if (playerMana >= 0 && mana <= playerMana)
        {
            playerMana = playerMana - mana;
            healthBar.SetMana(playerMana);
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



    public virtual void Die(){
        //play death animation
    }

}
