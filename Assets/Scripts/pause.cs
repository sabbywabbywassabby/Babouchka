using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class pause : MonoBehaviour
{
    public static bool gameispaused = false;
    public GameObject pausemenuUI;
    public GameObject settings;
    public GameObject Controles;
    public Controleur_Bryan control;


    void Update()
    {
        if(control == null)
        {
            control = GameObject.Find("Player_1").GetComponent<Controleur_Bryan>();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameispaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }
    public void Paused()
    {
        Cursor.lockState = CursorLockMode.None;
        control.stop_moving = true;
        pausemenuUI.SetActive(true);
        gameispaused = true;
    }

    public void MainMenu()
    {
        //NetworkManager.singleton.StopClient();
        SceneManager.LoadScene("Menu");
    }

    public void Settings(){
        pausemenuUI.SetActive(false);
        settings.SetActive(true);
    }

    public void Controle()
    {
        pausemenuUI.SetActive(false);
        Controles.SetActive(true);
    }

    public void Back()
    {
        settings.SetActive(false);
        pausemenuUI.SetActive(true);
    }

    public void backcontrole()
    {
        Controles.SetActive(false);
        settings.SetActive(true);
    }

    public void Resume()
    {
        pausemenuUI.SetActive(false);
        gameispaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        control.stop_moving = false;
    }
}
