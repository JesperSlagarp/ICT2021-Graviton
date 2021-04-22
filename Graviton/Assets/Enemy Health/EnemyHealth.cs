using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health = 10f;
    // Start is called before the first frame update
   /* void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            if (health == 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                health = health - 1;
            }
        }
    }
}
