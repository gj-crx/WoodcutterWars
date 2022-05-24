using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using ClientSideLogic;

public class GameNetCoordinator : NetworkBehaviour
{
    /// <summary>
    /// only coordinator for all lobbies at this server
    /// </summary>
    public static GameNetCoordinator Singleton;
    public List<Game> RunnedGames;

    public float UnitSyncInterval = 0.3f;

    public GameObject prefab_GameLobby;

    private float timer_UnitSync = 0;
    private bool IsInitialized = false;


    private void Awake()
    {
        TypesData.LoadAllTypes(PrefabManager.Singleton.prefabs_Units, PrefabManager.Singleton.prefabs_Buildings, PrefabManager.Singleton.prefabs_Trees);
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (IsServer)
        {
            UnitSynchronization();
        }
    }

    public void Init()
    {
        if (IsInitialized == false)
        {
            IsInitialized = true;
        }
        else
        {
            return;
        }
        Singleton = this;
        LobbyManager.LobbyManagerInit();
        LobbyManager.CreateMainLobby();
        ScenariosManager.TestingScenarioStart();

    }

    private void UnitSynchronization()
    {
        if (timer_UnitSync > UnitSyncInterval)
        {
            timer_UnitSync = 0;
            foreach (var Lobby in LobbyManager.Lobbies)
            {
                if (Lobby.IsStarted)
                {
                    SyncUnitList(Lobby.GameRunningInLobby.unitsController.AllUnits);
                }
            }
        }
        else
        {
            timer_UnitSync += Time.deltaTime;
        }
    }
    public Game CreateNewGame(short GameLobbyID)
    {
        Game NewGame = Instantiate(prefab_GameLobby).GetComponent<Game>();
        NewGame.gameObject.name = "Game " + GameLobbyID;
        RunnedGames.Add(NewGame);
        NewGame.InitializeGame();
        return NewGame;
    }
    public Game GetGameByID(sbyte IDOfGame)
    {
        return RunnedGames[IDOfGame];
    }

    private void SyncUnitList(List<Unit> UnitsToSync)
    {
        foreach (var Unit in UnitsToSync)
        {
            if (Unit != null)
            {
                SyncUnitClientRpc(new Unit.UnitSerializableData(Unit));
            }
        }
    }
    [ClientRpc]
    public void SyncUnitClientRpc(Unit.UnitSerializableData data)
    {
        if (ClientUnitController.UnitExistInClient(data.UnitObjectID))
        {
            UnitClientSide ClientSideUnit = ClientUnitController.FindUnitByID(data.UnitObjectID);
            ClientSideUnit.ApplyRecievedData(data);
        }
        else
        { //create client side unit representation
            ClientUnitController.CreateUnitRepresentation(data);
        }

    }
    [ClientRpc]
    public void SyncStateClientRpc(State.StateSerializableData data)
    {
        if (ClientStateController.StateExistInClient(data.StateID))
        {
            StateClientSide ClientSideState = ClientStateController.States[data.StateID];
            data.Apply(ClientSideState);
        }
        else
        { //create client side unit representation
            ClientStateController.CreateClientStateRepresentation(data);
        }

    }

}
