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
                GameObject.Find("Player").GetComponent<CharStats>().getExp(objectQuantity);
                GameObject.Find("Player").GetComponent<CharStats>().UpdateStats();
            }
            if (currentObject == PickupObject.HEART)
            {
                GameObject.Find("Player").GetComponent<CharStats>().Heal(objectQuantity);
                GameObject.Find("Player").GetComponent<CharStats>().UpdateStats();
            }
            Destroy(gameObject);
        }
    }
}
