using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public Transform firePoint;
    public float duration; 
    private float nextShoot = 0f;
    private float timer;
    //private bool reload;
    public LineRenderer lineRenderer;
    public int damage;
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
            Shoot();
        }
        else
        {
            lineRenderer.enabled = false;
        }

        if (Time.time - timer > duration)
        {
            //charstats.SetReloadBar(nextShoot, duration);
        }
    }

    void Shoot()
    {
        int mask = (1 << LayerMask.NameToLayer("Enemies")) | (1 << LayerMask.NameToLayer("obstacles"));
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, 100, mask);
        if (hit.collider != null)
        {

            if (hit.collider.gameObject.tag == "Enemy")
            {
                EnemyStats enemystats = hit.collider.gameObject.GetComponent<EnemyStats>();
                enemystats.TakeDamage(damage);
            } 
            else if (hit.collider.gameObject.tag == "Boss")
            {
                hit.collider.gameObject.GetComponent<bossController>().TakeDamage(damage);
            }

            Debug.Log(hit.collider.gameObject.name);

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.transform.right);
        }


        lineRenderer.enabled = true;
    }
}
