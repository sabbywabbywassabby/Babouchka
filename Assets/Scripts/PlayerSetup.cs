using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using SojaExiles;
using UnityEngine.UI;

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
    private GameObject petrol_can;

    public GameObject Flash;
    public GameObject blue;
    public GameObject black;
    public GameObject red;
    public GameObject green;
    public AudioClip Sound;

    private AudioSource audioSource;
    public Controleur_Bryan script;
    public ia_babou script_babou;
    Camera sceneCamera;
    private GameObject Player_Serveur;
    private GameObject Player_Client;
    private Image canva_noir;
    private GameObject waiting;
    private bool coroutineStarted = false;

    private void Start()
    {
        waiting = GameObject.Find("Waiting");
        canva_noir = GameObject.Find("Noir").GetComponent<Image>();
        canva_noir.enabled = false;
        DoorsParent = GameObject.Find("DoorsParent");
        gold_key = GameObject.Find("key");
        blue_key = GameObject.Find("blue_key");
        red_key = GameObject.Find("red_key");
        black_key = GameObject.Find("black_key");
        green_key = GameObject.Find("green_key");
        petrol_can = GameObject.Find("Petrol");
        audioSource = GetComponent<AudioSource>();


       
        PickUp p = gold_key.GetComponentInChildren<PickUp>();
        PickUp p1 = black_key.GetComponent<PickUp>();
        PickUp p2 = blue_key.GetComponent<PickUp>();
        PickUp p3 = red_key.GetComponent<PickUp>();
        PickUp p4 = green_key.GetComponent<PickUp>();
        PickUp p5 = petrol_can.GetComponent<PickUp>();

        script_babou = GameObject.Find("Babouchka").GetComponent<ia_babou>();

        

        if (!isLocalPlayer)
        {
            gameObject.name = "Player_2";

            
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
            p5.Player = gameObject;

            //gameObject.GetComponent<Respawn>().enabled = false;

            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

            
            
           
            //initialiser la variable du joueur dans les differentes portes
            foreach(Transform c in DoorsParent.transform)
            {
                opencloseDoor doorComponent1 = c.GetComponentInChildren<opencloseDoor>();
                opencloseDoor1 doorComponent2 = c.GetComponentInChildren<opencloseDoor1>();
                KeyPad_Door doorComponent3 = c.GetComponentInChildren<KeyPad_Door>();
                Garage_Door doorComponent4 = c.GetComponentInChildren<Garage_Door>();
                

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

    public IEnumerator Starter()
    {
        waiting.SetActive(false);
        canva_noir.enabled = true;
        yield return new WaitForSeconds(2f);
        if (isLocalPlayer)
        {
            script.enabled = true;
        }
        
        if (isServer)
        {
            script_babou.enabled = true;
        }
        
        
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        
        audioSource.clip = Sound;
        audioSource.Play();
        Color originalColor = canva_noir.color;
        float fadeDuration = 5f; // Dur�e du fondu en secondes
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1.0f, 0.0f, t / fadeDuration);
            canva_noir.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        canva_noir.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);
        canva_noir.gameObject.SetActive(false); // D�sactive l'image une fois le fondu termin�
    }

    private void Update()
    {
        Player_Client = GameObject.Find("Player_1");
        Player_Serveur = GameObject.Find("Player_2");
        
        if (!coroutineStarted && Player_Client != null && Player_Serveur != null)
        {
            coroutineStarted = true;
            StartCoroutine(Starter());
        }
    }
}
