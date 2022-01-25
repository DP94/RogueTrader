using System.Collections;
using System.Collections.Generic;
using Mirror;
using StarterAssets;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private NetworkManagerExtension _networkManager;
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        NetworkManager.GamePlayers.Add(this);
        if (isLocalPlayer)
        {
            if (GetComponents<Behaviour>() != null)
            {
                foreach (var component in GetComponents<Behaviour>())
                {
                    component.enabled = true;
                }
            }

            if (GetComponents<Collider>() != null)
            {
                foreach (var component in GetComponents<Collider>())
                {
                    component.enabled = true;
                }
            }

            GetComponent<FirstPersonController>().enabled = true;
        }
    }
    
    public NetworkManagerExtension NetworkManager
    {
        get
        {
            if (_networkManager == null)
            {
                _networkManager = NetworkManagerExtension.singleton as NetworkManagerExtension;
            }

            return _networkManager;
        }
    }
}
