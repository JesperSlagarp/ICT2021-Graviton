using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
    void FixedUpdate()
    {
        GameObject player = GameObject.Find("Player");
        Vector2 playerpos = player.transform.position;
        Vector2 transpos = transform.position;
        Vector2 lookDir = playerpos - transpos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;
    }
}
