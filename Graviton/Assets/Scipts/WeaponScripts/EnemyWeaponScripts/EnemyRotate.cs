using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
    public bool isLaser;
    public float rotateSpeed;
    private Vector3 weaponPos;
    private SpriteRenderer weaponSprite;
    private GameObject player;
    private GameObject enemy;

    void Start()
    {
        player = GameObject.Find("Player");
        enemy = this.gameObject.transform.parent.gameObject;
        weaponPos = this.gameObject.transform.localPosition;
        weaponSprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Vector2 playerpos = player.transform.position;
        Vector2 enemypos = enemy.transform.position;
        Vector2 transpos = transform.position;
        Vector2 lookDir = playerpos - transpos;
        Vector2 enemyLookDir = playerpos - enemypos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (enemyLookDir.x < 0)
        {
            this.gameObject.transform.localScale = new Vector3(1, -1, 1);
            this.gameObject.transform.localPosition = new Vector3(weaponPos.x * -1, weaponPos.y, weaponPos.z);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            this.gameObject.transform.localPosition = new Vector3(weaponPos.x, weaponPos.y, weaponPos.z);
        }

        if (isLaser == true)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = rotation;
        }
    }
}
