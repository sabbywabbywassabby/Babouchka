using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Garage_Door : NetworkBehaviour
{
    public Animator Open_Garage;
    public bool open;
    public GameObject OpenNeed;
    public GameObject Click;

    // R�f�rences pour la synchronisation
    public Transform player;
    public GameObject key_in_inventaire;
    public Image imageToFade; // L'image à faire apparaître
    public float fadeDuration = 2f;
    public GameObject txt;
    public GameObject voiture_need;
    public GameObject voiture;
    public car voiture_controleur;


    private void Start()
    {
        open = false;
        voiture_controleur = voiture.GetComponent<car>();
    }

    // Fonction pour d�finir le joueur qui interagit avec la porte
    public void SetPlayer(Transform p)
    {
        player = p;
        
    }

    private void OnMouseOver()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        if (player && (dist < 5))
        {
            if (voiture_controleur.petrol_in)
            {
                voiture_need.SetActive(true);
            }

            else if (!open)
            {
                    if(key_in_inventaire.activeSelf){
                        Click.SetActive(true);
                    }
                    else{
                        OpenNeed.SetActive(true);
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                           
                        if (key_in_inventaire.activeSelf)
                        {
                            Click.SetActive(false);
                            CmdOpenDoor();
                            open = true;
                            
                        }
                    }
                
            }
        }
    }

    private void OnMouseExit()
    {
        OpenNeed.SetActive(false);
        Click.SetActive(false);
        voiture_need.SetActive(false);
    }

    // Commande pour ouvrir la porte c�t� serveur
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
        yield return new WaitForSeconds(1f);
        // Assurez-vous que l'image est active
        imageToFade.gameObject.SetActive(true);

        // Obtenez la couleur originale de l'image
        Color originalColor = imageToFade.color;

        // Réinitialisez l'alpha à 0 (transparent)
        imageToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        // Interpolez l'alpha de 0 à 1 sur la durée spécifiée
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0.0f, 1.0f, t / fadeDuration);
            imageToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Assurez-vous que l'alpha est bien à 1 à la fin du fade in
        imageToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1.0f);
        txt.SetActive(true);
    }

    
}
