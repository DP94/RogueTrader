using System;
using Mirror;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField]
    private bool _pressed;

    [SyncVar]
    [SerializeField]
    private int _id;

    private AudioSource _pressSound;

    [SerializeField]
    private GameObject _openButton;

    [SerializeField]
    private GameObject _closeButton;
    
    public event EventHandler ButtonPressed;

    private void Awake()
    {
        _pressSound = GetComponent<AudioSource>();
    }

    public void Press()
    {
        this.ButtonPressed?.Invoke(this, EventArgs.Empty);
        _pressed = true;
        _pressSound.Play();
    }

    public int ID
    {
        get => _id;
        set => _id = value;
    }
}
