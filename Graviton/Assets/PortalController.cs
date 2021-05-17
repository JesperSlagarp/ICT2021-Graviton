using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    private GameObject activatedPortal;

    private bool canActivate = false;
    // Update is called once per frame
    private void Start()
    {
        activatedPortal.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("Pickup") && canActivate)
        {
            activatedPortal.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            canActivate = true;
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            canActivate = false;
    }
}
