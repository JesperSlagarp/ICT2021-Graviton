using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //public GameObject player;
    public GameObject playerWeapon;
    void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetButton("Pickup"))
        {
            GameObject player = GameObject.Find("PlayerFront");
            if (player.transform.childCount == 0)
            {
                GameObject weapon = Instantiate(playerWeapon, player.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                weapon.transform.parent = player.transform;
            }
        }
    }
}
