using System.Collections;
using System.Collections.Generic;
using Mirror;
using StarterAssets;
using TMPro;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private NetworkManagerExtension _networkManager;
    
    [SyncVar]
    [SerializeField]
    private string _playerName;
    
    [SerializeField]
    private Canvas _playerCanvas;
    
    [SerializeField]
    private TextMeshProUGUI _playerNamePlate;
    
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
            _playerCanvas.gameObject.SetActive(true);
        }
        else
        {
            _playerNamePlate.gameObject.SetActive(true);
            _playerNamePlate.text = PlayerName;
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

    public string PlayerName
    {
        get => _playerName;
        set => _playerName = value;
    }
}
