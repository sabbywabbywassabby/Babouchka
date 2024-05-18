using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBackground : MonoBehaviour
{
    public AudioSource backgroundAudio;
    // Start is called before the first frame update
    void Start()
    {
        backgroundAudio.Play();   
    }
}
