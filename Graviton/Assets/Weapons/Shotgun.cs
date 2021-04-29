using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject droppedWeapon;
    public float reloadTime;
    private float nextShoot = 0f;

    public float bulletForce = 20f;
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextShoot)
        {
            nextShoot = Time.time + reloadTime;
            Shoot();
        }

        else if (Input.GetButtonDown("Drop"))
        {
            GameObject weapon = Instantiate(droppedWeapon, firePoint.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }


    void Shoot()
    {
        //Vector3 ex = new Vector3(0,1,0) + firePoint.position;
        //Vector3 direx = firePoint.position.normalized;
        //Vector3 dir = new Vector3(direx.y, direx.x, 0);
        //Vector3 dir = new Vector3(0, 1, 0) + direx;

        Rigidbody2D[] rigidbodies = new Rigidbody2D[3];

        for (int n = 0; n < 3; n++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            rigidbodies[n] = bullet.GetComponent<Rigidbody2D>();
        }

        Vector3 dir = Quaternion.AngleAxis(10, firePoint.up) * firePoint.right;

        rigidbodies[0].AddForce(dir * bulletForce, ForceMode2D.Impulse);

        rigidbodies[0].AddForce((firePoint.up + firePoint.right) * bulletForce, ForceMode2D.Impulse);
        rigidbodies[1].AddForce((firePoint.right) * bulletForce, ForceMode2D.Impulse);
        rigidbodies[2].AddForce((-(firePoint.up) + firePoint.right) * bulletForce, ForceMode2D.Impulse);

        //Vector3 dir = Quaternion.AngleAxis(-90, Vector3.up) * firePoint.right;

        /*GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //GameObject bullet3 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        //Rigidbody2D rb3 = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * bulletForce, ForceMode2D.Impulse);
        rb2.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        //rb3.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);*/
    }
}
