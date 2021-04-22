using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeAnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private Vector3 lastPos;
    
    void Start()
    {
        animator.SetBool("isMoving", false);
        lastPos = transform.position;
    }

    void FixedUpdate()
    {
        animator.SetBool("isMoving", lastPos != transform.position);
        if(!(lastPos.x == transform.position.x))
            spriteRenderer.flipX = lastPos.x > transform.position.x;

        lastPos = transform.position;
    }
}
