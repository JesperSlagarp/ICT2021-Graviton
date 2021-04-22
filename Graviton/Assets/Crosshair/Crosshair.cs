using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseCursosPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursosPos;
    }
}
