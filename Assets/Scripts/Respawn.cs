using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Respawn : NetworkBehaviour
{
    public int lives;
    public GameObject game_over;
    public GameObject screamer;
    public GameObject un;
    public GameObject deux;
    public GameObject trois;
    public bool is_dead;

    private void Start()
    {
        lives = 4;
        is_dead = false;
    }

    // Call this method to respawn the player
    [Server]
    public void Respaw(Transform t)
    {
        RpcRespawn(t.position, is_dead);
    }

    [ClientRpc]
    public void RpcRespawn(Vector3 position, bool dead)
    {
        if (isLocalPlayer)
        {
            lives--;

            if (lives == 0 || dead)
            {
                game_over.SetActive(true);
            }
            else
            {
                StartCoroutine(HandleRespawn(position));
            }
        }
    }

    private IEnumerator HandleRespawn(Vector3 position)
    {
        print("enter");
        print(lives);
        transform.position = position;
        screamer.SetActive(true);
        yield return new WaitForSeconds(2f);
        screamer.SetActive(false);

        un.SetActive(lives == 1);
        deux.SetActive(lives == 2);
        trois.SetActive(lives == 3);

        yield return new WaitForSeconds(2f);

        un.SetActive(false);
        deux.SetActive(false);
        trois.SetActive(false);
    }
}
