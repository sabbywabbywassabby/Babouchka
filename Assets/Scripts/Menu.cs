using UnityEngine;

public class Menu : MonoBehaviour
{
   public string load;
   public GameObject settingsWindow;
    public GameObject startingWindow;


    public void StartButton()
    {
         startingWindow.SetActive(true);
    }

    public void CloseStartWindow()
    {
        startingWindow.SetActive(false);
    }


   public void StartGame()
   {
   // faut faire le lancement pour en solo et en multijoueur
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
