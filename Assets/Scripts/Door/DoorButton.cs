using System;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] private ButtonType _type;

    [SerializeField]
    private bool _pressed;

    private AudioSource _pressSound;
    
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

    private void Update()
    {
        if (_pressed)
        {
            
        }
    }
}
