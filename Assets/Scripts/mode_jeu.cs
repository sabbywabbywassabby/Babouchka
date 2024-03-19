using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mode_jeu : MonoBehaviour
{
    public string Game_scene;
    public void GameButton()
    {
        SceneManager.LoadScene(Game_scene);
    }
}
