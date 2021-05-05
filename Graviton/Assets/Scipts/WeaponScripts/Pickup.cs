using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //public GameObject player;
    private WeaponSlots weaponSlots;
    public GameObject playerWeapon;
    private InventorySlots inventorySlots;

    private void Start()
    {
        weaponSlots = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSlots>();
        inventorySlots = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySlots>();
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetButton("Pickup")){
            Debug.Log("Pressed F");
            for(int i = 0; i < weaponSlots.slots.Length; i++)
            {
                if(weaponSlots.isFull[i] == false)
                {
                    weaponSlots.isFull[i] = true;
                    GameObject player = GameObject.Find("Player");
                    Debug.Log("Before error");
                    weaponSlots.slots[i].sprite = playerWeapon.GetComponent<SpriteRenderer>().sprite;
                    Debug.Log("After error");
                    if (player.transform.childCount == 1)
                    {
                        Vector3 pos = new Vector3(0.6f, 0.8f, 0) + player.transform.position;

                        GameObject weapon = Instantiate(playerWeapon, pos, Quaternion.identity);
                        
                        Destroy(this.gameObject);
                        weapon.transform.parent = player.transform;
                    }
               }
            }
            for (int j = 0; j < inventorySlots.slots.Length; j++)
            {
                if (inventorySlots.isFull[j] == false)
                {
                    inventorySlots.isFull[j] = true;
                    inventorySlots.slots[j].sprite = playerWeapon.GetComponent<SpriteRenderer>().sprite;
                    break;
                }
            }
        }
    }
    
    void UpdateUI()
    {
        for (int i = 0; i < weaponSlots.slots.Length; i++)
        {
            
        }

    }
}
