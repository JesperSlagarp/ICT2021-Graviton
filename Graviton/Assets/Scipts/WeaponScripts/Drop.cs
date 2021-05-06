using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject droppedWeapon;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Drop"))
        {
            GameObject weapon = Instantiate(droppedWeapon, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
