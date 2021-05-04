using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public Transform firePoint;
    public float reloadTime;
    public float duration; 
    private float nextShoot = 0f;
    public LineRenderer lineRenderer;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void Shoot()
    {
        int mask = (1 << LayerMask.NameToLayer("Enemies")) | (1 << LayerMask.NameToLayer("obstacles"));
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, Mathf.Infinity, mask);

        if (hit.collider.gameObject.tag == "Enemy")
        {
            EnemyStats enemystats = hit.collider.gameObject.GetComponent<EnemyStats>();
            enemystats.TakeDamage(damage);
        }
        //Debug.DrawRay(firePoint.position, firePoint.right, Color.red, 10.0f);
        Debug.Log(hit.collider.gameObject.name);

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, hit.point);

        lineRenderer.enabled = true;
    }
}
