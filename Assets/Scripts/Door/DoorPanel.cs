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
    private DoorButton _openButton;

    [SerializeField]
    private DoorButton _closeButton;

    [SerializeField]
    private GameObject _panelCanvas;

    [SerializeField]
    private TextMeshProUGUI _doorStatusText;

    private void Awake()
    {
        _openButton.ButtonPressed += OpenDoor;
        _closeButton.ButtonPressed += CloseDoor;
        _doorStatusText.text = "";
    }

    private void OpenDoor(object source, EventArgs args)
    {
        _door.DoorState = DoorState.Opening;
        _doorStatusText.text = _door.DoorState.ToString();
    }
    
    private void CloseDoor(object source, EventArgs args)
    {
        _door.DoorState = DoorState.Closing;
        _doorStatusText.text = _door.DoorState.ToString();
    }
}
