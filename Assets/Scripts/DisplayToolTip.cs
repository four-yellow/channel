using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayToolTip : MonoBehaviour
{

    private SpriteRenderer MyTip;



    //Duration of fade-in/out
    private float GainRate = 0.05f;
    private float MyAlpha;
    private bool Load;

    // Start is called before the first frame update
    void Start()
    {
        MyTip = this.GetComponentInChildren<SpriteRenderer>();
        MyAlpha = 0.0f;
        StartCoroutine(ChangeAlpha());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Load = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Load = false;
        }
    }

    private Color UpdateAlpha (Color c,float p)
    {
        return new Color(c.r, c.g, c.b, p);
    }

    private IEnumerator ChangeAlpha()
    {
        while(true)
        {
            if (Load) MyAlpha = Mathf.Clamp(MyAlpha + GainRate, 0.0f, 1.0f);
            else MyAlpha = Mathf.Clamp(MyAlpha - GainRate, 0.0f, 1.0f);

            MyTip.color = UpdateAlpha(MyTip.color,MyAlpha);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
