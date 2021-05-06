using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlots : MonoBehaviour
{
    public bool[] isFull;
    public Image[] slots;
    private GameObject weapons;


    void Awake()
    {

        weapons = GameObject.Find("Weapons");
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = weapons.transform.GetChild(i).gameObject.GetComponent<Image>();
        }
    }
}
