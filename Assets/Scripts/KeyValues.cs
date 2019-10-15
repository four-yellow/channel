using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyValues : MonoBehaviour
{

    private Vector3 StartPoint;
    // Start is called before the first frame update
    void Awake()
    {
        StartPoint = this.transform.position;
    }

    private void OnEnable()
    {
        this.transform.position = StartPoint;
    }
}
