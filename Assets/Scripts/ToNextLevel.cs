using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNextLevel : MonoBehaviour
{
    public bool playerIn;
    public bool parentIn;
    public string levelName;
    public bool animationPlay;
    public bool readyToTransition;

    // Update is called once per frame
    void Update()
    {
        if (playerIn && parentIn)
        {
            readyToTransition = true;
        }
        else readyToTransition = false;

        if (readyToTransition)
        {
            animationPlay = true;
        }
        else
        {
            animationPlay = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerIn |= collision.CompareTag("Player");
        parentIn |= collision.CompareTag("parent");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerIn &= !other.CompareTag("Player");
        parentIn &= !other.CompareTag("parent");
    }
}
