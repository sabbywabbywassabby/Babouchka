using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Garage_Door : NetworkBehaviour
{
    public Animator Open_Garage;
    private bool open;
    public GameObject OpenNeed;
    public GameObject Click;

    // Références pour la synchronisation
    public Transform player;
    private GameObject key;
    

    private void Start()
    {
        open = false;
    }

    // Fonction pour définir le joueur qui interagit avec la porte
    public void SetPlayer(Transform p)
    {
        player = p;
        key = player.transform.Find("garage_key").gameObject;
    }

    private void OnMouseOver()
    {
        if (player)
        {
            if (!open)
            {

                float dist = Vector3.Distance(player.position, transform.position);
                if (dist < 5)
                {
                    Click.SetActive(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        
                        if (key.activeSelf)
                        {
                            Click.SetActive(false);
                            CmdOpenDoor();
                        }
                        else
                        { 
                            OpenNeed.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    private void OnMouseExit()
    {
        OpenNeed.SetActive(false);
        Click.SetActive(false);
    }

    // Commande pour ouvrir la porte côté serveur
    [Command(requiresAuthority = false)]
    private void CmdOpenDoor()
    {
        RpcPlayOpenAnimation();
    }

    

    // Rappel pour jouer l'animation d'ouverture de la porte sur tous les clients
    [ClientRpc]
    private void RpcPlayOpenAnimation()
    {
        StartCoroutine(Opening());
    }

    private IEnumerator Opening()
    {
        Open_Garage.SetBool("open", true);
        open = true;
        yield return new WaitForSeconds(.5f);
    }
}
