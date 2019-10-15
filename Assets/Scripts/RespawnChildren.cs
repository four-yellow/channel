using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnChildren : MonoBehaviour
{
    List<GameObject> children = new List<GameObject>();

    void Start()
    {
        foreach (Transform t in transform)
        {
            Debug.Log("I made a child");
            children.Add(t.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
        if(collision.otherCollider.gameObject.CompareTag("Player"))
        {
            foreach (GameObject g in children)
            {
                g.SetActive(true);
            }
        }

    }
}
