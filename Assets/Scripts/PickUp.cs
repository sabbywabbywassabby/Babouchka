using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PickUp : NetworkBehaviour
{
    public GameObject PickUpText;
    public GameObject FlashLightOnPlayer;
    public GameObject GoldKey;
    public GameObject Player;

    public void SetKey(GameObject k)
    {
        FlashLightOnPlayer = k;
    }
    // Start is called before the first frame update
    void Start()
    {
        FlashLightOnPlayer.SetActive(false); 
        PickUpText.SetActive(false);
        GoldKey.SetActive(false);
    }


    
    //souris sur l'objet cle
    private void OnTriggerStay(Collider other)
    {
        // Vérifier si l'objet avec lequel nous entrons en collision est le joueur local
        if (other.gameObject == Player)
        {
            
            // Cet objet est un joueur local
            PickUpText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                PickUpText.SetActive(false);
                
                FlashLightOnPlayer.SetActive(true);
                GoldKey.SetActive(true);
                CmdTake();
            }
        }
    }

    //souris qui pars de la cle
    private void OnTriggerExit(Collider other)
    {
        PickUpText.SetActive(false);
    }

    //fonction pour drop la cle
    public void Drop(Vector3 dropposition)
    {
        if (FlashLightOnPlayer.activeSelf)
        {
            FlashLightOnPlayer.SetActive(false);
            GoldKey.SetActive(false);
            CmdDrop(dropposition);
        }
        
    }

    // Commande pour prendre la cle côté serveur
    [Command(requiresAuthority = false)]
    private void CmdTake()
    {
        RpcTake();
        
    }

    // Commande pour drop la cle cote serveur
    [Command(requiresAuthority = false)]
    private void CmdDrop(Vector3 dropposition)
    {
        RpcDrop(dropposition);
        
    }

    // Rappel pour desactive la cle a terre pour tout les clients
    [ClientRpc]
    private void RpcTake()
    {
        this.gameObject.SetActive(false);
        PickUpText.SetActive(false);
    }

    // Rappel pour activer la cle a terre pour tout les clients
    [ClientRpc]
    private void RpcDrop(Vector3 droposition)
    {
        // Desactivez la cle attachee au joueur
        this.gameObject.transform.position = droposition;
        this.gameObject.SetActive(true);
        
    }

    

}
