using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private InventorySlots inventorySlots;

    private void Start()
    {
        inventorySlots = GameObject.Find("Player").GetComponent<InventorySlots>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetButton("Pickup")) 
        {
            for (int i = 0; i < inventorySlots.slots.Length; i++)
            {
                if (inventorySlots.isFull[i] == false)
                {
                    inventorySlots.isFull[i] = true;
                    inventorySlots.slots[i].sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    if(gameObject.tag == "Key")
                    {
                        inventorySlots.slots[i].sprite.name = "Key";
                    }
                    break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
