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
