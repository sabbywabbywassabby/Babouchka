using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Respawn : NetworkBehaviour
{
    // Call this method to respawn the player
    [Server]
    public void Respaw(Transform t)
    {
        RpcRespawn(t);
    }

    [ClientRpc]
    private void RpcRespawn(Transform t)
    {
        if (isLocalPlayer)
        {
            transform.position = t.position;
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
    }
}
