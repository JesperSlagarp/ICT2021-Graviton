using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject droppedWeapon;
    private GameObject player;

    // Update is called once per frame

    void Awake()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if (Input.GetButtonDown("Drop"))
        {
            DropWeapon();
        }
    }

    public void DropWeapon()
    {
        player.GetComponent<WeaponSlots>().Drop();
        gameObject.transform.parent = null;
        GameObject weapon = Instantiate(droppedWeapon, this.gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
