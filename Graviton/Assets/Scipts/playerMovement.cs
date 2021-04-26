using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //public GameObject dashEffect;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    private bool isDashButtonDown;
    [SerializeField] private LayerMask dashLayerMask;

    Vector2 movement;
    /*
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }*/
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.sqrMagnitude > 1)
        {
            movement = movement.normalized;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashButtonDown = true;
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);


    }

    //50 times/sec
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        
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
        }
    }

    
}
