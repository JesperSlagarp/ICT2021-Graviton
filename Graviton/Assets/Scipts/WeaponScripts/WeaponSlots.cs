using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlots : MonoBehaviour
{
    public bool[] isFull;
    public Image[] slots;
    public GameObject weaponNow;
    public GameObject weaponNext;
    private GameObject weapons;
    private GameObject player;


    void Start()
    {
        player = GameObject.Find("Player");
        weaponNow = null;
        weaponNext = null;
        weapons = GameObject.Find("Weapons");
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = weapons.transform.GetChild(i).gameObject.GetComponent<Image>();
        }
    }

    void Update()
    {
        if (GameObject.Find("Player").transform.childCount == 3)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                player.transform.Find(weaponNow.name + "(Clone)").gameObject.SetActive(false);
                player.transform.Find(weaponNext.name + "(Clone)").gameObject.SetActive(true);

                GameObject temp = weaponNow;
                weaponNow = weaponNext;
                weaponNext = temp;
            }
        }

        if(weaponNow != null)
        {
            slots[0].sprite = weaponNow.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            slots[0].sprite = null;
        }

        if(weaponNext != null)
        {
            slots[1].sprite = weaponNext.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            slots[1].sprite = null;
        }
        
    }

    public void Drop()
    {
        if(player.transform.childCount == 2)
        {
            weaponNow = null;
        }
        if(player.transform.childCount == 3)
        {
            player.transform.Find(weaponNext.name + "(Clone)").gameObject.SetActive(true);
            weaponNow = weaponNext;
            weaponNext = null;
        }
    }
}
