using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    private GameObject weapon;
    private string weaponname;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 2)
        {
            weapon = transform.GetChild(1).gameObject;
            weaponname = weapon.name;


        }
    }
}
