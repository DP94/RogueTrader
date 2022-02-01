using System.Collections.Generic;
using Mirror;

public class DoorNetworkManager
{
    private static DoorNetworkManager _instance = new();
    
    public static DoorNetworkManager Instance => _instance;

    public List<DoorPanel> DoorPanels { get; private set; } = new();

    [Server]
    public void RegisterDoorPanel(DoorPanel doorPanel)
    {
        DoorPanels.Add(doorPanel);
    }

    public void ProcessDoorForAllClients(int doorId)
    {
        var doorPanel = DoorPanels.Find(panel => panel.DoorButton.ID == doorId);
        if (doorPanel != null)
        {
            RpcUpdateDoor(doorPanel);
        }
    }

    [ClientRpc]
    private void RpcUpdateDoor(DoorPanel door)
    {
        door.DoorButton.Press();
    }
}