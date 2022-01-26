using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private NetworkManager _networkManager;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _joinGameMenu;

    private string _ipAddress;

    public void OnHostGameClicked()
    {
        _mainMenu.SetActive(false);
        _joinGameMenu.SetActive(false);
        PlayerPrefs.SetString("PlayerName", $"Danguinius{Random.Range(0, 10)}");
        _networkManager.StartHost();
    }

    public void OnJoinGameClicked()
    {
        _mainMenu.SetActive(false);
        _joinGameMenu.SetActive(true);
    }

    public void OnLobbyJoinIPAddressChange(string value)
    {
        this._ipAddress = value;
    }
    
    public void OnLobbyJoinGameClicked()
    {
        _mainMenu.SetActive(false);
        _joinGameMenu.SetActive(false);
        PlayerPrefs.SetString("PlayerName",  $"Danguinius{Random.Range(0, 10)}");
        _networkManager.networkAddress = this._ipAddress;
        _networkManager.StartClient();
    }

    public void OnLobbyBackClicked()
    {
        _mainMenu.SetActive(true);
        _joinGameMenu.SetActive(false);
    }
    
    public void OnExitClick()
    {
        Application.Quit();
    }
}
