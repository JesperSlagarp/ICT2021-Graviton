using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject droppedWeapon;
    public float reloadTime;
    public int bulletAmount;
    public int spreadVariation;
    private float nextShoot = 0f;
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
        charstats.ReloadBarSetup();
    }

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

        charstats.SetReloadBar(nextShoot, reloadTime);
    }


    void Shoot()
    {
        for (int n = 0; n < bulletAmount; n++)
        {
            float spread = Random.Range(-spreadVariation, spreadVariation);
            Quaternion bulletRotation = Quaternion.AngleAxis(spread, Vector3.forward);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Vector3 dir = bulletRotation * firePoint.right;
            bullet.GetComponent<Rigidbody2D>().AddForce(dir * bulletForce, ForceMode2D.Impulse);
        }
    }
}
