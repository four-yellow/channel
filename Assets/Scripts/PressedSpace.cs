using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedSpace : MonoBehaviour
{
    private Animator MyAnimator;
    private bool SpacePressed;
    // Start is called before the first frame update
    void Start()
    {
        MyAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SpacePressed && Input.GetKey(KeyCode.Space))
        {
            SpacePressed = true;
            MyAnimator.SetBool("SpacePressed", true);
        }
    }
}
