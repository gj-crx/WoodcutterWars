using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsController
{
    public List<Unit> AllUnits = new List<Unit>();
    public List<Unit> RegularUnits = new List<Unit>();
    public List<Building> Buildings = new List<Building>();
    public List<Unit> Trees = new List<Unit>();


    public float BuildingProductionTimeScale = 1;
    private float timer_BuildingProduction = 0;

    public void ControlUnits()
    {
        foreach (Unit u in RegularUnits)
        {
            u.unitcontrolling();
        }
    }
    public void ControlBuildings()
    {
        if (timer_BuildingProduction > BuildingProductionTimeScale)
        {
            foreach (Building b in Buildings)
            {
                b.LocalTickTimerExecution(1);
            }
        }
        else
        {
            timer_BuildingProduction += Time.deltaTime;
        }
    }
    /// <summary>
    /// 0 - regular unit, 1 - building, 2 - tree
    /// </summary>
    public void AddNewUnit(Unit unit, sbyte UnitClass = 0, Building building = null)
    {
        AllUnits.Add(unit);
        unit.ID = AllUnits.IndexOf(unit);
        if (UnitClass == 0)
        {
            RegularUnits.Add(unit);
        }
        if (UnitClass == 1)
        {
            if (building != null)
            {
                Buildings.Add(building);
                building.IDInBuildingsPool = Buildings.IndexOf(building);
            }
            else
            {
                Debug.Log("Building type not assigned");
                RegularUnits.Add(unit);
            }
        }
        if (UnitClass == 2)
        {
            Trees.Add(unit);
        }
    }

    public int GetNewUnitID()
    {
        return 1;
    }
}
