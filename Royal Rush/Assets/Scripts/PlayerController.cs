using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = System.Numerics.Vector3;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpForce = 10f;
    public bool canJump;
    public Vector2 moveInput;
    private TouchingDirections touchingDirections;
    
    //player death
    private UnityEngine.Vector3 respawnPoint;

    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving /*&& !touchingDirections.IsOnWall*/)
            {
                return walkSpeed;
            }
            else
            {
                // idle speed is 0
                return 0;
            }
        }
    }
    
    
    [SerializeField]
    private bool _isMoving = false; // controls whether player is moving for animation
    
    public bool IsMoving {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        } 
    }
   
    
    [SerializeField]
    private LayerMask jumpableGround;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private CapsuleCollider2D touchingCollider;
    
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        touchingDirections = GetComponent<TouchingDirections>();
        //touchingCollider = GetComponent<CapsuleCollider2D>();
        respawnPoint = transform.position;
    }
    
    // Use instead of standard Update()
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        animator.SetFloat("yVelocity", rb.velocity.y);
        
        
        // check if the player is moving left, and mirror the sprite
        if (moveInput.x < 0f)
        {
            sprite.flipX = true; // true = left
        }
        else if (moveInput.x > 0f)
        {
            sprite.flipX = false; // false = right
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO check if alive as well
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // respawn player if they touch the water
        if (collision.CompareTag("WaterDetector"))
        {
            transform.position = respawnPoint;
        }

        //respawn player if they reach the finish line
        if (collision.CompareTag("Finish"))
        {
            transform.position = respawnPoint;
        }

        // respawn player when touching the enemy
        if (collision.CompareTag("Enemy"))
        {
            transform.position = respawnPoint;
        }
    }
    
    
}
