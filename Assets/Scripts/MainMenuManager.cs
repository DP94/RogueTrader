using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private NetworkManager _networkManager;

    [SerializeField] private GameObject _mainMenu;

    public void OnHostGameClicked()
    {
        _mainMenu.SetActive(false);
        PlayerPrefs.SetString("PlayerName", $"Danguinius{Random.Range(0, 10)}");
        _networkManager.StartHost();
    }
    
    public void OnExitClick()
    {
        Application.Quit();
    }
}
