using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserGun : MonoBehaviour
{
    public Transform firePoint;
    public float reloadTime;
    public float duration;
    private float nextShoot = 0f;
    private float timer;
    private bool reload;
    public LineRenderer lineRenderer;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            
        }
    }
}
