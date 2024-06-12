using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class car :NetworkBehaviour
{
    private Transform player;
    private Controleur_Bryan controleur;
    public bool petrol_in;
    public bool info;
    private bool conducteur_in;

    public GameObject Need_Petrol;
    public GameObject Need_Key_Car;
    public GameObject No_Gaz_sigle;
    public GameObject Click;
    public GameObject key_inventaire;
    public GameObject petrol_inventaire;
    public Camera conducteur;
    public Camera passager;
    public Animator car_anim;
    [SyncVar]
    private NetworkIdentity driver;
    [SyncVar]
    private NetworkIdentity passenger;

    // Start is called before the first frame update
    void Start()
    {
        petrol_in = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null && GameObject.Find("Player_1") != null)
        {
            player = GameObject.Find("Player_1").transform;
            SetPlayer(player);
        }
        info = key_inventaire.activeSelf;
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
            if (key_inventaire.activeSelf)
            {
                print("info");
                Click.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    print("enter");
                    CmdEnterCar();
                }
            }
            else
            {
                Need_Key_Car.SetActive(true);
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

    [Command(requiresAuthority = false)]
    private void CmdEnterCar()
    {
        if (driver == null)
        {
            driver = player.GetComponent<NetworkIdentity>();
            RpcSetDriver(driver);
        }
        else if (passenger == null)
        {
            passenger = player.GetComponent<NetworkIdentity>();
            RpcSetPassenger(passenger);
        }
        if (driver != null && passenger != null)
        {
            RpcStartCar();
        }
    }

    [ClientRpc]
    private void RpcSetDriver(NetworkIdentity newDriver)
    {
        driver = newDriver;
        if (driver.isLocalPlayer)
        {
            controleur.cam.enabled = false;
            conducteur.enabled = true;
        }
    }
    [ClientRpc]
    private void RpcSetPassenger(NetworkIdentity newPassenger)
    {
        passenger = newPassenger;
        if (passenger.isLocalPlayer)
        {
            controleur.cam.enabled = false;
            passager.enabled = true;
        }
    }
    [ClientRpc]
    private void RpcStartCar()
    {
        car_anim.SetBool("Moving", true);
    }
}