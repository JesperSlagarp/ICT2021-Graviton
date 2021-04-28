using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickUp : MonoBehaviour
{
    public enum PickupObject{EXP, HEART};
    public PickupObject currentObject;
    public int objectQuantity;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(currentObject == PickupObject.EXP)
            {
                GameObject.Find("Player").GetComponent<CharStats>().exp += objectQuantity;
            }
            if (currentObject == PickupObject.HEART)
            {
                GameObject.Find("Player").GetComponent<CharStats>().playerHealth += objectQuantity;
                GameObject.Find("Player").GetComponent<CharStats>().healthBar.SetHealth(GameObject.Find("Player").GetComponent<CharStats>().playerHealth);
            }
            Destroy(gameObject);
        }
    }
}
