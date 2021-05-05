using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotgun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float reloadTime = 1f;
    public int bulletAmount;
    public int spreadVariation;
    private float nextShoot = 0f;

    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + reloadTime;
            Shoot();
        }
    }


    void Shoot()
    {
        GameObject player = GameObject.Find("Player");
        int mask = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("obstacles"));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, mask);
        if (hit.collider.gameObject.tag == "Player")
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
}
