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
        tempProjectile.GetComponent<Rigidbody2D>().AddForce(shootDir * projectileSpeed, ForceMode2D.Impulse);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
