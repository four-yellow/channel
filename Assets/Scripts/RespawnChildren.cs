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
            //Debug.Log("I made a childl");
            children.Add(t.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<UseKey>().ToggleGrabbed(false);
            //Debug.Log("Hit");
            foreach (GameObject g in children)
            {
                g.SetActive(false);
                g.SetActive(true);
            }
        }
    }
}
