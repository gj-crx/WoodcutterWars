using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;
using Unity.Netcode;
using ClientSideLogic;

public class State
{
    public Game game;
    public sbyte RaceID = 0;
    public string StateName = "Kazakhstan";

    public short StateID = 0;
    public int ControllingPlayerID = 0;
    public Vector3 position;

    public float[] ResourcesAmount = new float[3];
    public List<Building> Buildings = new List<Building>();
    public Building Townhall;




    public State(Game game, sbyte Race, Vector3 TownhallPosition, int ControllingPlayerID, string StateName = "Kazakhstan")
    {
        RaceID = Race;
        this.game = game;
        BuildingType TownhallType = PrefabManager.Singleton.prefabs_TownhallRaces[RaceID].GetComponent<BuildingTypePrefabVariant>().ToBasicType();
        Townhall = new Building(TownhallType, game, TownhallPosition, this, TownhallType.UnitTypeID, TownhallType.UnitClassID);
        position = TownhallPosition;
        StateID = (short)game.statesController.States.IndexOf(this);
        this.ControllingPlayerID = ControllingPlayerID;
        this.StateName = StateName;

        game.statesController.States.Add(this);
    }


    public struct StateSerializableData : INetworkSerializable
    {
        public short StateID;
        public sbyte RaceID;
        public int ControllingPlayerID;
        public string StateName;
        public Vector3 position;

        public float[] ResourcesAmount;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref StateID);
            serializer.SerializeValue(ref RaceID);
            serializer.SerializeValue(ref ControllingPlayerID);
            serializer.SerializeValue(ref ResourcesAmount);
            serializer.SerializeValue(ref StateName);
            serializer.SerializeValue(ref position);
        }
        public StateSerializableData(State state)
        {
            StateID = state.StateID;
            RaceID = state.RaceID;
            ControllingPlayerID = state.ControllingPlayerID;
            StateName = state.StateName;
            ResourcesAmount = state.ResourcesAmount;
            position = state.position;
        }
        public void Apply(StateClientSide StateToApply)
        {
            StateToApply.StateID = StateID;
            StateToApply.RaceID = RaceID;
            StateToApply.ControllingPlayerID = ControllingPlayerID;
            StateToApply.StateName = StateName;
            StateToApply.ResourcesAmount = ResourcesAmount;
            StateToApply.transform.position = position;
        }
    }
}