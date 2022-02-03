using System.Collections.Generic;
using Interactable;
using Mirror;

public class InteractableNetworkManager
{
    public static InteractableNetworkManager Instance { get; } = new();

    private List<IInteractable> Interactables { get; } = new();

    [Server]
    public void RegisterInteractable(IInteractable interactable)
    {
        Interactables.Add(interactable);
    }

    public void ProcessInteractableForAllClients(int id)
    {
        var doorPanel = Interactables.Find(i => i.GetId() == id);
        if (doorPanel != null)
        {
            RpcInteract(doorPanel);
        }
    }

    [ClientRpc]
    private void RpcInteract(IInteractable interactable)
    {
        interactable.Interact();
    }
}