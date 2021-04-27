using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public LootTable thisLoot;
    public GameObject healthbar;
    public float health;
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
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            health -= 20;
            healthbar.transform.localScale = new Vector3(health / maxHealth, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
           
            if (health == 0)
            {
                MakeLoot();
                Destroy(this.gameObject);
            }
                
        }
    }

    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            Sprite current = thisLoot.LootSprite();
            if (current != null)
            {
                Debug.Log("sssssssss");
                Instantiate(current, transform.position, Quaternion.identity);
            }

        }
    }
}
