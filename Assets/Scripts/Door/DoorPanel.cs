using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class DoorPanel : NetworkBehaviour
{
    [SerializeField]
    private Door _door;

    [SerializeField]
    private DoorButton _doorButton;

    [SerializeField]
    private GameObject _panelCanvas;

    [SerializeField]
    private TextMeshProUGUI _doorStatusText;

    private void Awake()
    {
        _doorButton.ButtonPressed += OpenDoor;
        _doorStatusText.text = "";
        DoorNetworkManager.Instance.RegisterDoorPanel(this);
    }

    private void Update()
    {
        _doorStatusText.text = _door.DoorState.ToString();
    }

    [ClientRpc]
    private void OpenDoor(object source, EventArgs args)
    {
        switch (_door.DoorState)
        {
            case DoorState.Closed:
            case DoorState.Closing:
                _door.DoorState = DoorState.Opening;
                return;
            case DoorState.Opened:
            case DoorState.Opening:
                _door.DoorState = DoorState.Closing;
                return;
            default:
                _door.DoorState = DoorState.Closing;
                return;
        }
    }

    public DoorButton DoorButton => _doorButton;
}
