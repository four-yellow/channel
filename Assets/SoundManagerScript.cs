using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        audioSrc.PlayOneShot(Resources.Load<AudioClip>(clip));

    }
}
