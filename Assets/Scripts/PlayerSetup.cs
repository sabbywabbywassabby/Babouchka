using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using SojaExiles;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private Behaviour[] componentsToDisable;
    

    private GameObject DoorsParent;
    private GameObject gold_key;
    private GameObject blue_key;
    private GameObject red_key;
    private GameObject black_key;
    private GameObject green_key;

    public GameObject Flash;
    public GameObject blue;
    public GameObject black;
    public GameObject red;
    public GameObject green;

    public Controleur_Bryan script;
    public ia_babou script_babou;
    Camera sceneCamera;
    private GameObject Player_Serveur;
    private GameObject Player_Client;
    
    private void Start()
    {
        
        DoorsParent = GameObject.Find("DoorsParent");
        gold_key = GameObject.Find("key");
        blue_key = GameObject.Find("blue_key");
        red_key = GameObject.Find("red_key");
        black_key = GameObject.Find("black_key");
        green_key = GameObject.Find("green_key");

       
        PickUp p = gold_key.GetComponentInChildren<PickUp>();
        PickUp p1 = black_key.GetComponent<PickUp>();
        PickUp p2 = blue_key.GetComponent<PickUp>();
        PickUp p3 = red_key.GetComponent<PickUp>();
        PickUp p4 = green_key.GetComponent<PickUp>();

        script_babou = GameObject.Find("Babouchka").GetComponent<ia_babou>();
        

        if (!isLocalPlayer)
        {
            gameObject.name = "Player_2";

            p.enabled = false;
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {

            gameObject.name = "Player_1";
            p.Player = this.gameObject;
            p1.Player = this.gameObject;
            p2.Player = this.gameObject;
            p3.Player = this.gameObject;
            p4.Player = this.gameObject;

            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

            
            p.SetKey(Flash);
            p1.SetKey(black);
            p2.SetKey(blue);
            p3.SetKey(red);
            p4.SetKey(green);
           
            //initialiser la variable du joueur dans les differentes portes
            foreach(Transform c in DoorsParent.transform)
            {
                opencloseDoor doorComponent1 = c.GetComponentInChildren<opencloseDoor>();
                opencloseDoor1 doorComponent2 = c.GetComponentInChildren<opencloseDoor1>();
                KeyPad_Door doorComponent3 = c.GetComponentInChildren<KeyPad_Door>();
                Garage_Door doorComponent4 = c.GetComponentInChildren<Garage_Door>();
                door_blue doorComponent5 = c.GetComponentInChildren<door_blue>();
                door_black doorComponent6 = c.GetComponentInChildren<door_black>();
                door_green doorComponent7 = c.GetComponentInChildren<door_green>();
                door_red doorComponent8 = c.GetComponentInChildren<door_red>();

                if (doorComponent1 != null) {
                doorComponent1.SetPlayer(transform);
                }
                else if(doorComponent2 != null) {
                doorComponent2.SetPlayer(transform);
                }
                else if (doorComponent3 != null)
                {
                    doorComponent3.SetPlayer(transform);
                }
                else if (doorComponent4 != null)
                {
                    doorComponent4.SetPlayer(transform);
                }
                else if (doorComponent5 != null){
                    doorComponent5.SetPlayer(transform);
                }
                else if (doorComponent6 != null) {
                    doorComponent6.SetPlayer(transform);
                }
                else if (doorComponent7 != null) {
                    doorComponent7.SetPlayer(transform);
                }
                else if (doorComponent8 != null) {
                    doorComponent8.SetPlayer(transform);
                }                
            }
        }
    }

    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        Player_Client = GameObject.Find("Player_1");
        Player_Serveur = GameObject.Find("Player_2");
        if (Player_Client != null && Player_Serveur != null && isLocalPlayer)
        {
            
        }

        
        script.enabled = true;
        script_babou.enabled = true;
    }
}
