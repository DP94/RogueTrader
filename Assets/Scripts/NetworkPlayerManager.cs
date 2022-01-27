using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkPlayerManager
{
    private static NetworkPlayerManager _instance = new();

    private Dictionary<int, Player> _players;

    public NetworkPlayerManager()
    {
        _players = new Dictionary<int, Player>();
    }
    
    public void AddPlayer(int connectionId, Player player)
    {
        _players.Add(connectionId, player);
    }

    public void RemovePlayer(int connectionId)
    {
        _players.Remove(connectionId);
    }

    public Player GetRandomPlayer()
    {
        int index = Random.Range(0, _players.Count);
        Player player;
        if (_players.TryGetValue(index, out player))
        {
            return player;
        }

        return null;
    }

    public static NetworkPlayerManager Instance => _instance;
}
