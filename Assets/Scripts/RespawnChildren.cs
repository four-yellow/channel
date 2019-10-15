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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject g in children)
            {
                g.SetActive(false);
                g.SetActive(true);
            }
        }
    }
}
