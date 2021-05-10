using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float reloadTime = 1f;
    public float nextShoot = 0f;
    private CharStats charstats;
    private GameObject player;

    public float bulletForce = 20f;

    void Awake()
    {
        player = GameObject.Find("Player");
        charstats = player.GetComponent<CharStats>();
    }

    void Start()
    {
        //charstats.ReloadBarSetup();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > nextShoot) 
        {
            nextShoot = Time.time + reloadTime;
            Shoot();
        }

        //charstats.SetReloadBar(nextShoot, reloadTime);
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
