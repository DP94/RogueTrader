using System;
using Interactable;
using Mirror;
using UnityEngine;

public class PanelButton : MonoBehaviour, IInteractable
{
    private AudioSource _pressSound;

    public event EventHandler ButtonPressed;

    private void Awake()
    {
        _pressSound = GetComponent<AudioSource>();
    }

    public void Press()
    {
        ButtonPressed?.Invoke(this, EventArgs.Empty);
        _pressSound.Play();
    }
    
    public void Interact()
    {
        Press();
    }

    public int GetId()
    {
        return (int) GetComponentInParent<NetworkBehaviour>().netId;
    }
}
