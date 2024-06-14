using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class car : NetworkBehaviour
{
    private Transform player;
    private Controleur_Bryan controleur;

    [SyncVar]
    public bool petrol_in;
    [SyncVar]
    public bool conducteur_in;
    [SyncVar]
    public bool win;



    public GameObject Need_Petrol;
    public GameObject Need_Key_Car;
    public GameObject No_Gaz_sigle;
    public GameObject Click;
    public GameObject key_inventaire;
    public GameObject petrol_inventaire;
    public GameObject conducteur;
    public GameObject passager;
    public GameObject win_canva;
    
    
    public Animator car_anim;
    

    // Start is called before the first frame update
    void Start()
    {
        petrol_in = false;
        conducteur_in = false;
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null && GameObject.Find("Player_1") != null)
        {
            player = GameObject.Find("Player_1").transform;
            SetPlayer(player);
        }
        if(player != null)
        {
            controleur.cam2 = conducteur;
        }
        if (win)
        {
            win_canva.SetActive(true);
        }

    }

    public void SetPlayer(Transform p)
    {
        player = p;
        controleur = player.GetComponent<Controleur_Bryan>();
    }

    private void OnMouseOver()
    {
        // Verify if the player is close and has the key active
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);
        if (dist >= 5) return;

        if (!petrol_in)
        {
            if (petrol_inventaire.activeSelf)
            {
                Click.SetActive(true);
                No_Gaz_sigle.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    petrol_in = true;
                    No_Gaz_sigle.SetActive(false);
                    Need_Petrol.SetActive(false);
                    Click.SetActive(false);
                    petrol_inventaire.SetActive(false);
                    
                }
            }
            else
            {
                Need_Petrol.SetActive(true);
                No_Gaz_sigle.SetActive(true);
            }
        }
        else
        {
            if (true)
            {
                
                Click.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    Click.SetActive(false);
                    if (conducteur_in)
                        passager.SetActive(true);
                    else
                    {
                        conducteur.SetActive(true);
                        conducteur_in = true;
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

    
    
    
    private void StartCar()
    {
        car_anim.SetBool("Moving", true);
    }
}