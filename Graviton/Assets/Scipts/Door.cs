using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private InventorySlots inventory;

    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<InventorySlots>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == true)
            {
                if (inventory.slots[i].sprite.name == "Key")
                {
                    Debug.Log("found");
                    Destroy(this.gameObject);
                }
                else
                    Debug.Log("not found");
            }
        }
    }
}
