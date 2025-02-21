using UnityEngine;

namespace Assets.DynamicEnvironment.ObstacleSystem
{
    public interface IDynamicObstacleSystemService
    {
        public DynamicObstacle SpawnObstacle(DynamicObstacleType type, Vector3 spawnPosition);
        public void DestroyObstacle(DynamicObstacle obstacle);
    }
}