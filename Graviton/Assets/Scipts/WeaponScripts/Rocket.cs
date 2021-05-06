using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public int damage;
    public int explosionRadius;
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject target in enemies)
        {
            float distance = Vector2.Distance(target.transform.position, transform.position);
            if (distance <= explosionRadius)
            {
                EnemyStats enemystat = target.GetComponent<EnemyStats>();
                enemystat.TakeDamage(damage);
            }
        }
        Destroy(this.gameObject);
    }
}
