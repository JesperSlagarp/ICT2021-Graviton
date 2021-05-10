using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMouseWorldPos : MonoBehaviour
{
   public static Vector3 GetMousePos(){
        Vector3 vec = GetMousePosZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
   }
   public static Vector3 GetMousePosZ(){
        return GetMousePosZ(Input.mousePosition,Camera.main);
   }
    public static Vector3 GetMousePosZ(Camera worldCamera){
        return GetMousePosZ(Input.mousePosition,worldCamera);
   }
    public static Vector3 GetMousePosZ(Vector3 screenPos, Camera worldCamera){
        Vector2 worldPosition = worldCamera.ScreenToWorldPoint(screenPos);
        return worldPosition;
   }
}
