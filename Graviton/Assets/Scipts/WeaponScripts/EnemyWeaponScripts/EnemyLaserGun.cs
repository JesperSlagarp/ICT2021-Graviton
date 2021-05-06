using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserGun : MonoBehaviour
{
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public int damage;

    // Update is called once per frame
    void Update()
    {
            Shoot();
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
            CharStats charstats = hit.collider.gameObject.GetComponent<CharStats>();
            charstats.TakeDamage(damage);
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hit.point);
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }

        
    }
}
