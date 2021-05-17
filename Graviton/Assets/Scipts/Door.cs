using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject closed;
    private GameObject open;

    void Start()
    {
        closed = gameObject.transform.Find("Closed").gameObject;
        open = gameObject.transform.Find("Open").gameObject;
        closeDoor();
    }

    private void closeDoor() {
        closed.SetActive(true);
        open.SetActive(false);
    }

    private void openDoor() {
        closed.SetActive(false);
        open.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
            openDoor();   
    }
}
