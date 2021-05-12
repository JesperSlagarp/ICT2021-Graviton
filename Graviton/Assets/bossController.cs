using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossController : MonoBehaviour
{
    [SerializeField]
    private GameObject healthbar;

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private GameObject laserMount;

    [SerializeField]
    private GameObject AoE_Magic;

    [SerializeField]
    private GameObject projectileMagic;

    [SerializeField]
    private bool phaseTwo = false;

    [SerializeField]
    private GameObject spriteRight;
    [SerializeField]
    private GameObject spriteLeft;
    [SerializeField]
    private GameObject spriteFront;
    [SerializeField]
    private GameObject spriteBack;

    private float health;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
        spriteRight.SetActive(true);
        spriteLeft.SetActive(false);
        spriteFront.SetActive(false);
        spriteBack.SetActive(false);
        health = maxHealth;
        laserMount.SetActive(false);
        AoE_Magic.SetActive(false);
        projectileMagic.SetActive(false);
        Debug.Log("All false");
    }

    private void FixedUpdate()
    {
        if (health < maxHealth / 2)
            phaseTwo = true;
        if (phaseTwo)
        {
            Debug.Log("phase 2");
            laserMount.GetComponent<laserMount>().phaseTwo();
            AoE_Magic.SetActive(true);
            projectileMagic.SetActive(true);
        }

        rotate();

    }

    private void rotate() {
        Vector3 BossToPlayer = player.transform.position - transform.position;
        float flip = 1;
        if (BossToPlayer.y < 0)
            flip = -1;
        float angle = flip * Vector3.Angle(BossToPlayer, Vector3.right);
        if (angle > -45f && angle < 45f)
        {
            spriteRight.SetActive(true);
            spriteLeft.SetActive(false);
            spriteFront.SetActive(false);
            spriteBack.SetActive(false);
        }
        else if (angle > 45f && angle < 135f)
        {

            spriteRight.SetActive(false);
            spriteLeft.SetActive(false);
            spriteFront.SetActive(false);
            spriteBack.SetActive(true);
        }
        else if ((angle > 135f && angle < 180f) || (angle < -135f && angle > -180f))
        {
            spriteRight.SetActive(false);
            spriteLeft.SetActive(true);
            spriteFront.SetActive(false);
            spriteBack.SetActive(false);
        }
        else if (angle > -135f && angle < -45f)
        {
            spriteRight.SetActive(false);
            spriteLeft.SetActive(false);
            spriteFront.SetActive(true);
            spriteBack.SetActive(false);
        }

    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Bullet") 
        { 
            int playerDamage = GameObject.Find("Player").GetComponent<CharStats>().baseDamage; TakeDamage(playerDamage); 
        } 
    }

    public void TakeDamage(int damage)
    {
        if (health < damage || health == 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("HUB level");
        }
        else
        {
            health = health - damage;
            healthbar.transform.localScale = new Vector3(health / maxHealth, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collider) {
        
        if (!collider.CompareTag("Player"))
            return;

        Debug.Log("Player at boss detected");
        laserMount.SetActive(true);
        AoE_Magic.SetActive(false);
        projectileMagic.SetActive(true);  
    }
}
