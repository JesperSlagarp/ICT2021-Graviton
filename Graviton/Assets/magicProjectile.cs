using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject.name);
        Destroy(gameObject);
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            GameObject.Find("Player").GetComponent<CharStats>().TakeDamage(10); 
        }
    }
}
