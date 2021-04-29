using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        void Shoot()
        {
            Vector3 ex = new Vector3(0,1,0) + firePoint.position;
            Vector3 direx = firePoint.position.normalized;
            Vector3 dir = new Vector3(direx.y, direx.x, 0);
            //Vector3 dir = new Vector3(0, 1, 0) + direx;

            GameObject bullet = Instantiate(bulletPrefab, ex, firePoint.rotation);
            GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            //GameObject bullet3 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            //Rigidbody2D rb3 = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(dir * bulletForce, ForceMode2D.Impulse);
            rb2.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            //rb3.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }
}
