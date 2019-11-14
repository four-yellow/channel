using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    public float smoothTime = 1F;
    private Vector3 velocity = Vector2.zero;

    public Transform LeftWall;
    public Transform RightWall;
    public Transform UpWall;
    public Transform DownWall;

    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    public float cameraOffset;
    public float ycameraOffset;

    void Start()
    {
        ycameraOffset = cameraOffset / 1.78f;
        xMin = LeftWall.position.x + cameraOffset;
        xMax = RightWall.position.x - cameraOffset;
        yMin = DownWall.position.y + ycameraOffset;
        yMax = UpWall.position.y - ycameraOffset;
    }

    void FixedUpdate()
    {
        if (player.transform.position.y < yMin && player.transform.position.x < xMin) // bottom left corner
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(xMin, yMin, -6), ref velocity, smoothTime);
        }
        else if (player.transform.position.y < yMin && player.transform.position.x > xMax) // bottom right corner
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(xMax, yMin, -6), ref velocity, smoothTime);
        }
        else if (player.transform.position.y > yMax && player.transform.position.x < xMin) // top left corner
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(xMin, yMax, -6), ref velocity, smoothTime);
        }
        else if (player.transform.position.y > yMax && player.transform.position.x > xMax) // top right corner 
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(xMax, yMax, -6), ref velocity, smoothTime);
        }
        else if (player.transform.position.y < yMin) // not corners
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x, yMin, -6), ref velocity, smoothTime);
        }
        else if (player.transform.position.y > yMax)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x, yMax, -6), ref velocity, smoothTime);
        }
        else if (player.transform.position.x < xMin)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(xMin, player.transform.position.y, -6), ref velocity, smoothTime);
        }
        else if (player.transform.position.x > xMax)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(xMax, player.transform.position.y, -6), ref velocity, smoothTime);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -6), ref velocity, smoothTime);

        }
    }
}
