using System;
using System.Collections;
using System.Collections.Generic;
using Interactable;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DoorPanel : AbstractControlPanel
{
    [SerializeField]
    private Door _door;
    
    [SerializeField]
    private TextMeshProUGUI _doorStatusText;

    private void Awake()
    {
        _panelButton.ButtonPressed += OpenPanel;
        _doorStatusText.text = "";
        InteractableNetworkManager.Instance.RegisterInteractable(this);
    }

    private void Update()
    {
        _doorStatusText.text = _door.DoorState.ToString();
    }

    [ClientRpc]
    private void OpenPanel(object source, EventArgs args)
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
}
