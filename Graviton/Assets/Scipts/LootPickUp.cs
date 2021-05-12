using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickUp : MonoBehaviour
{
    public enum PickupObject{EXP, HEART, ARMOR};
    public PickupObject currentObject;
    public int objectQuantity;
    private CharStats charStats;

    void Awake()
    {
        charStats = GameObject.Find("Player").GetComponent<CharStats>();
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(currentObject == PickupObject.EXP)
            {
                charStats.getExp(objectQuantity);
            }
            if (currentObject == PickupObject.HEART)
            {
                charStats.Heal(objectQuantity);
            }
            if(currentObject == PickupObject.ARMOR)
            {
                charStats.GetArmor(objectQuantity);
            }

            Destroy(gameObject);
        }
    }
}
