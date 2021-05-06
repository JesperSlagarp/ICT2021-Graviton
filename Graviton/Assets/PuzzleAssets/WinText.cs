using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinText : MonoBehaviour
{

    private int points = 8;
    private int current;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(current >= points)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Addpoints()
    {
        current++;
    }
}
