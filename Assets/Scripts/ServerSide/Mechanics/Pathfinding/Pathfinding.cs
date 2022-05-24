using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    

    public Map map;
    short[,] DistancesMap;


    short CurrentDistance = 0;
    public List<Vector2Int> Way = new List<Vector2Int>();

    Stack<Vector2Int>[] ToCheck;
    private short MaxSearchDistance = 150;

    bool BCurrentStackTurn = false;
    int CurrentStackTurn
    {
        get
        {
            return BoolToInt(BCurrentStackTurn);
        }
    }

    public Pathfinding(Map m)
    {
        map = m;
    }
    public bool GetWayPath(Unit MovingUnit, Vector3 Target)
    {
        bool result = CalculateWay(MovingUnit.position, Target);
        if (result)
        {
            MovingUnit.Way = Way;
        }
        return result;
    }
    public bool GetWayPath(Vector3 From, Vector3 Target)
    {
        return CalculateWay(From, Target);
    }

    private bool CalculateWay(Vector3 from, Vector3 target) 
    {
        Vector2Int From = ConvertVector3(from);
        Vector2Int Target = ConvertVector3(target);
        DistancesMap = new short[map.MapSizeX, map.MapSizeY];
        CurrentDistance = 0;
        ToCheck = new Stack<Vector2Int>[2];
        ToCheck[0] = new Stack<Vector2Int>();
        ToCheck[1] = new Stack<Vector2Int>();
        BCurrentStackTurn = false;
        ToCheck[CurrentStackTurn].Push(Target);

        bool found = false;
        while (found == false && CurrentDistance < MaxSearchDistance)
        {
            found = IterateToCheckList(From);
        }
        RestoreWay(From, Target);
        return found;
    }
    private bool IterateToCheckList(Vector2Int Target)
    {
        foreach (var v in ToCheck[CurrentStackTurn])
        {
            if (v == Target)
            { //Target is found
                return true;
            }
            else
            {
                DistancesMap[v.x, v.y] = CurrentDistance; //setting this point to distance map
                GetNeighbours(v, BoolToInt(!BCurrentStackTurn)); //adding neighbour patches to next stack
            }
        }
        ToCheck[CurrentStackTurn].Clear();
        CurrentDistance++;
        BCurrentStackTurn = !BCurrentStackTurn;
        return false;
    }
    private void GetNeighbours(Vector2Int point, int StackToAdd)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                Vector2Int current = new Vector2Int(point.x + x, point.y + y);
                if (ToCheck[0].Contains(current) == false && ToCheck[1].Contains(current) == false && DistancesMap[current.x, current.y] == 0 && map.ObstaclesMap[current.x, current.y] == false)
                {
                    ToCheck[StackToAdd].Push(current);
                }
            }
        }
    }
    private Vector2Int GetPartOfReturningWay(Vector2Int CurrentPoint)
    {
        short MinimalDistance = MaxSearchDistance;
        Vector2Int MinimalDistancePath = Vector2Int.zero;
        for (int y = 0; y != -2; y++)
        {
            for (int x = 0; x != -2; x++)
            {
                //Debug.Log("path " + new Vector2Int(CurrentPoint.x + x, CurrentPoint.y + y) + " distance: " + DistancesMap[CurrentPoint.x + x, CurrentPoint.y + y]);
                if (DistancesMap[CurrentPoint.x + x, CurrentPoint.y + y] < MinimalDistance && DistancesMap[CurrentPoint.x + x, CurrentPoint.y + y] != 0)
                {
                    MinimalDistance = DistancesMap[CurrentPoint.x + x, CurrentPoint.y + y];
                    MinimalDistancePath = new Vector2Int(CurrentPoint.x + x, CurrentPoint.y + y);
                  //  Debug.Log("Minimal distance out of  " + CurrentPoint + " is "  + MinimalDistancePath + " : " + MinimalDistance);
                }
                if (x == -1) x = -3;
                if (x == 1) x = -2;
            }
            if (y == -1) y = -3;
            if (y == 1) y = -2;
        }
        return MinimalDistancePath;
    }
    private void RestoreWay(Vector2Int StartingPosition, Vector2Int EndPosition)
    {
        Way = new List<Vector2Int>();
        Way.Add(StartingPosition);
        for (int i = 0; i < CurrentDistance; i++)
        {
            Way.Add(GetPartOfReturningWay(Way[i]));
        }
        Way.Add(EndPosition);
    }
    private int BoolToInt(bool b)
    {
        if (b) return 1;
        else return 0;
    }
    private Vector2Int ConvertVector3(Vector3 v)
    {
        return new Vector2Int((int)v.x, (int)v.z);
    }
    public Vector3 Vector2IntToVector3(Vector2Int v, float y = 1)
    {
        return new Vector3(v.x, y, v.y);
    }
}
