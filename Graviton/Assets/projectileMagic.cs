using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMagic : MonoBehaviour
{
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private GameObject projectile;

    private GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");

        InvokeRepeating("shoot", fireRate, fireRate);
    }
    private void shoot()
    {
        Vector3 shootDir = player.transform.position - transform.position;
        shootDir = shootDir.normalized;
        GameObject tempProjectile = Instantiate(projectile, transform);
        GameObject tempProjectile1 = Instantiate(projectile, transform);
        GameObject tempProjectile2 = Instantiate(projectile, transform);
        float spread = 20;
        Quaternion bulletRotation1 = Quaternion.AngleAxis(spread, Vector3.forward);
        Vector3 shootDir1 = bulletRotation1 * shootDir;
        Quaternion bulletRotation2 = Quaternion.AngleAxis(-spread, Vector3.forward);
        Vector3 shootDir2 = bulletRotation2 * shootDir;
        tempProjectile.GetComponent<Rigidbody2D>().AddForce(shootDir * projectileSpeed, ForceMode2D.Impulse);
        tempProjectile1.GetComponent<Rigidbody2D>().AddForce(shootDir1 * projectileSpeed, ForceMode2D.Impulse);
        tempProjectile2.GetComponent<Rigidbody2D>().AddForce(shootDir2 * projectileSpeed, ForceMode2D.Impulse);
    }
}

    
