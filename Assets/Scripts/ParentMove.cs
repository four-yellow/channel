using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentMove : MonoBehaviour
{
    public bool hungry;
    public float speed;
    private static readonly float[] parentMoveToX = { 8, 0 };
    private static readonly float[] parentMoveToY = { -3, -3 };
    private float upBound, downBound, rightBound, leftBound;
    public int count;
    public bool obstacleInWay; // raycast hit
    public Vector3 direction;
    public float rayDistance = 2f;

    private void Start()
    {
        count = 0;
        hungry = UseKey.hunger;
    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        hungry = UseKey.hunger;
        if (!hungry)
        {
            upBound = parentMoveToY[count] + (float).1;
            downBound = parentMoveToY[count] - (float).1;
            rightBound = parentMoveToX[count] + (float).1;
            leftBound = parentMoveToX[count] - (float).1;

            Vector3 checkpoint = new Vector3(parentMoveToX[count], parentMoveToY[count]);
            direction = (checkpoint - transform.position).normalized;
            obstacleInWay = Physics2D.Raycast(transform.position, direction, rayDistance);


            if (transform.position.y >= upBound || transform.position.y <= downBound ||
                transform.position.x >= rightBound || transform.position.x <= leftBound)
            {
                if (!obstacleInWay)
                    transform.Translate(direction * speed * Time.deltaTime);
            }
            else if (count != 1) { count++; }

            hungry |= count == parentMoveToX.Length + 1; // change parentMoveToX.Length+1 to split path into sections
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, direction);
    }
}

