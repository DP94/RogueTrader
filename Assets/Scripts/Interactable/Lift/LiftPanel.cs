using System;
using System.Collections;
using System.Collections.Generic;
using Interactable;
using Mirror;
using UnityEngine;

public class LiftPanel: AbstractControlPanel
{
    [SerializeField]
    private Lift _lift;

    private void Awake()
    {
        _panelButton.ButtonPressed += MoveLift;
        InteractableNetworkManager.Instance.RegisterInteractable(this);
    }
    
    [ClientRpc]
    private void MoveLift(object source, EventArgs args)
    {
        _lift.MoveToNextTarget();
    }
}
