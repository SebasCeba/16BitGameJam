using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;

    private float horizontalInput;
    [SerializeField]
    private float speed = 8f;
    [SerializeField]
    private float jumpingPower = 16f; 
    private bool isFacingRight = true;

    public BlockManager blockManager;

    private void Update()
    {
        if(blockManager.IsBusy())
        {
            return;
        }
        horizontalInput = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // This checks if the player is touching the ground and checks if it could.
        if(Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpingPower);
            anim.SetBool("Jumping", true);
        }

        // Depending on how long or short the player holds the button, they longer/high they jump. 
        if (Input.GetKeyUp(KeyCode.W) && rb2D.velocity.y > 0f)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.5f);
            anim.SetBool("Jumping", true);
        }

        Flip();

        anim.SetBool("Jumping", !IsGrounded());
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(horizontalInput * speed, rb2D.velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if(isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale; 
        }
    }
}
