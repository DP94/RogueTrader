using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class NetworkManagerExtension : NetworkManager
{
    [SerializeField] private int minimumPlayers = 2;

    [SerializeField] private LobbyPlayer _lobbyPlayerPrefab;

    [SerializeField] private Player _playerPrefab;

    [SerializeField] private List<LobbyPlayer> _lobbyPlayers = new();

    [SerializeField] private List<Player> _gamePlayers = new();

    public event EventHandler PlayerJoinHandler;

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers >= maxConnections || SceneManager.GetActiveScene().name != "Menu")
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            LobbyPlayer player = Instantiate(_lobbyPlayerPrefab);
            player.ConnectionId = conn.connectionId;
            player.PlayerName = PlayerPrefs.GetString("PlayerName");
            player.IsHost = _lobbyPlayers.Count == 0;
            NetworkServer.AddPlayerForConnection(conn, player.gameObject);
        }
        else
        {
            base.OnServerAddPlayer(conn);
        }
    }

    public void AddPlayerToLobbyList(LobbyPlayer lobbyPlayer)
    {
        _lobbyPlayers.Add(lobbyPlayer);
         var playerJoinEvent = new PlayerJoinEvent
         {
             Player = lobbyPlayer,
             LobbyIndex = _lobbyPlayers.IndexOf(lobbyPlayer)
         };
        _lobbyPlayers.ForEach(p => p.OnPlayerJoinLobbyEvent(playerJoinEvent, EventArgs.Empty));
    }

    public void RemovePlayerToLobbyList(LobbyPlayer lobbyPlayer)
    {
        _lobbyPlayers.Add(lobbyPlayer);
    }

    public int GetLobbyPlayersCount()
    {
        return _lobbyPlayers.Count;
    }

    public List<LobbyPlayer> LobbyPlayers
    {
        get => _lobbyPlayers;
    }
    
    public List<Player> GamePlayers
    {
        get => _gamePlayers;
    }

    public void StartGame()
    {
        ServerChangeScene("Game");
    }

    public override void ServerChangeScene(string newSceneName)
    {
        if (SceneManager.GetActiveScene().name == "Menu" && newSceneName == "Game")
        {
            for (int i = LobbyPlayers.Count - 1; i >= 0; i--)
            {
                LobbyPlayer player = LobbyPlayers[i];
                var conn = player.connectionToClient;
                var gamePlayerInstance = Instantiate(playerPrefab);
                NetworkServer.Destroy(conn.identity.gameObject);
                DontDestroyOnLoad(gamePlayerInstance);
                NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject, true);
            }
            _lobbyPlayers.Clear();
        }

        base.ServerChangeScene(newSceneName);
    }

    public Player GetRandomPlayer()
    {
        return _gamePlayers[Random.Range(0, _gamePlayers.Count)];
    }

    
}