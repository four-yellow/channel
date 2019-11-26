using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    private Vector3 pos;
    private Transform target;
    private SpriteRenderer spriteBounds;

    void Start()
    {
        transform.position = new Vector3(GameObject.FindWithTag("Player").transform.position.x,
                                         GameObject.FindWithTag("Player").transform.position.y, -6);

        float vertExtent = GetComponent<Camera>().orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        GameObject bg = GameObject.FindWithTag("background");
        spriteBounds = bg.GetComponentInChildren<SpriteRenderer>();
        Vector3 bgPos = bg.GetComponent<Transform>().position;


        target = GameObject.FindWithTag("Player").transform;

        xMin = horzExtent - (spriteBounds.sprite.bounds.size.x / 2.0f) + bgPos.x;
        xMax = spriteBounds.sprite.bounds.size.x / 2.0f + bgPos.x - horzExtent;
        //xMin = -spriteBounds.sprite.bounds.size.x / 2.0f - bgPos.x + horzExtent;
        yMin = vertExtent - (spriteBounds.sprite.bounds.size.y / 2.0f) + bgPos.y;
        yMax = spriteBounds.sprite.bounds.size.y / 2.0f + bgPos.y - vertExtent;
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
