using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    //public Rigidbody2D rb;
    //public Camera cam;
    public Vector2 mousePos;
    private bool negativeDir;
    private bool positiveDir;
    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if (mousePos.x < 0)
        {
            Debug.Log("hey");
        }
        Vector2 transpos = transform.position;
        Vector2 lookDir = mousePos - transpos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; //- 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;
    }
}
