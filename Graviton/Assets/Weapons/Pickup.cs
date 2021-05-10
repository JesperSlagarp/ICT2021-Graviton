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
            GameObject player = GameObject.Find("Player");
            
            if (player.transform.childCount == 2)
            {
              //  Vector3 pos = new Vector3(0.6f, 0.8f, 0) + player.transform.position;
                Vector3 pos = new Vector3(0, 0, 0) + player.transform.position;
               // GameObject weapon = Instantiate(playerWeapon, pos, Quaternion.identity);
                //Destroy(this.gameObject);
               // weapon.transform.parent = player.transform;
                gameObject.transform.parent = player.transform.Find("Aim");
                transform.GetComponent<BoxCollider2D>().enabled=false;

            }
        }
    }
}
