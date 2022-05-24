using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClientSideLogic
{

    public static class ClientUnitController
    {
        [Header("All game objects")]
        public static List<UnitClientSide> AllUnits = new List<UnitClientSide>();
        public static List<UnitClientSide> trees = new List<UnitClientSide>();
        public static List<UnitClientSide> units = new List<UnitClientSide>();
        public static List<UnitClientSide> buildings = new List<UnitClientSide>();



        /// <summary>
        /// 0 - tree, 1 - regular unit, 2 - building; Function returns ID in units pool
        /// </summary>
        public static int AddUnitToList(UnitClientSide g, int UnitType = 0)
        {
            AllUnits.Add(g);
            if (UnitType == 0)
            {
                //add sharding system
                trees.Add(g);
                return trees.IndexOf(g);
            }
            if (UnitType == 1)
            {
                units.Add(g);
                return units.IndexOf(g);
            }
            if (UnitType == 2)
            {
                buildings.Add(g);
                return buildings.IndexOf(g);
            }
            return 0;
        }


        public static void RemoveUnitFromList(UnitClientSide g, int UnitType = 0)
        {
            if (UnitType == 0)
            {
                //add sharding system
                trees.Remove(g);
            }
            if (UnitType == 1)
            {
                units.Remove(g);
            }
            if (UnitType == 2)
            {
                buildings.Remove(g);
            }
        }

        private static bool ListContainsUnit(int UnitObjectID, List<UnitClientSide> list)
        {
            foreach (var unit in list)
            {
                if (unit.ID == UnitObjectID)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool UnitExistInClient(int UnitObjectID)
        {
            if (ListContainsUnit(UnitObjectID, units) || ListContainsUnit(UnitObjectID, buildings) || ListContainsUnit(UnitObjectID, trees))
            {
                return true;
            }
            else return false;
        }
        private static UnitClientSide TryToFindUnitInList(int UnitObjectID, List<UnitClientSide> list)
        {
            foreach (var unit in list)
            {
                if (unit.ID == UnitObjectID)
                {
                    return unit;
                }
            }
            return null;
        }
        public static UnitClientSide FindUnitByID(int UnitObjectID)
        {
            UnitClientSide UnitToFind = TryToFindUnitInList(UnitObjectID, units);
            if (UnitToFind != null)
            {
                return UnitToFind;
            }
            UnitToFind = TryToFindUnitInList(UnitObjectID, buildings);
            if (UnitToFind != null)
            {
                return UnitToFind;
            }
            UnitToFind = TryToFindUnitInList(UnitObjectID, trees);
            if (UnitToFind != null)
            {
                return UnitToFind;
            }
            return null;
        }
        public static GameObject GetUnitPrefab(sbyte UnitTypeID, sbyte UnitClassID)
        {
            return PrefabManager.Singleton.prefabs_AllUnits[UnitTypeID];
            if (UnitClassID == 0)
            {
                return PrefabManager.Singleton.prefabs_Units[UnitTypeID];
            }
            if (UnitClassID == 1)
            {
                return PrefabManager.Singleton.prefabs_Buildings[UnitTypeID];
            }
            if (UnitClassID == 2)
            {
                return PrefabManager.Singleton.prefabs_Trees[UnitTypeID];
            }
            return null;
        }

        public static UnitClientSide CreateUnitRepresentation(Unit.UnitSerializableData data)
        {
            UnitClientSide NewUnit = GameObject.Instantiate(GetUnitPrefab(data.UnitTypeID, data.UnitClassID), data.position, Quaternion.identity).GetComponent<UnitClientSide>();
            NewUnit.ApplyRecievedData(data);
            return NewUnit;
        }
    }
}
