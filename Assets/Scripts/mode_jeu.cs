using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Net;
using System.Net.Sockets;
using System.Linq;

public class mode_jeu : MonoBehaviour
{
    public string Game_scene;
    public string Solo_Scene;
    public Text ipText;
    public void GameButton()
    {
        SceneManager.LoadScene(Game_scene);
        Camera.main.gameObject.SetActive(true);
    }
    public void SoloButton()
    {
        SceneManager.LoadScene(Solo_Scene);
    }

    public void Quit()
    {
        gameObject.SetActive(false);
    }

    public void Ip()
    {
        try
        {
            // Obtient toutes les adresses IP de l'h�te local
            IPAddress[] localIPs = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            // S�lectionne la premi�re adresse IPv4
            IPAddress localIPv4 = localIPs.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            if (localIPv4 != null)
            {
                ipText.text="L'adresse IP locale IPv4 de votre ordinateur est : " + localIPv4.ToString();
            }
            else
            {
                Console.WriteLine("Aucune adresse IPv4 locale trouv�e.");
            }
        }
        catch (Exception)
        {
            // En cas d'erreur, affiche un message d'erreur dans l'objet Text de l'interface utilisateur
            ipText.text = "Erreur : Impossible de r�cup�rer l'adresse IP locale.";
            
        }
        
    }
}
