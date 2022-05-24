using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Matchmaking;
using ClientSideLogic;

public class Player : NetworkBehaviour
{
    public int PlayerID = 0;

    public sbyte ConnectedGameID = 0;

    public StateClientSide ControlledState;

    //SERVER SIDE ONLY
    public PlayerAccount account;
    //


    //public State ControlledState;

    private void Awake()
    {
        
    }
    private void Start()
    {
        if (IsLocalPlayer)
        {
            gameObject.name = "LocalPlayer";
            UIPlayerActions.LocalPlayer = this;
        }
        
    }

    [ServerRpc]
    public void TrainUnitOrderServerRpc(int BuildingToTrainReferenceID, sbyte QueuedUnitTypeID)
    {
        GameNetCoordinator.Singleton.GetGameByID(ConnectedGameID).unitsController.Buildings[BuildingToTrainReferenceID].UnitTrainingQueue.Enqueue(QueuedUnitTypeID);
    }
    [ServerRpc]
    public void OrderUnitToMoveServerRpc(int UnitGlobalID, Vector3 Target, sbyte MovementOrderType = 0)
    {
        GameNetCoordinator.Singleton.RunnedGames[ConnectedGameID].unitsController.AllUnits[UnitGlobalID].GetWayTarget(Target);
    }
    [ServerRpc]
    public void ConnectToServerServerRpc()
    {
        account = LobbyManager.ConnectToServer(this);
    }
    [ClientRpc]
    public void GetClientSidedStateClientRpc(short StateID)
    {
        ControlledState = ClientGameController.Singleton.States[StateID];
    }
    /// <summary>
    /// Requires to be connected to the server first
    /// </summary>
    /// <param name="LobbyID"></param>
    [ServerRpc]
    public void TryToJoinLobbyServerRpc(short LobbyID = 0)
    {
        if (LobbyManager.Lobbies[LobbyID].JoinLobby(account))
        {
            ConnectedGameID = (sbyte)LobbyID;
        }
    }
    private void OnConnectedToServer()
    {
        ConnectToServerServerRpc();
        TryToJoinLobbyServerRpc(0);
        Debug.Log("Logged as " + account.Nickname);
    }

}
