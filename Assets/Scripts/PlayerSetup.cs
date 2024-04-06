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
    private GameObject Key;
    public GameObject Flash;
    public Controleur_Bryan script;
    Camera sceneCamera;
    private GameObject Player_Serveur;
    private GameObject Player_Client;
    
    private void Start()
    {
        
        DoorsParent = GameObject.Find("DoorsParent");
        Key = GameObject.Find("key");
        PickUp p = Key.GetComponentInChildren<PickUp>();

        

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
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

            
            p.SetKey(Flash);
           
            foreach(Transform c in DoorsParent.transform)
            {
                opencloseDoor doorComponent1 = c.GetComponentInChildren<opencloseDoor>();
                opencloseDoor1 doorComponent2 = c.GetComponentInChildren<opencloseDoor1>();

                if(doorComponent1 != null) {
                doorComponent1.SetPlayer(transform);
                }
                else if(doorComponent2 != null) {
                doorComponent2.SetPlayer(transform);
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
            script.enabled = true;
        }
    }
}
