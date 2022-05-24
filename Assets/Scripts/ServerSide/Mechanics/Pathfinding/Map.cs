using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public Vector3 StartingPosition = new Vector3(50, 0, 50);
    public int MapSizeX = 500;
    public int MapSizeY = 500;
    public bool[,] ObstaclesMap;

    

    public void SetupMap()
    {
        ObstaclesMap = new bool[MapSizeX, MapSizeY];

    }
    public void ApplyObstacle(Unit Obstacle, bool RadiusType = false)
    {
        if (RadiusType)
        {
            for (int y =  -Obstacle.CollisionRadius; y <= Obstacle.CollisionRadius; y++)
            {
                for (int x = -Obstacle.CollisionRadius; x <= Obstacle.CollisionRadius; x++)
                {
                      ObstaclesMap[(int)Obstacle.position.x + x, (int)Obstacle.position.z + y] = true;
                }
            }
        }
        else
        {
            for (int y = 0; y < Obstacle.CollisionRadius; y++)
            {
                for (int x = 0; x < Obstacle.CollisionRadius; x++)
                {
                      ObstaclesMap[(int)Obstacle.position.x + x, (int)Obstacle.position.z + y] = true;
                }
            }
        }
    }
    
}
