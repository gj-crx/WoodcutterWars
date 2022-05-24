using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Matchmaking;

public static class LobbyManager
{
    public static GameLobby CurrentMainLobby = null;

    public static List<PlayerAccount> ConnectedToServerPlayers = new List<PlayerAccount>();

    public static List<GameLobby> Lobbies = new List<GameLobby>();
    public static List<LobbyType> LobbyTypes = new List<LobbyType>();
    public static void CreateMainLobby()
    {
        CurrentMainLobby = new GameLobby(LobbyTypes[0]);
        Lobbies[0] = CurrentMainLobby;
        CurrentMainLobby.StartLobby();
    }
    public static void LobbyManagerInit()
    {
        LobbyTypes.Add(new LobbyType(0, 12));
        LobbyTypes.Add(new LobbyType(1, 1));
        LobbyTypes.Add(new LobbyType(2, 20));
    }
    public static PlayerAccount ConnectToServer(Player playerObject, string Nickname = "Noname", string password = "123")
    {
        PlayerAccount acc = new PlayerAccount(playerObject, Nickname);
        ConnectedToServerPlayers.Add(acc);
        return acc;
    }
    public static short GetNewLobbyID()
    {
        short i = 0;
        bool NotReachedLimit = true;
        while (NotReachedLimit)
        {
            if (Lobbies[i] == null)
            {
                return i;
            }
            else
            {
                i++;
                if (i > 1000)
                {
                    NotReachedLimit = false;
                    return 998;
                }
            }
        }
        return 0;
    }
}
namespace Matchmaking
{
    public class PlayerAccount
    {
        public Player PlayerIntentsObject = null;
        public int AccountID = 0;
        public string Nickname = "Noname";
        public sbyte SelectedRace = 0;
        public PlayerAccount(Player playerObject, string nickname)
        {
            AccountID = 0;
            Nickname = nickname;
        }
    }
    public class GameLobby
    {
        public short LobbyID = 0;
        public Game GameRunningInLobby = null;
        public List<PlayerAccount> Players = new List<PlayerAccount>();
        public LobbyType type;
        public bool IsFull { get; private set; }
        public bool IsStarted { get; private set; }

        public GameLobby(LobbyType lobbyType)
        {
            type = lobbyType;
            GameRunningInLobby = GameNetCoordinator.Singleton.CreateNewGame(LobbyID);
            LobbyManager.Lobbies.Add(this);
            LobbyID = (short)LobbyManager.Lobbies.IndexOf(this);
        }

        /// <summary>
        /// return true if connection accepted
        /// </summary>
        public bool JoinLobby(PlayerAccount player)
        {
            if (Players.Count < type.MaxPossiblePlayer)
            {
                Players.Add(player);
                if (Players.Count >= type.MaxPossiblePlayer)
                {
                    IsFull = true;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void RemovePlayerFromLobby(PlayerAccount player)
        {
            Players.Remove(player);
            if (Players.Count < type.MaxPossiblePlayer)
            {
                IsFull = false;
            }
        }
        public void StartLobby()
        {
            ScenariosManager.BasicLobbyStart(this);
            IsStarted = true;
            
        }
    }
    public class LobbyType
    {
        public short ID = 0;
        public sbyte MaxPossiblePlayer = 12;
        public short StartingTreesAmount = 200;
        public LobbyType(sbyte id, sbyte MaxPlayers)
        {
            ID = id;
            MaxPossiblePlayer = MaxPlayers;
        }
    }
}
