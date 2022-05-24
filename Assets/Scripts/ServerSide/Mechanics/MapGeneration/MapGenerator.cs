using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public static class MapGenerator
{
    public static void GenerateTrees(Game GameToGenerate, int AmountOfTrees)
    {
        for (int i = 0; i < AmountOfTrees; i++)
        {
            new Unit(GameToGenerate, GameToGenerate.map.StartingPosition + new Vector3(Random.Range(0, GameToGenerate.map.MapSizeX), 0, Random.Range(0, GameToGenerate.map.MapSizeX)), null,
                PrefabManager.Singleton.prefabs_Trees[0].GetComponent<UnitTypePrefabVariant>().UnitTypeID, 2);
        }
    }
}
