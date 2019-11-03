using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentMove : MonoBehaviour
{
    [System.Serializable]
    public struct WayPoint
    {
        public GameObject game;
        public bool bl;
    }
    public bool hungry;
    private float speed = 3;
    public WayPoint[] parentMoveToXY;
    private float upBound, downBound, rightBound, leftBound;
    private int count;
    private bool obstacleInWay; // raycast hit
    private Vector3 direction;
    public float rayDistance = 2f;

    //We will condense our X,Y coordinates into a new struct (as well as record whether or not the parent
    //is hungry at the start of this path.
    struct Coord
    {
        public float x;
        public float y;
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
        }

        count = 0;
        hungry = Path[count].startHungry;
    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if (!hungry)
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
                if (!obstacleInWay)
                    transform.Translate(direction * speed * Time.deltaTime);
            }
            else { count++;  hungry = Path[count].startHungry; }

        }
    }

    public void changeHunger(bool h)
    {
        hungry = h;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, direction);
    }
}

