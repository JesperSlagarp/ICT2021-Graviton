using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleRooms : MonoBehaviour
{
    public GameObject unvisitedRoomIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            int mask = (1 << LayerMask.NameToLayer("Minimap")) | (1 << LayerMask.NameToLayer("obstacles"));
            RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.right, Mathf.Infinity, mask);
            if (hit.collider.gameObject.tag == "Room")
            {
                Debug.Log("room found");
                Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.right, Color.red, 10f);
                Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
            }
            hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.right * -1, Mathf.Infinity, mask);
            if (hit.collider.gameObject.tag == "Room")
            {
                Debug.Log("room found");
                Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
            }
            hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.up, Mathf.Infinity, mask);
            if (hit.collider.gameObject.tag == "Room")
            {
                Debug.Log("room found");
                Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
            }
            hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.up * -1, Mathf.Infinity, mask);
            if (hit.collider.gameObject.tag == "Room")
            {
                Debug.Log("room found");
                Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
            }
    }
}
