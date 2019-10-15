using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Implements playge usage of a key. Keys near a player can be picked up with E, with closer keys taking priority,
 * and can be dropped by pressing E again. When near a destroyable wall while holding a key, the player can press E
 * to destroy the wall, using up the key.
 */
public class UseKey : MonoBehaviour
{
    public bool grabbed; // whether the player is holding a key
    public static bool unlockable; // whether the player can destroy a wall
    public Transform holdpoint; // point where player holds the key
    public List<GameObject> keysInRange; // all keys nearby player
    public GameObject adjacentWall; // a wall to destroy near the player

    private void Start()
    {
        keysInRange = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        adjacentWall = collision.gameObject;               // stores a wall if it enters the collider of the player

        if (collision.CompareTag("grabbable"))             // adds each key in the vicinity to an array
        {
            keysInRange.Add(collision.gameObject);
        }

        if (collision.CompareTag("Wall"))                  // if a wall is in range and the player has a key, sets unlockable to true
        {
            if (grabbed)
            {
                unlockable = true;
            }
            else
            {
                unlockable = false;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("grabbable"))             // removes keys that leave the player's range
        {
            keysInRange.Remove(collision.gameObject);
        }

        unlockable &= !collision.CompareTag("Wall");       // sets unlockable to false if no walls leave the player's range
        adjacentWall = null;
    }

    private void Update()
    {
        float[] distances = new float[keysInRange.Count];  // find distances from player of each key in range
        for (int i = 0; i < distances.Length; i++)
        {
            distances[i] = Vector3.Distance(keysInRange[i].transform.position, this.gameObject.transform.position);
        }

        float minDistance = 999999;                        // find key with minimum distance
        int index = 0;
        for (int i = 0; i < distances.Length; i++)
        {
            if (distances[i] < minDistance)
            {
                minDistance = distances[i];
                index = i;
            }
        }

        if (distances.Length > 0)                          // checks that there are keys near the player     
        {
            GameObject closestObj = keysInRange[index];
            if (Input.GetKeyDown(KeyCode.E))               // grabs/drops the key when E is pressed
            {
                if (!grabbed)
                {
                    grabbed = true;
                }
                else
                {
                    grabbed = false;
                }
            }

            if (grabbed)                                   // moves key to the holdpoint of the player
            {
                closestObj.transform.position = holdpoint.position;
            }

            if (unlockable && Input.GetKeyDown(KeyCode.E)) // destroys a wall and uses up key when E is pressed if unlockable
            {
                adjacentWall.SetActive(false);
                closestObj.SetActive(false);
            }
        }
    }
}
