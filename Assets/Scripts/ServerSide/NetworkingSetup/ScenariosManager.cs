using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Matchmaking;

public static class ScenariosManager
{
    public static List<Vector3> PlayersStartingPositions = new List<Vector3>();
    

    private static Vector3[] GetPlayersStartingPositions(int PlayersCount)
    {
        Vector3[] positions = new Vector3[PlayersCount];


        return positions;
    }
    private static void CreatePlayerBase(Game GameToCreate, Player player, Vector3 PositionToSpawn, sbyte RaceID)
    {
        State PlayerState = new State(GameToCreate, RaceID, PositionToSpawn, player.PlayerID);
        GameNetCoordinator.Singleton.SyncStateClientRpc(new State.StateSerializableData(PlayerState));
    }
    private static void SpawnPlayerStartingBases()
    {

    }
    public static void BasicLobbyStart(GameLobby LobbyToStart)
    {
        //Generate map
        MapGenerator.GenerateTrees(LobbyToStart.GameRunningInLobby, LobbyToStart.type.StartingTreesAmount);

        //Create players starting bases
        int Counter = 0;
        foreach (var Player in LobbyToStart.Players)
        {
            Vector3[] PlayersPositions = GetPlayersStartingPositions(LobbyToStart.Players.Count);
            CreatePlayerBase(LobbyToStart.GameRunningInLobby, Player.PlayerIntentsObject, PlayersStartingPositions[Counter], Player.SelectedRace);
            Counter++;
        }
    }
    public static void TestingScenarioStart()
    {
        //create some units
        Game NewGame = GameNetCoordinator.Singleton.GetGameByID(0);
        MapGenerator.GenerateTrees(NewGame, 200);
        new Unit(NewGame, new Vector3(85, 1, 135), null, 0);
        new Unit(NewGame, new Vector3(82, 1, 140), null, 0);
    }
}
