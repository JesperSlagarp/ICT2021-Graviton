using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public LineRenderer lineRenderer;
    public Transform firePoint;

    private GameObject player;
    private CharStats charStats;

    
    private void Awake()
    {
        player = GameObject.Find("Player");
        charStats = player.GetComponent<CharStats>();
    }

    private void FixedUpdate()
    {
        if(isActiveAndEnabled)
            shootLaser();
    }

    void shootLaser() 
    {

        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, transform.right, defDistanceRay,0x1 << LayerMask.NameToLayer("Player"));
        RaycastHit2D hitObstacles = Physics2D.Raycast(transform.position, transform.right, defDistanceRay, 0x1 << LayerMask.NameToLayer("obstacles"));
        
        if (hitPlayer.collider != null)
        {
            
            draw2DRay(firePoint.position, hitPlayer.point);

            //Makes player take damage
            charStats.TakeDamage(1);
        }
        else if (hitObstacles.collider != null) 
        {
            draw2DRay(firePoint.position, hitObstacles.point);
        }
        else 
        {
            draw2DRay(firePoint.position, firePoint.transform.right * defDistanceRay);
        }
    }

    void draw2DRay(Vector2 startPos, Vector2 endPos) 
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
