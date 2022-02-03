using System;
using Interactable;
using Mirror;
using UnityEngine;

public class PanelButton : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int _id;

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
        return _id;
    }
}
