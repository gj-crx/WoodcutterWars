using System.Collections;
using System.Collections.Generic;
using Types;
using UnityEngine;

/// <summary>
/// part of an Unit class
/// </summary>
public class Building : Unit
{
    /// <summary>
    /// refers to Buildings in UnitsController
    /// </summary>
    public int IDInBuildingsPool = 0;
    private int TickTimer_ResourceProduction;
    private int TickTimer_UnitTraining;

    public BuildingType type;
    /// <summary>
    /// contains UnitTypeIDs
    /// </summary>
    public Queue<sbyte> UnitTrainingQueue = new Queue<sbyte>();

    private Vector3 UnitSpawningOffset = new Vector3(3.4f, 0, 2.5f);

    public Building(BuildingType type, Game GameToBe,  Vector3 position, State state, sbyte UnitTypeID, sbyte UnitClassID = 1) : base (GameToBe, position, state, UnitTypeID, UnitClassID)
    {
        this.type = type;
    }

    public void LocalTickTimerExecution(int tick = 0)
    {
        if (type.ProducedResource != null)
        {
            TickTimer_ResourceProduction += tick;
            if (TickTimer_ResourceProduction > type.ProducedResource.ProductionTimeNeeded)
            {
                TickTimer_ResourceProduction = 0;
                ProduceResources();
            }
        }
        if (UnitTrainingQueue.Count > 0)
        {
            Debug.Log("unit training");
            TickTimer_UnitTraining += tick;
            if (TickTimer_UnitTraining > TypesData.UnitTypes[UnitTrainingQueue.Peek()].BuildTimeNeeded * type.UnitTrainingSpeedModifier)
            {
                TickTimer_ResourceProduction = 0;
                TrainUnit();
            }
        }
    }

    private void ProduceResources()
    {
        if (TickTimer_ResourceProduction >= type.ProducedResource.ProductionTimeNeeded)
        {
            bool EnoughResources = true;
            for (int i = 0; i < type.ProducedResource.ResourcesCostPer1.Length; i++)
            {
                if (state.ResourcesAmount[i] < TypesData.UnitTypes[UnitTypeID].ResourcesCostToBuild[i])
                {
                    EnoughResources = false;
                    break;
                }
            }
            if (EnoughResources)
            {
                for (int i = 0; i < type.ProducedResource.ResourcesCostPer1.Length; i++)
                {
                    state.ResourcesAmount[i] -= type.ProducedResource.ResourcesCostPer1[i];
                }
                state.ResourcesAmount[type.ProducedResource.ID] += type.ProducedResourcesAmount;
            }
        }
        else
        {
            TickTimer_ResourceProduction = 0;
        }
    }
    private void TrainUnit()
    {
        Unit NewUnit = new Unit(game, position + UnitSpawningOffset, state, UnitTrainingQueue.Dequeue());
    }
}
