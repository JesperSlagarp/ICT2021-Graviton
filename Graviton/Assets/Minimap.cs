using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private bool visitedRoom = false;
    private SpriteRenderer spriterenderer;
    private BoxCollider2D bosscollider;
    public Sprite currentRoomIcon; // room icon with a player sprite in it
    public Sprite lastRoomIcon; // room icon without player sprite in it
    public Sprite unvisitedRoomIcon;

    void Awake()
    {
        //roomIcon2 = roomIcon.GetComponent<SpriteRenderer>();
        //playerIcon2 = playerIcon2.GetComponent<SpriteRenderer>();
        spriterenderer = GetComponent<SpriteRenderer>();
        //bosscollider = GameObject.Find("Boss").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) // if player walks into a room, update the sprite for the minimap
    {
        if(collider.gameObject.tag == "Player")
        {
            spriterenderer.sprite = currentRoomIcon;
            //playerIconShown = Instantiate(playerIcon, this.gameObject.transform.position, Quaternion.identity);
            //roomIconEmpty = Instantiate(roomIcon, this.gameObject.transform.position, Quaternion.identity);
            /*if (visitedRoom == false)
            {
                int mask = (1 << LayerMask.NameToLayer("Minimap")) | (1 << LayerMask.NameToLayer("obstacles"));
                RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.right, Mathf.Infinity, mask);
                Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.right, Color.red, 10f);
                if (hit.collider.gameObject.tag == "Room")
                {
                    Debug.Log("room found");
                    hit.collider.gameObject.GetComponent<Minimap>().ShowUnvisitedRoom();
                    //Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
                }
                hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.right * -1, Mathf.Infinity, mask);
                Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.right * -1, Color.red, 10f);
                if (hit.collider.gameObject.tag == "Room")
                {
                    Debug.Log("room found");
                    hit.collider.gameObject.GetComponent<Minimap>().ShowUnvisitedRoom();
                    //Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
                }
                hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.up, Mathf.Infinity, mask);
                Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.up, Color.red, 10f);
                if (hit.collider.gameObject.tag == "Room")
                {
                    Debug.Log("room found");
                    hit.collider.gameObject.GetComponent<Minimap>().ShowUnvisitedRoom();
                    //Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
                }
                hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.up * -1, Mathf.Infinity, mask);
                Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.up * -1, Color.red, 10f);
                if (hit.collider.gameObject.tag == "Room")
                {
                    Debug.Log("room found");
                    hit.collider.gameObject.GetComponent<Minimap>().ShowUnvisitedRoom();
                    //Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
                }
            }
            visitedRoom = true;*/
            Debug.Log("visited room");
        }
    }

    void OnTriggerExit2D(Collider2D collider) // if the player exits the room, change the sprite for the minimap
    {
        if (collider.gameObject.tag == "Player")
        {
            spriterenderer.sprite = lastRoomIcon;
            //Destroy(playerIconShown);
            //roomIcon2.enabled = false;
            //Instantiate(unvisitedRoomIcon, this.gameObject.transform.position, Quaternion.identity);
        }
    }

    public void ShowUnvisitedRoom()
    {
        if (visitedRoom == false)
        {
            spriterenderer.sprite = unvisitedRoomIcon;
        }
    }
}
