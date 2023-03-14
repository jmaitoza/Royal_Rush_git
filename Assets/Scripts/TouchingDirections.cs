using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


// uses the collider to check directions to see if the obj is currently on the ground, touching a wall, or ceiling
public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter; //check to see if player is on ground/wall layer
    public float groundDist = 0.05f;
    public float wallDistance = 0.2f;
    
    private CapsuleCollider2D touchingCollider;
    private Animator animator;

    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    
    [SerializeField]
    private bool _isGrounded;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool("isGrounded", value);
        }
    }
    
    [SerializeField]
    private bool _isOnWall;

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left; // true then right, else false

    public bool IsOnWall
        {
            get
            {
                return _isOnWall;
            }
            private set
            {
                _isOnWall = value;
                animator.SetBool("isOnWall", value);
            }
        }

    private void Awake()
    {
        touchingCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    


    void FixedUpdate()
    {
        IsGrounded = touchingCollider.Cast(Vector2.down, castFilter, groundHits, groundDist) > 0;
        IsOnWall = touchingCollider.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
    }

}
