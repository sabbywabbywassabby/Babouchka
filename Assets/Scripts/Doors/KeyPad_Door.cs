using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KeyPad_Door : NetworkBehaviour
{
    public Animator openandclose1;
    private bool open;
    public GameObject Pad;
    public GameObject Click;

    // Références pour la synchronisation
    private Transform player;    
    private Controleur_Bryan controleur;
    private string characterName = "Babouchka";

    private void Start()
    {
        open = false;
    }

    // Fonction pour définir le joueur qui interagit avec la porte
    public void SetPlayer(Transform p)
    {
        player = p;
        controleur = player.GetComponent<Controleur_Bryan>();
    }

    private void OnMouseOver()
    {
        // Vérifier si le joueur est proche et a la clé active
        if (player)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            if (dist < 5)
            {
                if (open == false)
                {
                    if(Pad.activeSelf == false)
                    {
                        Click.SetActive(true);
                    }
                    
                    // Si le joueur clique et a l'autorité sur l'objet, ouvrir la porte
                    if (Input.GetMouseButtonDown(0))
                    {
                        Cursor.lockState = CursorLockMode.None;
                        controleur.stop_moving = true;
                        Pad.SetActive(true);
                        Click.SetActive(false);
                    }
                }
                
            }
        }
    }

    private void OnMouseExit()
    {
        Click.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {       
        // Vérifie si "Babouchka" entre en collision avec la porte
        if (other.gameObject.name == "Babouchka")
        {
            // Ouvre la porte
            CmdOpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Vérifie si le personnage "babouchka" sort de la collision avec la porte
        if (other.gameObject.CompareTag(characterName))
        {
            // Ferme la porte
            CmdCloseDoor();
        }
    }

    // Commande pour ouvrir la porte côté serveur
    [Command(requiresAuthority = false)]
    public void CmdOpenDoor()
    {
        RpcPlayOpenAnimation();
    }

    // Commande pour fermer la porte côté serveur
    [Command(requiresAuthority = false)]
    private void CmdCloseDoor()
    {
        RpcPlayCloseAnimation();
    }

    // Rappel pour jouer l'animation d'ouverture de la porte sur tous les clients
    [ClientRpc]
    private void RpcPlayOpenAnimation()
    {
        StartCoroutine(Opening());
    }

    // Rappel pour jouer l'animation de fermeture de la porte sur tous les clients
    [ClientRpc]
    private void RpcPlayCloseAnimation()
    {
        StartCoroutine(Closing());
    }

    private IEnumerator Opening()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controleur.stop_moving = false;
        Pad.SetActive(false);
        openandclose1.Play("Opening 1");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    private IEnumerator Closing()
    {
        print("you are closing the door");
        openandclose1.Play("Closing 1");
        open = false;
        yield return new WaitForSeconds(.5f);
    }
}
