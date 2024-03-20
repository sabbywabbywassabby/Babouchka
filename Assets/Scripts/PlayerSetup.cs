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
    Camera sceneCamera;
    private GameObject Plafond;
    private void Start()
    {
        Plafond = GameObject.Find("Plafond (1)");
        
        
        Debug.Log("FindPLafind");
        Plafond.SetActive(true);
        DoorsParent = GameObject.Find("DoorsParent");
        Key = GameObject.Find("key");

        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

            PickUp p = Key.GetComponentInChildren<PickUp>();
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
    
}
