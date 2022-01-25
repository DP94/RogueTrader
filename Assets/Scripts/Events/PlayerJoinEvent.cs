using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoinEvent
{
    private LobbyPlayer _newPlayer;
    private int _lobbyIndex;

    public LobbyPlayer Player
    {
        get => _newPlayer;
        set => _newPlayer = value;
    }

    public int LobbyIndex
    {
        get => _lobbyIndex;
        set => _lobbyIndex = value;
    }
}
