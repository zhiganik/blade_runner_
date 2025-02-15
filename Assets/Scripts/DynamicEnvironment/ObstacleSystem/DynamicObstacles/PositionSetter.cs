using UnityEngine;

namespace Assets.DynamicEnvironment.ObstacleSystem.DynamicObstacles
{
    public static class PositionSetter 
    {
        public static Vector3 ClampSpawnPosition(Vector3 spawnPosition, BoxCollider spawnCollider, BoxCollider platformCollider)
        {
            if (spawnCollider.bounds.size.z >= platformCollider.bounds.size.z)
            {
                return platformCollider.bounds.center;
            }

            Vector3 rightPlatformEdge = (platformCollider.bounds.center + platformCollider.bounds.extents);
            Vector3 leftPlatformEdge = (platformCollider.bounds.center - platformCollider.bounds.extents);

            float rightMax = (rightPlatformEdge - spawnCollider.bounds.extents).z;
            float leftMax = (leftPlatformEdge + spawnCollider.bounds.extents).z;

            spawnPosition = new Vector3(spawnPosition.x,
                                        spawnPosition.y,
                                        Mathf.Clamp(spawnPosition.z, leftMax, rightMax));
            return spawnPosition;
        }
        public static Vector3 ClampSpawnPosition(Vector3 spawnPosition, CapsuleCollider spawnCollider, BoxCollider platformCollider)
        {
            if (spawnCollider.bounds.size.z >= platformCollider.bounds.size.z)
            {
                return platformCollider.bounds.center;
            }

            Vector3 rightPlatformEdge = (platformCollider.bounds.center + platformCollider.bounds.extents);
            Vector3 leftPlatformEdge = (platformCollider.bounds.center - platformCollider.bounds.extents);

            float rightExtremum = (rightPlatformEdge - spawnCollider.bounds.extents).z;
            float leftExtremum = (leftPlatformEdge + spawnCollider.bounds.extents).z;

            spawnPosition = new Vector3(spawnPosition.x,
                                        spawnPosition.y,
                                        Mathf.Clamp(spawnPosition.z, leftExtremum, rightExtremum));
            return spawnPosition;
        }
    }
}
