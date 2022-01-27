using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LobbyPlayerUI : NetworkBehaviour
{
    [SerializeField]
    private GameObject _lobbyCanvas;
    
    [SerializeField] private List<TextMeshProUGUI> _playerNames;
    [SerializeField] private List<Button> _readyButtons;
    [SerializeField] private Button _startGame;

    private UnityAction _readyButtonListener;
    private UnityAction _startGameListener;

    private LobbyPlayer _player;


    private void Awake()
    {
        _player = GetComponent<LobbyPlayer>();
    }

    public void RefreshLobbyUIForAllPlayers(List<LobbyPlayer> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            TextMeshProUGUI playerName = _playerNames[i];
            LobbyPlayer lobbyPlayer = players[i];
            playerName.gameObject.SetActive(true);
            playerName.transform.GetChild(0).gameObject.SetActive(true);
            Button readyButton = _readyButtons[i];
            if (lobbyPlayer.IsReady)
            {
                readyButton.image.color = Color.green;
            }
            else
            {
                readyButton.image.color = Color.red;
            }
            playerName.text = lobbyPlayer.PlayerName;
        }
        _startGame.gameObject.SetActive(players.All(s => s.IsReady) && _player.IsHost);
    }
    
    public void ActivateLobbyUI()
    {
        if (!_lobbyCanvas.activeInHierarchy)
        {
            int index = _player.NetworkManager.GetLobbyPlayersCount() - 1;
            _lobbyCanvas.SetActive(true);
            TextMeshProUGUI playerName = _playerNames[index];
            playerName.gameObject.SetActive(true);
            Button readyButton = _readyButtons[index];
            readyButton.onClick.AddListener(() =>
            {
                CmdSetReady(!_player.IsReady);
            });
            readyButton.gameObject.SetActive(true);
            playerName.text = _player.PlayerName;
        }
    }

    [Command]
    private void CmdStartGame()
    {
        _player.NetworkManager.StartGame();
    }
    
    [Command]
    private void CmdSetReady(bool ready)
    {
        _player.IsReady = ready;
    }

    public LobbyPlayer Player
    {
        get => _player;
        set => _player = value;
    }

    public void AddListenersToButtons()
    {
        _readyButtonListener = () => { CmdSetReady(!_player.IsReady); };
        _startGameListener = CmdStartGame;
        if (_player.IsHost)
        {
            _startGame.onClick.AddListener(_startGameListener);
        }
    }

    public void RemoveListenersFromButtons()
    {
        //Button readyButton = _readyButtons[_player.NetworkManager.GetLobbyPlayersCount() - 1];
        //readyButton.onClick.RemoveListener(_readyButtonListener);
    }
}
