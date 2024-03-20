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
    public Text ipText;
    public void GameButton()
    {
        SceneManager.UnloadSceneAsync("Menu");
        SceneManager.LoadScene(Game_scene);
        Camera.main.gameObject.SetActive(true);
    }
    public void SoloButton()
    {
        SceneManager.LoadScene("SoloScene");
    }

    public void Ip()
    {
        try
        {
            // Obtient toutes les adresses IP de l'hôte local
            IPAddress[] localIPs = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            // Sélectionne la première adresse IPv4
            IPAddress localIPv4 = localIPs.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            if (localIPv4 != null)
            {
                ipText.text="L'adresse IP locale IPv4 de votre ordinateur est : " + localIPv4.ToString();
            }
            else
            {
                Console.WriteLine("Aucune adresse IPv4 locale trouvée.");
            }
        }
        catch (Exception)
        {
            // En cas d'erreur, affiche un message d'erreur dans l'objet Text de l'interface utilisateur
            ipText.text = "Erreur : Impossible de récupérer l'adresse IP locale.";
            
        }
        
    }
}
