using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHere : MonoBehaviour
{
    public Camera Camera;
    public GameObject Player;

    private float MinY;
    private float MaxY;
    private float MinX;
    private float MaxX;

    public boolRef LezGooo;


    private float Speed = 4.0f;

    void Start()
    {
        CameraControl control = Camera.GetComponent<CameraControl>();
        MinY = control.yMin;
        MinX = control.xMin;
        MaxY = control.yMax;
        MaxX = control.xMax;
        StartCoroutine(MoveToPlayer());
    }

    private IEnumerator MoveToPlayer()
    {
        Vector3 start = Camera.transform.position;
        Vector3 end = Player.transform.position;
        end = new Vector3 (end.x, end.y, start.z);
        float StartTime = Time.time;
        float journeyLength = Vector3.Distance(start, end);
        float DistCovered;
        float fracOfJourney = 0.0f;
        LezGooo.Val = true;

        while (fracOfJourney < 1.0f)
        {
            DistCovered = (Time.time - StartTime) * Speed;
            fracOfJourney = DistCovered / journeyLength;
            Debug.Log(fracOfJourney);
            Vector3 NewPos = Vector3.Slerp(start, end, fracOfJourney);
            Camera.transform.position = new Vector3(Mathf.Clamp(NewPos.x, MinX, MaxX), Mathf.Clamp(NewPos.y, MinY, MaxY), NewPos.z);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        LezGooo.Val = false;


        Camera.GetComponent<CameraControl>().CameraLock = false;
    }
}
