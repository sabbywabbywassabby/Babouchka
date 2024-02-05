using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject PickUpText;
    public GameObject FlashLightOnPlayer;
    // Start is called before the first frame update
    void Start()
    {
        FlashLightOnPlayer.SetActive(false); 
        PickUpText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickUpText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                FlashLightOnPlayer.SetActive(true);
                PickUpText.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickUpText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
