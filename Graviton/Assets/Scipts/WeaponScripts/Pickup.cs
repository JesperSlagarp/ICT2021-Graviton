using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //public GameObject player;
    private WeaponSlots weaponSlots;
    public GameObject playerWeapon;

    private void Start()
    {
        weaponSlots = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSlots>();
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
        }

        for(int i = 0; i < weaponSlots.inventorySlots.Length; i++)
        {
            if (weaponSlots.inventoryIsFull[i] == false)
            {
                weaponSlots.inventoryIsFull[i] = true;
                weaponSlots.inventorySlots[i].sprite = playerWeapon.GetComponent<SpriteRenderer>().sprite;
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
