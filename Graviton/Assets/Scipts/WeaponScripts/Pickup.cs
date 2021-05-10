using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //public GameObject player;
    private WeaponSlots weaponSlots;
    public GameObject playerWeapon;
    //private InventorySlots inventorySlots;
    private GameObject player;

    private void Start()
    {
        weaponSlots = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSlots>();
        //inventorySlots = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySlots>();
        player = GameObject.Find("Player");

    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetButton("Pickup")){
            Debug.Log("Pressed F");
            for (int i = 0; i < weaponSlots.slots.Length; i++)
            {
                if(weaponSlots.isFull[i] == false)
                {
                    weaponSlots.isFull[i] = true;
                    
                    Debug.Log("Before error");
                    weaponSlots.slots[i].sprite = playerWeapon.GetComponent<SpriteRenderer>().sprite;
                    Debug.Log("After error");
                }
            }

            if (player.transform.childCount == 1)
            {
                Vector3 pos = new Vector3(0.6f, 0.8f, 0) + player.transform.position;

                GameObject weapon = Instantiate(playerWeapon, pos, Quaternion.identity);

                Destroy(this.gameObject);
                weapon.transform.parent = player.transform;
                //transform.GetComponent<BoxCollider2D>().enabled = false;
            }
            /*for (int j = 0; j < inventorySlots.slots.Length; j++)
            {
                if (inventorySlots.isFull[j] == false)
                {
                    inventorySlots.isFull[j] = true;
                    inventorySlots.slots[j].sprite = playerWeapon.GetComponent<SpriteRenderer>().sprite;
                    break;
                }
            }*/
        }
    }


    
}
