using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /*public Collision2D collision;

    void FixedUpdate()
    {
        OnCollisionEnter2D(collision);
    }*/

    /*void Start()
    {
        GameObject player = GameObject.Find("PlayerFront");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }*/

    public int damage;
    void OnCollisionEnter2D(Collision2D collision)
    {
            Destroy(this.gameObject);
    }
}
