using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public GameObject healthbar;
    public float health;
    public int damage;
    private float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        //healthBar.SetHealth(health, maxHealth);
    }

    /*/ Update is called once per frame
    void Update()
    {
        
    }*/


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Bullet enemybullet = collision.gameObject.GetComponent<Bullet>();
            health -= enemybullet.damage; 
            healthbar.transform.localScale = new Vector3(health / maxHealth, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
           
            if (health == 0)
                Destroy(this.gameObject);
        }
    }
}
