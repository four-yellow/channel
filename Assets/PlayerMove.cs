using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float moveSpeed;
    Vector2 movement;
    public float minHeight, maxHeight;
    public float minWidth, maxWidth;

    public float horDir; //-1=left, 1=right
    public float verDir; //-1=down, 1=up

    private void Start()
    {
        horDir = 1;
        verDir = 1;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        if (System.Math.Abs(movement.x) > 0)
        {
            horDir = movement.x;
        }

        movement.y = Input.GetAxisRaw("Vertical");
        if (System.Math.Abs(movement.y) > 0)
        {
            verDir = movement.y;
        }

        if (((transform.position.x <= minWidth) && (movement.x <= -1)) || ((transform.position.x >= maxWidth) && (movement.x >= 1)))
        {
            movement.x = 0;
        }
        if (((transform.position.y <= minHeight) && (movement.y <= -1)) || ((transform.position.y >= maxHeight) && (movement.y >= 1)))
        {
            movement.y = 0;
        }
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
