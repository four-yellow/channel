using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBounds : MonoBehaviour
{
    public GameObject g;
    static BoxCollider2D Top;
    static BoxCollider2D Bottom;
    static BoxCollider2D Left;
    static BoxCollider2D Right;
    // Start is called before the first frame update
    void Start()
    {
        Bounds bounds = this.gameObject.GetComponent<SpriteRenderer>().sprite.bounds;
        float MinX = bounds.min.x;
        float MinY = bounds.min.y;
        float MaxX = bounds.max.x;
        float MaxY = bounds.max.y;
        float width = MaxX - MinX;
        float height = MaxY - MinY;
        Vector3 blank = new Vector3(0, 0, 0);
        Quaternion q = new Quaternion(0, 0, 0, 0);


        GameObject topWall   = Instantiate(g, blank, q);
        Top = topWall.GetComponent<BoxCollider2D>();
        GameObject botWall   = Instantiate(g, blank, q);
        Bottom = botWall.GetComponent<BoxCollider2D>();
        GameObject leftWall  = Instantiate(g, blank, q);
        Left = leftWall.GetComponent<BoxCollider2D>();
        GameObject rightWall = Instantiate(g, blank, q);
        Right = rightWall.GetComponent<BoxCollider2D>();

        Top.offset = new Vector2(0, (height / 2f) + 1);
        Top.size = new Vector2(width, 2f);
        Bottom.offset = new Vector2(0, -((height / 2f) + 1));
        Bottom.size = new Vector2(width, 2f);
        Right.offset = new Vector2((width / 2f) + 1, 0);
        Right.size = new Vector2(2f, height);
        Left.offset = new Vector2(-((width / 2f) + 1), 0);
        Left.size = new Vector2(2f, height);

    }
}
