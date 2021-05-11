using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    private InventorySlots inventory;
    private GameObject closed;
    private GameObject open;

    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<InventorySlots>();
        closed = gameObject.transform.Find("Closed").gameObject;
        open = gameObject.transform.Find("Open").gameObject;
        closeDoor();
    }

    private void closeDoor() {
        closed.SetActive(true);
        open.SetActive(false);
        Debug.Log("Door closed");
    }

    private void openDoor() {
        closed.SetActive(false);
        open.SetActive(true);
        Debug.Log("Door opened");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Door trigger");
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == true)
            {
                if (inventory.slots[i].sprite.name == "Key")
                {
                    openDoor();
                }
            }
        }
    }
}
