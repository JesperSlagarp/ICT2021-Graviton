using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Vector2 mousePos;
    private Vector3 weaponPos;
    private SpriteRenderer weaponSprite;
    private GameObject player;
    private int defaultSortingOrder;

    void Start()
    {
        player = GameObject.Find("Player");
        weaponPos = this.gameObject.transform.localPosition;
        weaponSprite = this.gameObject.GetComponent<SpriteRenderer>();
        defaultSortingOrder = weaponSprite.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        Vector2 transpos = transform.position;
        Vector2 playerpos = player.transform.position;
        Vector2 lookDir = mousePos - transpos;
        Vector2 playerLookDir = mousePos - playerpos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        float playerAngle = Mathf.Atan2(playerLookDir.y, playerLookDir.x) * Mathf.Rad2Deg;

        if (playerLookDir.x < 0)
        {
            this.gameObject.transform.localScale = new Vector3(1, -1, 1);
            this.gameObject.transform.localPosition = new Vector3(weaponPos.x * -1, weaponPos.y, weaponPos.z);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            this.gameObject.transform.localPosition = new Vector3(weaponPos.x, weaponPos.y, weaponPos.z);
        }
       

        if (Input.GetKey("w")) // if player faces forward, hide gun
        {
            weaponSprite.sortingOrder = defaultSortingOrder - 1;
        }
        else
        {
            weaponSprite.sortingOrder = defaultSortingOrder;
        }

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;
    }
}