using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeTargetScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform mob;
    private GameObject player;
    private Vector3 lastSeen;
    RaycastHit2D hit;
    void Start()
    {
        player = GameObject.Find("Player");
        lastSeen = mob.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanSeePlayer())
        {
            lastSeen = player.transform.position;
            this.transform.position = player.transform.position;
        }
        else
        {
            this.transform.position = lastSeen;
        }
    }

    bool CanSeePlayer()
    {
        Vector2 v = new Vector2(player.transform.position.x - mob.position.x, player.transform.position.y - mob.position.y);
        float distance = v.magnitude;
        hit = Physics2D.Raycast(mob.position, v, distance, 0x1 << LayerMask.NameToLayer("obstacles"));
        if (hit.collider == null) return true;
        else return false;
    }
}
