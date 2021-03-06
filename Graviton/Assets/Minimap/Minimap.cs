using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private bool visitedRoom = false;
    private SpriteRenderer spriterenderer;
    public Sprite currentRoomIcon; // room icon with a player sprite in it
    public Sprite lastRoomIcon; // room icon without player sprite in it
    public Sprite unvisitedRoomIcon;
    public Sprite unvisitedBossRoomIcon;


    private GameObject boss;

    void Awake()
    {
        //roomIcon2 = roomIcon.GetComponent<SpriteRenderer>();
        //playerIcon2 = playerIcon2.GetComponent<SpriteRenderer>();
        spriterenderer = GetComponent<SpriteRenderer>();
        //bosscollider = GameObject.Find("Boss").GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        
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
                int maskBoss = (1 << LayerMask.NameToLayer("BossDetector"));
                int maskDoor = (1 << LayerMask.NameToLayer("Door"));
                


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
                if (hit.collider != null && hitDoor.collider != null)
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

                hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.right, 65, maskBoss);
                hitDoor = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.right, 65, maskDoor);

                if (hit.collider != null && hitDoor.collider != null)
                {
                    Debug.Log("bossroom found");
                    hit.collider.gameObject.GetComponent<Minimap>().ShowUnvisitedBossRoom();
                    //Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
                }
                hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.right * -1, 65, maskBoss);
                hitDoor = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.right * -1, 65, maskDoor);

                Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.right * -1, Color.red, 10f);
                if (hit.collider != null && hitDoor.collider != null)
                {
                    Debug.Log("bossroom found");
                    hit.collider.gameObject.GetComponent<Minimap>().ShowUnvisitedBossRoom();
                    //Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
                }
                hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.up, 60, maskBoss);
                hitDoor = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.up, 60, maskDoor);

                Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.up, Color.red, 10f);
                if (hit.collider != null && hitDoor.collider != null)
                {
                    Debug.Log("bossroom found");
                    hit.collider.gameObject.GetComponent<Minimap>().ShowUnvisitedBossRoom();
                    //Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
                }
                hit = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.up * -1, 60, maskBoss);
                hitDoor = Physics2D.Raycast(this.gameObject.transform.position, this.gameObject.transform.up * -1, 60, maskDoor);

                Debug.DrawRay(this.gameObject.transform.position, this.gameObject.transform.up * -1, Color.red, 10f);
                if (hit.collider != null && hitDoor.collider != null)
                {
                    Debug.Log("bossroom found");
                    hit.collider.gameObject.GetComponent<Minimap>().ShowUnvisitedBossRoom();
                    //Instantiate(unvisitedRoomIcon, hit.collider.gameObject.transform.position, Quaternion.identity);
                }
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

    public void ShowUnvisitedBossRoom()
    {
        if (visitedRoom == false)
        {
            spriterenderer.sprite = unvisitedBossRoomIcon;
        }
    }
}
