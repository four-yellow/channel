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
    public static bool feedable; // whether the player can feed parent
    public Transform holdpoint; // point where player holds the key
    public List<GameObject> keysInRange; // all keys nearby player
    private List<GameObject>wallsInRange; // all walls nearby player
    private GameObject closestWall; //closest wall to player
    public boolRef h;

    private void Start()
    {
        keysInRange = new List<GameObject>();
        wallsInRange = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
                                                            // stores a wall if it enters the collider of the player
        if (collision.CompareTag("grabbable"))             // adds each key in the vicinity to an array
        {
            keysInRange.Add(collision.gameObject);
        }

        if (collision.CompareTag("Wall"))                  // if a wall is in range and the player has a key, sets unlockable to true
        {
            wallsInRange.Add(collision.gameObject);

            unlockable = grabbed;
        }

        if (collision.CompareTag("parent"))
        {
            feedable = grabbed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("grabbable"))             // removes keys that leave the player's range
        {
            keysInRange.Remove(collision.gameObject);
        }

        if (collision.CompareTag("Wall"))// sets unlockable to false if walls leave the player's range
        {
            wallsInRange.Remove(collision.gameObject);
            unlockable = false;
        }
            
        
        if (collision.CompareTag("parent")) // sets feedable to false if parent leaves the player's range
        {
            feedable = false;
        }
    }

    private void Update()
    {
        float[] distancesK = new float[keysInRange.Count];  // find distances from player of each key in range
        for (int i = 0; i < distancesK.Length; i++)
        {
            distancesK[i] = Vector3.Distance(keysInRange[i].transform.position, this.gameObject.transform.position);
        }

        float minDistance = 999999;                        // find key with minimum distance
        int indexK = 0;
        for (int i = 0; i < distancesK.Length; i++)
        {
            if (distancesK[i] < minDistance)
            {
                minDistance = distancesK[i];
                indexK = i;
            }
        }

        //-----------------------------------WE ARE DOING WALL STUFF HERE-----------------------------------//

        float[] distancesW = new float[wallsInRange.Count];  // find distances from player of each wall in range
        for (int i = 0; i < distancesW.Length; i++)
        {
            distancesW[i] = Vector3.Distance(wallsInRange[i].transform.position, this.gameObject.transform.position);
        }

        minDistance = 999999;                                            // find wall with minimum distance
        int indexW = 0;
        for (int i = 0; i < distancesW.Length; i++)
        {
            if (distancesW[i] < minDistance)
            {
                minDistance = distancesW[i];
                indexW = i;
            }
        }
        if (distancesW.Length > 0)
            closestWall = wallsInRange[indexW];
        if (distancesK.Length > 0)                          // checks that there are keys near the player     
        {
            GameObject closestKey = keysInRange[indexK];
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
                closestKey.transform.position = holdpoint.position;
            }

            if (unlockable && Input.GetKeyDown(KeyCode.E) && closestWall != null && closestKey.GetComponent<Food_tag>() == null) // destroys a wall and uses up key when E is pressed if unlockable
            {
                closestWall.SetActive(false);
                closestKey.SetActive(false);
            }
            else if (feedable && Input.GetKeyDown(KeyCode.E) && h.Val&& closestKey.GetComponent<Food_tag>() != null)
            {
                h.Val = false;
                closestKey.SetActive(false);
            }
        }
    }
}
