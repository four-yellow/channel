using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabKey : MonoBehaviour
{
    public bool grabbed;
    RaycastHit2D hit;
    public float distance;
    public float horizontal;
    public float vertical;
    public Transform holdpoint;
    public bool horPress;
    public bool verPress;

    private void Start()
    {
        horizontal = GameObject.Find("Player").GetComponent<PlayerMove>().horDir;
        vertical = GameObject.Find("Player").GetComponent<PlayerMove>().verDir;
        horPress = true;
        verPress = false;
    }

    private void Update()
    {
        horizontal = GameObject.Find("Player").GetComponent<PlayerMove>().horDir;
        vertical = GameObject.Find("Player").GetComponent<PlayerMove>().verDir;

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            verPress = true;
            horPress = false;
        }
        else
        {
            horPress = true;
            verPress = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!grabbed)
            { // grab
                Physics2D.queriesStartInColliders = false;
                if ((horizontal <= -1) || (horizontal >= 1))
                {
                    hit = Physics2D.Raycast(transform.position, new Vector2(horizontal, 0) * transform.localScale.x, distance);
                }
                else
                {
                    hit = Physics2D.Raycast(transform.position, new Vector2(0, vertical) * transform.localScale.y, distance);
                }

                if (hit.collider != null)
                {
                    grabbed = true;
                }

            }
            else
            { // drop
                grabbed = false;
            }
        }

        if (grabbed)
        {
            hit.collider.gameObject.transform.position = holdpoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (horPress)
        {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(horizontal, 0) * transform.localScale.x * distance);
        }

        if (verPress)
        {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, vertical) * transform.localScale.y * distance);
        }
    }
}
