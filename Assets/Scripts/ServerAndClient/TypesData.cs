using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public static class TypesData
{
    public static List<ResourceType> ResourceTypes = new List<ResourceType>();
    /// <summary>
    /// units, buildings and trees
    /// </summary>
    public static List<BasicUnitType> UnitTypes = new List<BasicUnitType>();

    
    public static void LoadAllTypes(GameObject[] UnitPrefabs, GameObject[] BuildingPrefabs, GameObject[] TreesPrefabs)
    {
        LoadResourceTypes();
        LoadUnitTypes(UnitPrefabs, BuildingPrefabs, TreesPrefabs);
    }
    private static void LoadUnitTypes(GameObject[] UnitPrefabs, GameObject[] BuildingPrefabs, GameObject[] TreesPrefabs)
    {
        foreach (var g in UnitPrefabs)
        {
            BasicUnitType unitType = g.GetComponent<UnitTypePrefabVariant>().ToBasicType();
            UnitTypes.Add(unitType);
            sbyte NewUnitTypeID = (sbyte)UnitTypes.IndexOf(unitType);
            UnitTypes[NewUnitTypeID].UnitTypeID = NewUnitTypeID;
            g.GetComponent<UnitTypePrefabVariant>().UnitTypeID = NewUnitTypeID;
            PrefabManager.Singleton.prefabs_AllUnits[NewUnitTypeID] = g;
        }
        foreach (var g in BuildingPrefabs)
        {
            BasicUnitType buildingType = g.GetComponent<BuildingTypePrefabVariant>().ToBasicType();
            UnitTypes.Add(buildingType);
            sbyte NewUnitTypeID = (sbyte)UnitTypes.IndexOf(buildingType);
            UnitTypes[NewUnitTypeID].UnitTypeID = NewUnitTypeID;
            PrefabManager.Singleton.prefabs_AllUnits[NewUnitTypeID] = g;
        }
        foreach (var g in TreesPrefabs)
        {
            BasicUnitType unitType = g.GetComponent<UnitTypePrefabVariant>().ToBasicType();
            UnitTypes.Add(unitType);
            sbyte NewUnitTypeID = (sbyte)UnitTypes.IndexOf(unitType);
            UnitTypes[NewUnitTypeID].UnitTypeID = NewUnitTypeID;
            g.GetComponent<UnitTypePrefabVariant>().UnitTypeID = NewUnitTypeID;
            PrefabManager.Singleton.prefabs_AllUnits[NewUnitTypeID] = g;
        }
    }
    private static void LoadResourceTypes()
    { //resource types are hardcoded for now

        //0 - lumber
        ResourceType type = new ResourceType();
        type.ID = 0;
        type.name = "Lumber";
        type.ProductionTimeNeeded = 8;
        type.ResourcesCostPer1 = new float[4];
        ResourceTypes.Add(type);

        //1 - materials
        type = new ResourceType();
        type.ID = 1;
        type.name = "Materials";
        type.ProductionTimeNeeded = 14;
        type.ResourcesCostPer1 = new float[4];
        type.ResourcesCostPer1[0] = 1;
        ResourceTypes.Add(type);

        //2
        type = new ResourceType();
        type.ID = 2;
        type.name = "Housing";
        type.ProductionTimeNeeded = 15;
        type.ResourcesCostPer1 = new float[4];
        ResourceTypes.Add(type);
    }
}
