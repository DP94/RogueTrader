using System;
using Interactable;
using Mirror;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private StarterAssetsInputs _input;
    
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

            _input = GetComponent<StarterAssetsInputs>();
            _playerCanvas.gameObject.SetActive(true);
        }
        else
        {
            _playerNamePlate.gameObject.SetActive(true);
            _playerNamePlate.text = PlayerName;
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        
        if (_input.activate)
        {
            Vector2 screenCentre = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCentre);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactable.Interact();
                    //CmdButtonPress(interactable.GetId());
                }
            }

            _input.activate = false;
        }
    }

    [Command]
    private void CmdButtonPress(int buttonId)
    {
        InteractableNetworkManager.Instance.ProcessDoorForAllClients(buttonId);
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
