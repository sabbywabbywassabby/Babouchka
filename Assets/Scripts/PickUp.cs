using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject PickUpText;
    public GameObject FlashLightOnPlayer;

    public void SetKey(GameObject k)
    {
        FlashLightOnPlayer = k;
    }
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

    public void Drop()
    {
   
            // Désactivez la clé attachée au joueur
        if (FlashLightOnPlayer != null)
            {
                FlashLightOnPlayer.SetActive(false);

                // Réinitialisez la position de la clé devant le joueur
                // Vous pouvez définir une position spécifique pour le réapparition de la clé
                // Par exemple, utilisez la position du joueur pour le placer devant lui
                Vector3 dropPosition = FlashLightOnPlayer.transform.position + transform.forward * 2.0f; // À 2 unités devant le joueur
                this.gameObject.transform.position = dropPosition;
                this.gameObject.SetActive(true);

            }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
