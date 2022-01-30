using System;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] private ButtonType _type;

    [SerializeField]
    private bool _pressed;
    
    public event EventHandler ButtonPressed;

    public void Press()
    {
        this.ButtonPressed?.Invoke(this, EventArgs.Empty);
        _pressed = true;
    }

    private void Update()
    {
        if (_pressed)
        {
            
        }
    }
}
