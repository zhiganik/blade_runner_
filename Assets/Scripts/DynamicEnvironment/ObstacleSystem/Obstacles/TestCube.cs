using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    public class TestCube : Obstacle
    {
        private BoxCollider boxCollider;
        private void OnEnable()
        {
            RigidBody = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
        }
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(4f);
            Reset();
        }
        public override void Spawn(Vector3 spawnPosition, Vector3 runnerPosition, Vector3 forceDir, BoxCollider platformCollider)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = ClampSpawnPosition(spawnPosition, platformCollider);
            RigidBody.useGravity = true;
        }
        private Vector3 ClampSpawnPosition(Vector3 spawnPosition, BoxCollider platformCollider)
        {
            if (boxCollider.bounds.size.z >= platformCollider.bounds.size.z)
            {
                return platformCollider.bounds.center;
            }
            Vector3 rightPlatformEdge = (platformCollider.bounds.center + platformCollider.bounds.extents);
            Vector3 leftPlatformEdge = (platformCollider.bounds.center - platformCollider.bounds.extents);

            float rightExtremum = (rightPlatformEdge - boxCollider.bounds.extents).z;
            float leftExtremum = (leftPlatformEdge + boxCollider.bounds.extents).z;

            spawnPosition = new Vector3(spawnPosition.x,
                                        spawnPosition.y,
                                        Mathf.Clamp(spawnPosition.z, leftExtremum, rightExtremum));
            return spawnPosition;
        }
    }
}
