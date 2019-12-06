﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentMove : MonoBehaviour
{
    public bool parentInZone2;

    [System.Serializable]
    public struct WayPoint
    {
        public GameObject game;
        public GameObject[] walls;
        public bool bl;
    }
    private float speed = 3;
    public WayPoint[] parentMoveToXY;
    private float upBound, downBound, rightBound, leftBound;
    private int count;
    private bool obstacleInWay; // raycast hit
    private Vector3 direction;
    public float rayDistance = 2f;
    public boolRef h;

    //We will condense our X,Y coordinates into a new struct (as well as record whether or not the parent
    //is hungry at the start of this path.

    //A new value is being added to better decide when to move. An array will be given with walls.
    //Once all of these are deactivated then our parent will move.
    struct Coord
    {
        public float x;
        public float y;
        public GameObject[] obstacles;
        public bool startHungry;
    }


    private Coord[] Path;

    private void Start()
    {
        //Load our path info
        Path = new Coord[parentMoveToXY.Length];
        for (int i = 0; i < parentMoveToXY.Length; i++)
        {
            Vector3 v = parentMoveToXY[i].game.transform.position;
            (Path[i].x, Path[i].y, Path[i].startHungry) = (v.x, v.y, parentMoveToXY[i].bl);
            Path[i].obstacles = parentMoveToXY[i].walls;
        }

        count = 0;
        h.Val = Path[count].startHungry;
    }

    private bool AllDisabled(GameObject[] objects)
    {
        foreach (GameObject g in objects) { if ((g.activeInHierarchy) == true) return false; }
        return true;

    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if (!h.Val && count < parentMoveToXY.Length)
        {
            upBound = Path[count].y + (float).1;
            downBound = Path[count].y - (float).1;
            rightBound = Path[count].x + (float).1;
            leftBound = Path[count].x - (float).1;

            Vector3 checkpoint = new Vector3(Path[count].x, Path[count].y);
            direction = (checkpoint - transform.position).normalized;
            obstacleInWay = Physics2D.Raycast(transform.position, direction, rayDistance);

            if (transform.position.y >= upBound || transform.position.y <= downBound ||
                transform.position.x >= rightBound || transform.position.x <= leftBound)
            {
                if (AllDisabled(Path[count].obstacles) && !obstacleInWay)
                    transform.Translate(direction * speed * Time.deltaTime);
            }
            else { count++; if (count < parentMoveToXY.Length) h.Val = Path[count].startHungry; }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("zone2"))
        {
            parentInZone2 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("zone2"))
        {
            parentInZone2 = false;
        }
    }
}

