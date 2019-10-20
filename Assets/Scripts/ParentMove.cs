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
    //private Vector3 goal = new Vector3(parentMoveToX[parentMoveToX.Length], parentMoveToY[parentMoveToY.Length]);


    private void Start()
    {
        count = 0;
        hungry = UseKey.hunger;
    }

    private void Update()
    {
        hungry = UseKey.hunger;
        if (!hungry)
        {
            upBound = parentMoveToY[count] + (float).1;
            downBound = parentMoveToY[count] - (float).1;
            rightBound = parentMoveToX[count] + (float).1;
            leftBound = parentMoveToX[count] - (float).1;

            Vector3 checkpoint = new Vector3(parentMoveToX[count], parentMoveToY[count]);
            Vector3 direction = (checkpoint - transform.position).normalized;

            if (transform.position.y >= upBound || transform.position.y <= downBound ||
                transform.position.x >= rightBound || transform.position.x <= leftBound)
                transform.Translate(direction * speed * Time.deltaTime);
            else if (count != 1) { count++; }

            hungry |= count == parentMoveToX.Length + 1; // change parentMoveToX.Length+1 to split path into sections
        }


        //UseKey.hunger |= transform.position == goal;







        //hungry = UseKey.hunger;
        //if (!hungry)
        //{
        //    int i = 0;
        //    Vector3 goal = new Vector3(parentMoveToX[parentMoveToX.Length], parentMoveToY[parentMoveToY.Length]);
        //    Vector3 checkpoint = new Vector3(parentMoveToX[0], parentMoveToY[0]);
        //    Vector3 direction = (checkpoint - transform.position).normalized;

        //    if (transform.position != checkpoint)
        //        transform.Translate(direction * speed * Time.deltaTime);
        //    else i++;

        //    UseKey.hunger |= transform.position == goal;
        //}




        //for (int i = 0; i < parentMoveToX.Length; i++)
        //{
        //    Vector3 goal = new Vector3(parentMoveToX[i], parentMoveToY[i]);

        //    while (transform.position != goal)
        //    {
        //        Vector3 direction = goal - transform.position;
        //        transform.Translate(direction * speed * Time.deltaTime);
        //    }
        //}
    }
}

