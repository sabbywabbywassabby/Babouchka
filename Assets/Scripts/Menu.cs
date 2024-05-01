using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{
    public GameObject settingsWindow;
    public GameObject startingWindow;
    public GameObject background;
    public GameObject controles;

    public void ControleButton()
    {
        controles.SetActive(true);
    }

    public void CloseControles()
    {
        controles.SetActive(false);
    }


    public void StartButton()
    {
         startingWindow.SetActive(true);
    }

    public void CloseStartWindow()
    {
        startingWindow.SetActive(false);
    }



   public void SettingsButton()
   {
        settingsWindow.SetActive(true);
   }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}

public class DelayedCanvasActivation : MonoBehaviour
{
    public Canvas Menu;
    public float delayInSeconds = 2f;

    void Start()
    {
        Invoke("ActivateCanvas", delayInSeconds);
    }

    void ActivateCanvas()
    {
        if (Menu != null)
        {
            Menu.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Le Canvas n'est pas référencé dans le script.");
        }
    }
}
