using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker
{
    
    public float AI_DecisionSpeed = 10;

    private Fighting fighting;
    private Unit unit;

    private bool InventoryFull = false;
    private float MaximumTreeSearchingDistance = 75;
    private float timer_AIDecisions = 5;


    void Update()
    {
        if (InventoryFull == false)
        {
            GatherTrees();
        }
        else
        {
            ReturnResources();
        }
    }
    
    private void OnUnitKill(Unit victim = null)
    {
        Debug.Log("Lumber gathered");
        for (int i = 0; i < unit.ResourcesCarried.Length; i++)
        {
            if (unit.ResourcesCarried[i] == unit.ResourcesCarriedMaximum)
            {
                InventoryFull = true;
                timer_AIDecisions = AI_DecisionSpeed;
            }
        }
    }
    private Unit FindNearestTree()
    {
        return UnitLogic.FindNearestObject(unit.position, unit.game.unitsController.Trees, MaximumTreeSearchingDistance);
    }
    private void TransferResources(Unit target, float TransferingDistance = 4.5f)
    {
        if (Vector3.Distance(unit.position, target.position) < TransferingDistance)
        {
            for (int i = 0; i < unit.ResourcesCarried.Length; i++)
            {
                unit.state.ResourcesAmount[i] += unit.ResourcesCarried[i];
                unit.ResourcesCarried[i] = 0;
            }
            InventoryFull = false;
        }
    }
    public void GatherTrees()
    {
        if (timer_AIDecisions > AI_DecisionSpeed)
        {
            timer_AIDecisions = 0;
            Unit tree = FindNearestTree();
            if (tree != null)
            {
                if (unit.game.pf.GetWayPath(unit, tree.position))
                {

                }
                fighting.CurrentTarget = tree;
            }
            else
            { //roaming

            }
        }
        else
        {
            timer_AIDecisions += Time.deltaTime;
        }
    }
    public void ReturnResources()
    {
        if (timer_AIDecisions > AI_DecisionSpeed)
        {
            timer_AIDecisions = 0;
            unit.game.pf.GetWayPath(unit, unit.state.Townhall.position);
            TransferResources(unit.state.Townhall);
        }
        else
        {
            timer_AIDecisions += Time.deltaTime;
        }
    }
}
