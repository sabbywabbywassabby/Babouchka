using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSoundEffects : MonoBehaviour
{
    public AudioClip opening;
    public AudioClip closing;
    private bool isIn;
    private bool open;
    private AudioSource audioSource;

    void Start()
    {
        isIn = false;
        open = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isIn)
        {
            Debug.Log("Player is in the zone");
            if (Input.GetKeyDown(KeyCode.F))
            {
                open = !open;
                Debug.Log("Player is pressing F and open is: " + open);
                if (open)
                {
                    Debug.Log("Playing opening sound");
                    audioSource.clip = opening;
                    audioSource.Play();
                }
                else
                {
                    Debug.Log("Playing closing sound");
                    audioSource.clip = closing;
                    audioSource.Play();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isIn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isIn = false;
        }
    }
}
