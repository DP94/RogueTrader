using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LobbyPlayer : NetworkBehaviour
{
    [SyncVar] [SerializeField] private string _playerName;

    [SyncVar] [SerializeField] private int _connectionId;

    [SyncVar(hook = nameof(OnPlayerReadyUp))] [SerializeField]
    private bool _isReady;

    private bool _isHost;
    
    private const string PlayerPrefsNameKey = "PlayerName";

    private NetworkManagerExtension _networkManager;
    
    private LobbyPlayerUI _lobbyPlayerUI;


    private void Awake()
    {
        _lobbyPlayerUI = GetComponent<LobbyPlayerUI>();
    }

    public override void OnStartAuthority()
    {
        gameObject.name = "LocalLobbyPlayer";
        CmdSetPlayerName(PlayerPrefs.GetString(PlayerPrefsNameKey));
    }

    [Command]
    private void CmdSetPlayerName(string name)
    {
        PlayerName = name;
    }

    
    public override void OnStartClient()
    {
        NetworkManager.AddPlayerToLobbyList(this);
        NetworkManager.PlayerJoinHandler += OnPlayerJoinLobbyEvent;
        _lobbyPlayerUI.AddListenersToButtons();
        //Show everyone's status in lobby first
        _lobbyPlayerUI.RefreshLobbyUIForAllPlayers(NetworkManager.LobbyPlayers);
        //Then update your own
        UpdateLobbyUI();
    }

    public override void OnStopClient()
    {
        NetworkManager.RemovePlayerToLobbyList(this);
        _lobbyPlayerUI.RemoveListenersFromButtons();
        UpdateLobbyUI();
    }

    
    public void UpdateLobbyUI()
    {
        GameObject localPlayer = GameObject.Find("LocalLobbyPlayer");
        if (localPlayer != null)
        {
            localPlayer.GetComponent<LobbyPlayerUI>().ActivateLobbyUI();
        }
    }

    public string PlayerName
    {
        get => _playerName;
        set => _playerName = value;
    }

    public int ConnectionId
    {
        get => _connectionId;
        set => _connectionId = value;
    }

    public bool IsReady
    {
        get => _isReady;
        set => _isReady = value;
    }

    public bool IsHost
    {
        get => _isHost;
        set => _isHost = value;
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

    public void OnPlayerJoinLobbyEvent(object sender, EventArgs args)
    {
        if (sender is PlayerJoinEvent)
        {
            _lobbyPlayerUI.RefreshLobbyUIForAllPlayers(NetworkManager.LobbyPlayers);
        }
    }

    public void OnPlayerReadyUp(bool oldValue, bool newValue)
    {
        for (int i = 0; i < NetworkManager.LobbyPlayers.Count; i++)
        {
            LobbyPlayer lobbyPlayer = NetworkManager.LobbyPlayers[i];
            lobbyPlayer.LobbyPlayerUI.RefreshLobbyUIForAllPlayers(NetworkManager.LobbyPlayers);
        }
    }


    public LobbyPlayerUI LobbyPlayerUI
    {
        get => _lobbyPlayerUI;
        set => _lobbyPlayerUI = value;
    }
}