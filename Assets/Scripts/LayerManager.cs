using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    private SpriteRenderer MySR;

    [HideInInspector]
    public int MyY { get; set; }
    [HideInInspector]
    public bool overRide;

    [Range(-1.0f,1.0f)]
    public float Adjust;
    private float AdjustCenter;
    private float Fidelity = 1000;
    private float height;
    // Start is called before the first frame update
    void Start()
    {
        overRide = false;
        Bounds b = this.GetComponent<SpriteRenderer>().sprite.bounds;
        height = b.max.y - b.min.y;
        AdjustCenter = (Adjust*(this.transform.lossyScale.y * height)/2);
        MySR = this.GetComponent<SpriteRenderer>();
        Vector3 tmp = this.transform.position;
        MyY = -(int)((AdjustCenter+tmp.y)*Fidelity);
        MySR.sortingOrder = MyY;
    }

    // Update is called once per frame
    void Update()
    {
        if (!overRide)
        AdjustCenter = (Adjust * (this.transform.lossyScale.y * height) / 2);
        Vector3 tmp = this.transform.position;
        MyY = -(int)((AdjustCenter+tmp.y) * Fidelity);
        MySR.sortingOrder = MyY;
    }
}
