using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pause : MonoBehaviour
{
    public static bool gameispaused = false;
    public GameObject pausemenuUI;
    public GameObject settings;
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
    void Paused()
    {   
        pausemenuUI.SetActive(true);
        gameispaused = true;
        // Time.timeScale=0; pour en solo
    }

    void Settings(){
        pausemenuUI.SetActive(false);
        settings.SetActive(true);
    }

    void Resume()
    {
        Debug.Log("caca");
        // Time.timeScale=1; pour en solo
        pausemenuUI.SetActive(false);
        gameispaused = false;
    }
}
