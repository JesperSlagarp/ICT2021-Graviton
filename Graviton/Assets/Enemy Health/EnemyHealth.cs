using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    //public enemyHealthbarBehaviour healthBar;
    public int maxHealth;
    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        //healthBar.SetHealth(health, maxHealth);
    }

    /*/ Update is called once per frame
    void Update()
    {
        
    }*/


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            health = health - 1;
            //healthBar.SetHealth(health, maxHealth);
           
            if (health == 0)
                Destroy(this.gameObject);
        }
    }
}
