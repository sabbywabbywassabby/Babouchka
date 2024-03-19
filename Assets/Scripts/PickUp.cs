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

    public void Drop(Vector3 droposition)
    {
   
            // D�sactivez la cl� attach�e au joueur
        if (FlashLightOnPlayer != null)
            {
                FlashLightOnPlayer.SetActive(false);

                // R�initialisez la position de la cl� devant le joueur
                // Vous pouvez d�finir une position sp�cifique pour le r�apparition de la cl�
                // Par exemple, utilisez la position du joueur pour le placer devant lui
                    this.gameObject.transform.position = droposition;
                this.gameObject.SetActive(true);

            }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
