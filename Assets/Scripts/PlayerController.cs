using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerControllerScript : MonoBehaviour
{
    [Header("Stats")]
    //public CameraController camControl;
    public float speed = 6f;
    public float acceleration = 8f;
    public float deceleration = 1f;
    public float horizontal;
    public float vertical;
    public float jumpForce = 20f;
    public float maxYVelocity;
    public Rigidbody2D myRB;
    public bool facingRight = true;
    private bool moving = false;


    public GameObject playerSpriteObject;

    [Header("Grounded")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    [Header("Death Values")]
    public Transform playerSpawn;

    [Header("PhysicMats")]
    public Collider2D myCol;
    public PhysicsMaterial2D slide;
    public PhysicsMaterial2D stop;

    [Header("CoyoteTime & Jump Buffer")]
    public float coyoteTime = 0.3f;
    public float coyoteTimeTimer;
    public float jumpBuffer = 0.3f;
    public float jumpBufferTimer;
    public float airControlFactor = 0.5f;

    void Start()
    {
     //   camControl = GameObject.Find("CameraController").GetComponent<CameraController>();
        transform.position = playerSpawn.position;
    }

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        Move();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
            jumpBufferTimer = 0f;
        }
    }
    public void Move()
    {
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical).normalized * speed * Time.deltaTime;

        if (IsGrounded())
        {
            if (horizontal != 0)
                myRB.velocity = new Vector2(Mathf.MoveTowards(myRB.velocity.x, horizontal * speed, acceleration * Time.deltaTime), myRB.velocity.y);

            if (horizontal == 0 && myRB.velocity.x != 0)
                myRB.velocity = new Vector2(Mathf.MoveTowards(myRB.velocity.x, 0f, deceleration * Time.deltaTime), myRB.velocity.y);
            transform.Translate(movement);
            
        }
        else
        {
            // Apply air control (optional)
            transform.Translate(movement * 0.5f, Space.Self);
        }
 
        //swap facing direction
        if (horizontal < -0.1f && facingRight)
        {
            Flip();
        }
        if (horizontal > 0.1f && !facingRight)
        {
            Flip();
        }

        if (IsGrounded())
        {
    //        camControl.DeadZoneOff();
            coyoteTimeTimer = coyoteTime;
        }

        else
        {
      //      camControl.DeadZoneOn();
            coyoteTimeTimer -= Time.deltaTime;
        }
    }


    public bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        return colliders.Length > 0;
    }

public void Flip()
    {

        Vector3 currentScale = playerSpriteObject.transform.localScale;
  
            if (facingRight)
            {
                currentScale.x = -1;
                playerSpriteObject.transform.localScale = currentScale;
                facingRight = false;
            }
            else if (!facingRight)
            {
                currentScale.x = 1;
                playerSpriteObject.transform.localScale = currentScale;
                facingRight = true;
            }
    }

    public void Jump()
    {
        Debug.Log("Jumped");
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);

    }
 

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //screenshake for camera incase of death
            //camControl.ScreenShake(10f, 0.3f, 10f);
            transform.position = playerSpawn.position;
        }


    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            playerSpawn.position = collision.transform.position;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Death();
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {

    }



    public void Death()
    {
    }





}