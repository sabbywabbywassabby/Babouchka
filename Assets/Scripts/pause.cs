using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pause : MonoBehaviour
{
    public static bool gameispaused = false;
    public GameObject pausemenuUI;
    private Controleur_Bryan control;


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
        control.enabled = false;
        pausemenuUI.SetActive(true);
        gameispaused = true;
    }

    void Resume()
    {
        control.enabled = true;
        pausemenuUI.SetActive(false);
        gameispaused = false;
    }
}
