using System;
using Mirror;
using UnityEngine;

namespace Interactable
{
    public abstract class AbstractControlPanel: NetworkBehaviour, IInteractable
    {
        [SerializeField]
        protected PanelButton _panelButton;
        
        [SerializeField]
        protected GameObject _panelCanvas;

        [SerializeField] protected int _id;

        protected void Awake()
        {
            InteractableNetworkManager.Instance.RegisterInteractable(_panelButton);
            InteractableNetworkManager.Instance.RegisterInteractable(this);
        }

        public void Interact()
        {
            _panelButton.Press();
        }

        public int GetId()
        {
            return _id;
        }
    }
}