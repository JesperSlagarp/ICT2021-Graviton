using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "EnemyBullet(Clone)" && collision.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
