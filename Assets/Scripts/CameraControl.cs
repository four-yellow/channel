using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    public bool CameraLock;

    private Vector3 pos;
    private Transform target;
    private SpriteRenderer spriteBounds;
    private float Velocity;

    void Start()
    {
        if (!CameraLock)
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
        if (!CameraLock)
        {
            pos = new Vector3(target.position.x, target.position.y, -6);
            pos.x = Mathf.Clamp(pos.x, xMin, xMax);
            pos.y = Mathf.Clamp(pos.y, yMin, yMax);
            transform.position = pos;
        }
        
    }

    public void DoTheThing()
    {
        StartCoroutine(TitleTransition());
    }

    public IEnumerator TitleTransition()
    {
        yield return new WaitForSeconds(0.5f);
        float time = 1.0f;
        while (time > 0.0f)
        {
            pos = new Vector3(target.position.x, target.position.y, -6)
            {
                x = Mathf.SmoothDamp(transform.position.x, target.position.x, ref Velocity, 1.0f),
                y = Mathf.SmoothDamp(transform.position.y, target.position.y, ref Velocity, 1.0f)
            };
            transform.position = pos;
            time -= Time.deltaTime;
            yield return null;
        }
        CameraLock = false;
    }

}
