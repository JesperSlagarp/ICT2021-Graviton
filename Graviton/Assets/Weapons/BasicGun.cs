using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject player;
    public GameObject droppedWeapon;

    public float bulletForce = 20f;

    // Update is called once per frame
    void Update(){

        if (Input.GetButtonDown("Drop"))
        {
           // GameObject weapon = Instantiate(droppedWeapon, firePoint.position, Quaternion.identity);
            // Destroy(this.gameObject);
            gameObject.transform.parent = null;
            transform.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
