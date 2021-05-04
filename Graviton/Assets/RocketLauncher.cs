using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Transform firePoint;
    public GameObject rocketPrefab;
    public GameObject droppedWeapon;
    public float reloadTime = 1f;
    private float nextShoot = 0f;

    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextShoot)
        {
            nextShoot = Time.time + reloadTime;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
