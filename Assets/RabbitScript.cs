using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitScript : MonoBehaviour
{
    private float speed = 3;
    public GameObject[] rabbitMoveToXY;
    private float upBound, downBound, rightBound, leftBound;
    private int count;
    private Vector3 direction;

    private bool playerNear;

    //We will condense our X,Y coordinates into a new struct (as well as record whether or not the parent
    //is hungry at the start of this path.

    //A new value is being added to better decide when to move. An array will be given with walls.
    //Once all of these are deactivated then our parent will move.
    struct Coord
    {
        public float x;
        public float y;
    }


    private Coord[] Path;

    private void Start()
    {
        //Load our path info
        Path = new Coord[rabbitMoveToXY.Length];
        for (int i = 0; i < rabbitMoveToXY.Length; i++)
        {
            Vector3 v = rabbitMoveToXY[i].transform.position;
            (Path[i].x, Path[i].y) = (v.x, v.y);
        }

        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerNear = true;
            GetComponent<Animator>().SetBool("NearPlayer", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = false;
            GetComponent<Animator>().SetBool("NearPlayer", false);
        }
    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if (playerNear && count < rabbitMoveToXY.Length)
        {
            upBound = Path[count].y + (float).1;
            downBound = Path[count].y - (float).1;
            rightBound = Path[count].x + (float).1;
            leftBound = Path[count].x - (float).1;

            Vector3 checkpoint = new Vector3(Path[count].x, Path[count].y);
            direction = (checkpoint - transform.position).normalized;

            if (transform.position.y >= upBound || transform.position.y <= downBound ||
                transform.position.x >= rightBound || transform.position.x <= leftBound)
            {
                transform.Translate(direction * speed * Time.deltaTime);
            }
            else { count++;}

        }
    }
}

