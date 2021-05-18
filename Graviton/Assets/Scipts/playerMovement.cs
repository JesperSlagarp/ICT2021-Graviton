using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerMovement : MonoBehaviour
{
    //public GameObject dashEffect;
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public Rigidbody2D rb;
    public Animator animator;
    private bool dashOnCooldown;
    private bool isDashing;
    [SerializeField] private LayerMask dashLayerMask;

    private float currSpeed;
    Vector2 movement;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currSpeed = moveSpeed;
        

    }

    private void OnEnable()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /*void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        switch (scene.name) {
            case "HUB level": transform.position = new Vector3(0, 0, 0); break;
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        if (!isDashing)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        if (movement.sqrMagnitude > 1)
        {
            movement = movement.normalized;
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.Space) && !dashOnCooldown)
        {
            //isDashButtonDown = true;
            currSpeed = dashSpeed;
            isDashing = true;
            dashOnCooldown = true;
            gameObject.GetComponent<CharStats>().shield = true;
            Invoke("stopDash", 0.2f);
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);


    }

    private void stopDash()
    {
        isDashing = false;
        currSpeed = moveSpeed;
        gameObject.GetComponent<CharStats>().shield = false;
        Invoke("dashCooldown", 0.5f);
    }

    private void dashCooldown() {
        dashOnCooldown = false;
    }

    //50 times/sec
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currSpeed * Time.fixedDeltaTime);

        /*
        if (isDashButtonDown){

            float dashAmount = 5f;
            Vector2 dashPosition = rb.position + movement * dashAmount;
           // GameObject clone = Instantiate(dashEffect, rb.position, Quaternion.identity);
            //clone.name = "wierd";
            RaycastHit2D raycasthit2D = Physics2D.Raycast(rb.position, movement, dashAmount, dashLayerMask);
            if(raycasthit2D.collider != null)
            {
                dashPosition = raycasthit2D.point;
            }
            rb.MovePosition(rb.position + movement * dashAmount);
            isDashButtonDown = false; 
        }*/
    }

    
}
