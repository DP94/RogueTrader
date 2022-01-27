using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class DoorPanel : NetworkBehaviour
{
    [SerializeField]
    private Door _door;

    private void OnTriggerEnter(Collider other)
    {
        _door.DoorState = DoorState.Opening;
    }

    private void OnTriggerExit(Collider other)
    {
        _door.DoorState = DoorState.Closing;
    }
}
