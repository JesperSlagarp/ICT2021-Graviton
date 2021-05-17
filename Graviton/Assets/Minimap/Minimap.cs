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
           
            if (visitedRoom == false)
            {
                Debug.Log("unvisited");
                int mask = (1 << LayerMask.NameToLayer("Minimap"));//| (1 << LayerMask.NameToLayer("obstacles"));
                int maskDoor = (1 << LayerMask.NameToLayer("Door"));
                int maskBoss = (1 << LayerMask.NameToLayer("bossDetector"));
                RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.right, 25, mask);
                RaycastHit2D hitDoor = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.right, 25, maskDoor);
                

                Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.right, Color.red, 10f);
                
                if (hit.collider != null && hitDoor.collider != null)
                {
                    Debug.Log("room found");
                    hit.collider.gameObject.GetComponent<Minimap>().ShowUnvisitedRoom();
                    //Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
                }
                hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.right * -1, 25, mask);
                hitDoor = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.right * -1, 25, maskDoor);
                
                Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.right * -1, Color.red, 10f);
                if (hit.collider != null && hitDoor.collider != null/*hit.collider.gameObject.tag == "Room"*/)
                {
                    Debug.Log("room found");
                    hit.collider.gameObject.GetComponent<Minimap>().ShowUnvisitedRoom();
                    //Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
                }
                hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.up, 15, mask);
                hitDoor = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.up, 15, maskDoor);
                
                Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.up, Color.red, 10f);
                if (hit.collider != null && hitDoor.collider != null)
                {
                    Debug.Log("room found");
                    hit.collider.gameObject.GetComponent<Minimap>().ShowUnvisitedRoom();
                    //Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
                }
                hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.up * -1, 15, mask);
                hitDoor = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.up * -1, 15, maskDoor);
               
                Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.up * -1, Color.red, 10f);
                if (hit.collider != null && hitDoor.collider != null)
                {
                    Debug.Log("room found");
                    hit.collider.gameObject.GetComponent<Minimap>().ShowUnvisitedRoom();
                    //Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
                }

                /*if (Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.right, 15, maskBoss).collider != null) {

                }
                else if ()
                {

                }
                else if ()
                {

                }
                else if () { }*/
                
            }
            visitedRoom = true;
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
