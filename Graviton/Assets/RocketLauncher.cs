using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Transform firePoint;
    public GameObject rocketPrefab;
    public GameObject droppedWeapon;
    private GameObject player;
    public float reloadTime = 1f;
    private float nextShoot = 0f;

    public float bulletForce = 20f;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

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
        Recoil();
    }

    void Recoil()
    {
        Rigidbody2D playerrb = player.GetComponent<Rigidbody2D>();
        //playerrb.AddForce((firePoint.right) * bulletForce, ForceMode2D.Force);
        //playerrb.MovePosition(player.transform.position + 20);
    }
}
