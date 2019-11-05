using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    private SpriteRenderer MySR;

    private int MyY;
    // Start is called before the first frame update
    void Start()
    {
        MySR = this.GetComponent<SpriteRenderer>();
        Vector3 tmp = this.transform.position;
        MyY = -(int)tmp.y;
        MySR.sortingOrder = MyY;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmp = this.transform.position;
        MyY = -(int)tmp.y;
        MySR.sortingOrder = MyY;
    }
}
