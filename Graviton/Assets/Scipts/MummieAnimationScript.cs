using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummieAnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private Vector3 lastPos;

    Vector2 movement;

    void Start()
    {
        animator.SetBool("isMoving", false);
        lastPos = transform.position;
    }

    void FixedUpdate()
    {
        movement.x = transform.position.x - lastPos.x;
        movement.y = transform.position.y -lastPos.y;

        
        if(movement.y > 0)
        {
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 1f);
        }
        else if (movement.y < 0)
        {
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", -1f);
        }
        else if (movement.x > 0)
        {
            animator.SetFloat("Horizontal", 1f);
            animator.SetFloat("Vertical", 0);
        }
        else if (movement.x < 0)
        {
            animator.SetFloat("Horizontal", -1f);
            animator.SetFloat("Vertical", 0);
        }

        animator.SetBool("isMoving", lastPos != transform.position);
        if (!(lastPos.x == transform.position.x))
            spriteRenderer.flipX = lastPos.x > transform.position.x;

        lastPos = transform.position;
    }
}