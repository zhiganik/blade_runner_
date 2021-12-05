using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    public interface IDynamicObstacleSystemService
    {
        public DynamicObstacle SpawnObstacle(DynamicObstacleType type, Vector3 spawnPosition);
        public DynamicObstacle SpawnObstacle(DynamicObstacleType type, Vector3 spawnPosition, float force);
        public void DestroyObstacle(DynamicObstacle obstacle);
    }
}