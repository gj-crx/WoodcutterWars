using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Unit
{
    /// <summary>
    /// refers to the game this unit belongs to
    /// </summary>
    public Game game;
    /// <summary>
    /// main key for syncronization, refers to AllUnits in UnitsController
    /// </summary>
    public int ID = 0;
    /// <summary>
    /// refers to unit type in TypesData
    /// </summary>
    public sbyte UnitTypeID = 0;
    /// <summary>
    /// 0 - regular unit, 1 - building, 2 - tree
    /// </summary>
    public readonly sbyte UnitClass = 0;
    public string UnitTypeName = "Unknown unit";
    public State state;

    public Vector3 position;
    public List<Vector2Int> Way = new List<Vector2Int>();
    private int CurrentDistance = 1;

    public float TrainingTimeNeeded = 10;
    public float[] ResourcesCostToTrain = new float[4];

    public float MaxHP = 100;
    public float CurrentHP = 100; //high synced value
    public float Damage = 10;
    public float Regeneration = 1f;
    public float MoveSpeed = 3;
    public float AttackDelay = 0.5f;
    public float AttackRange = 2.5f;

    public float ResourcesCarriedMaximum = 20;
    public float[] ResourcesCarried = new float[4];
    public float[] ResourcesGivenOnKilled = new float[4];

    public sbyte CollisionRadius = 1;

    private bool Simulated = true;

    public delegate void OnKill(Unit killed);
    public delegate void UnitControlling();

    public OnKill onkill;
    public UnitControlling unitcontrolling;



    /// <summary>
    /// 0 - regular unit class, 1 - building class, 2 - tree class
    /// </summary>
    public Unit(Game GameToCreateUnit, Vector3 Position, State state, sbyte TypeID, sbyte UnitClassID = 0)
    {
        game = GameToCreateUnit;
        UnitTypeID = TypeID;
        UnitClass = UnitClassID;
        game.unitsController.AddNewUnit(this, UnitClass);
        this.position = Position;
        this.state = state;
        onkill = onkillMethod;
        unitcontrolling = UnitControllingMethod;
        TypesData.UnitTypes[UnitTypeID].ApplyType(this);
    }
    
    
    private void onkillMethod(Unit KilledUnit)
    {

    }
    /// <summary>
    /// unit actions can only be controlled through this method
    /// </summary>
    private void UnitControllingMethod()
    {
        if (Way.Count > 0)
        {
            WayMoving();
        }
    }
    public void Death(Unit KillerUnit)
    { //sync death with clients
        game.unitsController.RegularUnits[game.unitsController.RegularUnits.IndexOf(this)] = null;
    }
    private void MoveUnit(Vector3 To)
    {
        Vector3 MovementVector = UnitLogic.VectorToDirection(To - position);
        position += MovementVector * Time.deltaTime;
    }
    private void WayMoving()
    {
        //Debug.Log(position);
        if (Vector3.Distance(position, game.pf.Vector2IntToVector3(Way[CurrentDistance], position.y)) < MoveSpeed * 0.1f)
        {
            // Debug.Log(Way[CurrentDistance] + " next way point ");
            position = game.pf.Vector2IntToVector3(Way[CurrentDistance], position.y);
            CurrentDistance++;
        }
        if (CurrentDistance >= Way.Count)
        {
            Way.Clear();
        }
        else
        {
            MoveUnit(game.pf.Vector2IntToVector3(Way[CurrentDistance], position.y));
        }
    }

    public bool GetWayTarget(Vector3 target)
    {
        return game.pf.GetWayPath(this, target);
    }
    
    /// <summary>
    /// Contains all data (related to this unit) that should be transfered to clients from server
    /// </summary>
    public struct UnitSerializableData : INetworkSerializable
    {
        /// <summary>
        /// ID in units pool
        /// </summary>
        public int UnitObjectID;
        /// <summary>
        /// refers to GameData.UnitTypes
        /// </summary>
        public sbyte UnitTypeID;
        /// <summary>
        /// 0 - regular unit, 1 - building, 2 - tree
        /// </summary>
        public sbyte UnitClassID;
        public float CurrentHP;
        public Vector3 position;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref UnitObjectID);
            serializer.SerializeValue(ref UnitTypeID);
            serializer.SerializeValue(ref UnitClassID);
            serializer.SerializeValue(ref CurrentHP);
            serializer.SerializeValue(ref position);
        }
        public UnitSerializableData(Unit unit)
        {
            UnitObjectID = unit.ID;
            UnitTypeID = unit.UnitTypeID;
            UnitClassID = unit.UnitClass;
            CurrentHP = unit.CurrentHP;
            position = unit.position;
        }
    }
}
