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
        player = GameObject.Find("Player");
        weaponSlots = player.GetComponent<WeaponSlots>();
        //inventorySlots = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySlots>();

    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetButton("Pickup")){
            Debug.Log("Pressed F");
            /*for (int i = 0; i < weaponSlots.slots.Length; i++)
            {
                if(weaponSlots.isFull[i] == false)
                {
                    weaponSlots.isFull[i] = true;
                    
                    Debug.Log("Before error");
                    break;
                }
            }*/


            Vector3 pos = new Vector3(0.6f, 0.8f, 0) + player.transform.position;

            GameObject weapon = Instantiate(playerWeapon, pos, Quaternion.identity);

            Destroy(this.gameObject);

            weapon.transform.parent = player.transform;

            if (player.transform.childCount == 2)
            {
                weaponSlots.weaponNow = playerWeapon;
            }
            else if(player.transform.childCount == 3)
            {
                weaponSlots.weaponNext = weaponSlots.weaponNow;
                weaponSlots.weaponNow = playerWeapon;
                player.transform.Find(weaponSlots.weaponNext.name + "(Clone)").gameObject.SetActive(false);
                //Destroy(player.transform.GetChild(1).gameObject);
            }
            else if(player.transform.childCount == 4)
            {
                GameObject temp = weaponSlots.weaponNow;
                player.transform.Find(temp.name + "(Clone)").gameObject.GetComponent<Drop>().DropWeapon();
                weaponSlots.weaponNow = playerWeapon;
            }


            /*if (player.transform.childCount <= 2)
            {
                Vector3 pos = new Vector3(0.6f, 0.8f, 0) + player.transform.position;

                GameObject weapon = Instantiate(playerWeapon, pos, Quaternion.identity);

                Destroy(this.gameObject);
                weapon.transform.parent = player.transform;
                //transform.GetComponent<BoxCollider2D>().enabled = false;
            }*/


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
