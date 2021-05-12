using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrossBow : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int maxDistance;
    public float reloadTime = 1f;
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
        //Vector2 v = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        //float distance = v.magnitude;
        int mask = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("obstacles"));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, mask);
        //Debug.Log(hit.collider.gameObject.name);
        if (hit.collider.gameObject.tag == "Player") //&& distance < maxDistance
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
        }
    }
}
