using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    public interface IObstacleSystemService
    {
        public Obstacle SpawnObstacle(ObstacleType type, Vector3 spawnPosition);
        public Obstacle SpawnObstacle(ObstacleType type, Vector3 spawnPosition, float force);
        public void DestroyObstacle(Obstacle obstacle);
    }
}