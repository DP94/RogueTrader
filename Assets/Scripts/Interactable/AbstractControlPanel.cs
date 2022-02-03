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