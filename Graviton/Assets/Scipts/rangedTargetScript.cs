using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedTargetScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform mob;
    private Transform player;
    private Vector3 lastSeen;
    RaycastHit2D hit;

    private float range = 20;
    void Start()
    {
        lastSeen = mob.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player = GameObject.Find("Player").transform;
        if (CanSeePlayer() && isActiveAndEnabled)
        {
            lastSeen = player.position;
            this.transform.position = mob.position;
        } else {
            this.transform.position = lastSeen;
        }
    }

    bool CanSeePlayer() {
        Vector2 v = new Vector2(player.position.x - mob.position.x, player.position.y - mob.position.y);
        float distance = Mathf.Min(v.magnitude, range);
        hit = Physics2D.Raycast(mob.position, v, distance, 0x1 << LayerMask.NameToLayer("obstacles"));
        if (hit.collider == null && distance < range) return true;
        else return false;
    }
}
