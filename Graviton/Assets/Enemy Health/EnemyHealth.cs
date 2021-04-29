using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public GameObject healthbar;
    public float health;
    private float maxHealth;
    public LootTable lootTable;
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
                Destroy(this.gameObject);
                MakeLoot();
            }
                
        }
    }

    private void MakeLoot()
    {
        if(lootTable != null)
        {
            GameObject current = lootTable.LootGameObject();
            if(current != null)
            {
                Instantiate(current, transform.position, Quaternion.identity);
            }
        }
    }
}
