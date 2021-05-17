using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject droppedWeapon;
    public float bulletForce = 20f;
    public float reloadTime = 1f;
    public float fireRate;
    private float nextShoot = 0f;
    private float nextBullet = 0f;
    public float duration;
    private float timer;
    private CharStats charstats;
    private GameObject player;

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
        if (Input.GetButtonDown("Fire1") && Time.time > nextShoot)
        {
            timer = Time.time;
        }
        else if (Input.GetButton("Fire1") && Time.time - timer < duration)
        {
            nextShoot = (Time.time - timer) + Time.time;
            if (Time.time > nextBullet)
            {
                nextBullet = Time.time + fireRate;
                Shoot();
            }
        }

        if (Time.time - timer > duration)
        {
            //charstats.SetReloadBar(nextShoot, duration);
        }

        /*if (Input.GetButtonDown("Drop"))
        {
            GameObject weapon = Instantiate(droppedWeapon, firePoint.position, Quaternion.identity);
            Destroy(this.gameObject);
        }*/
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
