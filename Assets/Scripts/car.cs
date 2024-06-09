using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    private Transform player;
    private Controleur_Bryan controleur;
    private bool petrol_in;

    public GameObject Need_Petrol;
    public GameObject Need_Key_Car;
    public GameObject No_Gaz_sigle;
    public GameObject Click;
    public GameObject key_inventaire;
    // Start is called before the first frame update
    void Start()
    {
        petrol_in = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player_1") != null)
        {
            player = GameObject.Find("Player_1").transform;
            SetPlayer(player);
        }
    }

    public void SetPlayer(Transform p)
    {
        player = p;
        controleur = player.GetComponent<Controleur_Bryan>();
    }

    private void OnMouseOver()
    {
        // Vérifier si le joueur est proche et a la clé active
        if (player && !key_inventaire.activeSelf)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            if (dist < 5)
            {
                Need_Petrol.SetActive(true);
                No_Gaz_sigle.SetActive(true);
            }
                
        }

        // Vérifier si le joueur est proche et a la clé active
        if (player && key_inventaire.activeSelf)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            if (dist < 5)
            {
                if (petrol_in == false)
                {
                    Click.SetActive(true);
                    // Si le joueur clique et a l'autorité sur l'objet, ouvrir la porte
                    if (Input.GetMouseButtonDown(0))
                    {
                        No_Gaz_sigle.SetActive(false);
                    }
                }
                else
                {
                    if (petrol_in)
                    {
                        Need_Key_Car.SetActive(true);
                        // Si le joueur clique et a l'autorité sur l'objet, fermer la porte
                        if (Input.GetMouseButtonDown(0))
                        {
                            
                        }
                    }
                }
            }
        }
    }

    private void OnMouseExit()
    {
        
        Need_Petrol.SetActive(false);
        No_Gaz_sigle.SetActive(false);
        Click.SetActive(false);
        Need_Key_Car.SetActive(false);
    }
}
