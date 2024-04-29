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
    public void Paused()
    {   
        pausemenuUI.SetActive(true);
        gameispaused = true;
    }

    public void Settings(){
        pausemenuUI.SetActive(false);
        settings.SetActive(true);
    }

    public void Resume()
    {
        Debug.Log("caca");
        
        pausemenuUI.SetActive(false);
        gameispaused = false;
    }
}
