using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform mob;
    public Transform player;
    RaycastHit2D hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canSeePlayer())
        {
            transform.position = player.position;
        } else {
            
            transform.position = mob.position;        
        }
    }

    bool canSeePlayer() {
        Vector2 v = new Vector2(player.position.x - mob.position.x, player.position.y - mob.position.y);
        float distance = v.magnitude;
        hit = Physics2D.Raycast(mob.position, v, distance, 0x1 << LayerMask.NameToLayer("obstacles"));
        if (hit.collider != null) return true;
        else return false;
    }
}
