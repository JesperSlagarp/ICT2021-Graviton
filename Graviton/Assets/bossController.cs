using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    [SerializeField]
    private GameObject healthbar;

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private GameObject laserMount;

    [SerializeField]
    private GameObject AoE_Magic;

    [SerializeField]
    private GameObject projectileMagic;

    [SerializeField]
    private bool phaseTwo;

    private float health;

    private void Awake()
    {
        health = maxHealth;
        laserMount.SetActive(false);
        AoE_Magic.SetActive(false);
        projectileMagic.SetActive(false);
        Debug.Log("All false");
    }

    private void FixedUpdate()
    {
        if (health < maxHealth / 2)
            phaseTwo = true;
        if (phaseTwo)
        {
            Debug.Log("phase 2");
            laserMount.SetActive(true);
            AoE_Magic.SetActive(true);
            projectileMagic.SetActive(true);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Bullet") 
        { 
            int playerDamage = GameObject.Find("Player").GetComponent<CharStats>().baseDamage; TakeDamage(playerDamage); 
        } 
    }

    public void TakeDamage(int damage)
    {
        if (health < damage || health == 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            health = health - damage;
            healthbar.transform.localScale = new Vector3(health / maxHealth, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Detected");
        if (!collider.CompareTag("Player"))
            return;

        Debug.Log("Player detected");
        laserMount.SetActive(true);
        AoE_Magic.SetActive(false);
        projectileMagic.SetActive(false);  
    }
}
