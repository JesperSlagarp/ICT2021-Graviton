using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class temporary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K)) {
            AstarPath.active.Scan();
        }
    }

}
