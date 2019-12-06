﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Allows the player to move using the arrow keys or WASD. Sets a height and width limit so the player cannot move
 * off the screen, and displays horizontal and vertical inputs with 1 and -1. 
 */
public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator Mine;
    public float moveSpeed;
    Vector2 movement; // tracks direction the player is trying to move in. 

    //Variables deemed irrelevant
    /*
    public float minHeight, maxHeight;
    public float minWidth, maxWidth;     */

    public float horDir; //-1=left, 1=right
    public float verDir; //-1=down, 1=up

    private int direction;
    private void Start()
    {
        Mine = GetComponent<Animator>();
        horDir = 1;
        verDir = 1;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if ((movement.magnitude) <= 0.0f) Mine.SetBool("Walking", false);
        else
        {
            Mine.SetBool("Walking", true);
            if(Input.GetKey(KeyCode.UpArrow))
            {
                direction = 3;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                direction = 0;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction = 2;
            }
            Mine.SetInteger("Direction", direction);
        }
            
        //I don't think these boundaries are necessary
        /*
        // tests if player is trying to move past width boundaries
        if (((transform.position.x <= minWidth) && (movement.x <= -1)) || ((transform.position.x >= maxWidth) && (movement.x >= 1)))
        {
            movement.x = 0;
        }

        // tests if player is trying to move past height boundaries
        if (((transform.position.y <= minHeight) && (movement.y <= -1)) || ((transform.position.y >= maxHeight) && (movement.y >= 1)))
        {
            movement.y = 0;
        }
        */
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime); // moves the player
    }
}
