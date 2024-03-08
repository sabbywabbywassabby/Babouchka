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

    public Component doorscript;

    public GameObject DoorsParent;

    Camera sceneCamera;
    private void Start()
    {
        DoorsParent = GameObject.Find("DoorsParent");

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
