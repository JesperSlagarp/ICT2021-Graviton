using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAim : MonoBehaviour
{

    [SerializeField]private Transform aimT;
    public Vector2 mousePos;

    private void awake()
    {
        aimT = transform.Find("Aim");
        
    }

   private void Update()
    {
      Vector3 mousePos = GetMouseWorldPos.GetMousePos();
      
      Vector2 lookDir = (mousePos - transform.position).normalized;
      float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
      aimT.eulerAngles = new Vector3(0,0, angle);
      Debug.Log(angle);
    }
}
