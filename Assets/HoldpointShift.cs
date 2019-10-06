using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldpointShift : MonoBehaviour
{
    public float horizontal;

    private void Start()
    {
        horizontal = GameObject.Find("Player").GetComponent<PlayerMove>().horDir;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        horizontal = GameObject.Find("Player").GetComponent<PlayerMove>().horDir;
        transform.localPosition = new Vector3(horizontal, 0, 0);
    }
}
