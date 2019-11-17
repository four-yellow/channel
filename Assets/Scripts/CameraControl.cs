using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    //Honestly don't know how to save this person
    private bool isGracehere;

    private Vector3 pos;
    private Transform target;
    private SpriteRenderer spriteBounds;

    void Start()
    {
        transform.position = GameObject.FindWithTag("Player").transform.position;

        float vertExtent = GetComponent<Camera>().orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        spriteBounds = GameObject.FindWithTag("background").GetComponentInChildren<SpriteRenderer>();

        target = GameObject.FindWithTag("Player").transform;

        //xMin = horzExtent - spriteBounds.sprite.bounds.size.x / 2.0f;
        xMax = spriteBounds.sprite.bounds.size.x / 2.0f - horzExtent;
        xMin = -1 * xMax;
        yMin = vertExtent - spriteBounds.sprite.bounds.size.y / 2.0f;
        yMax = spriteBounds.sprite.bounds.size.y / 2.0f - vertExtent;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log();
        pos = new Vector3(target.position.x, target.position.y, -6);
        pos.x = Mathf.Clamp(pos.x, xMin, xMax);
        pos.y = Mathf.Clamp(pos.y, yMin, yMax);
        transform.position = pos;
    }

}
