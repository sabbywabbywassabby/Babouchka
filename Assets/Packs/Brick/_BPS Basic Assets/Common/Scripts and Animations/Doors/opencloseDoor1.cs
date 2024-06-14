using UnityEngine;
using Mirror;
using System.Collections;

namespace SojaExiles
{
    public class opencloseDoor1 : NetworkBehaviour
    {
        public Animator openandclose1;
        [SyncVar]
        private bool open;
        public GameObject OpenNeed;
        public GameObject Click;

        // Références pour la synchronisation
        private Transform player;
        public GameObject key_inventaire;
        private Controleur_Bryan controleur;
        private string characterName = "Babouchka";
        
        // Références pour le son
        public AudioClip openingSound;
        public AudioClip closingSound;
        private AudioSource audioSource;

        

        private void Start()
        {
            open = false;
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if(player == null && GameObject.Find("Player_1") != null)
            {
                player = GameObject.Find("Player_1").transform;
                SetPlayer(player);
            }
        }
        // Fonction pour définir le joueur qui interagit avec la porte
        public void SetPlayer(Transform p)
        {
            player = p;
            controleur = player.GetComponent<Controleur_Bryan>();
        }

        private void OnMouseOver()
        {
            float dist = Vector3.Distance(player.position, transform.position);
            if (dist > 4)
                return;

            // Vérifier si le joueur est proche et a la clé active
            if (player && !key_inventaire.activeSelf)
            {
                OpenNeed.SetActive(true);
            }

            // Vérifier si le joueur est proche et a la clé active
            if (player && key_inventaire.activeSelf)
            {
                Click.SetActive(true);

                if (open == false)// Si le joueur clique et a l'autorité sur l'objet, ouvrir la porte
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Click.SetActive(false);
                        CmdOpenDoor();
                    }
                }
                else
                    {
                    if (open == true)// Si le joueur clique et a l'autorité sur l'objet, fermer la porte
                    {
                        if (Input.GetMouseButtonDown(0))
                            {
                                CmdCloseDoor();
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

        private void OnTriggerEnter(Collider other)
        {
            // Vérifie si le personnage "babouchka" entre en collision avec la porte
            if (other.gameObject.CompareTag(characterName))
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
                StartCoroutine(DelayedOpen(1.5f));
            }
        }

        // Commande pour ouvrir la porte côté serveur
        [Command (requiresAuthority = false)]
        private void CmdOpenDoor()
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
            // joue le son
            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.clip = openingSound;
                audioSource.Play();
            }
            openandclose1.Play("Opening 1");
            open = true;
            yield return new WaitForSeconds(.5f);
        }
        
        private IEnumerator Closing()
        {
            openandclose1.Play("Closing 1");
            open = false;
            // joue le son
            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.clip = closingSound;
                audioSource.Play();
            }
            yield return new WaitForSeconds(.5f);
        }

        

        private IEnumerator DelayedOpen(float delay)
        {
            yield return new WaitForSeconds(delay);
            CmdCloseDoor();
        }
    }
}

