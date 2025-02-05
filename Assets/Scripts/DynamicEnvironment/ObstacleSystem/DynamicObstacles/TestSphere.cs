using PoolFactory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    public class TestSphere : DynamicObstacle
    {
        private void OnEnable()
        {
            RigidBody = gameObject.GetComponent<Rigidbody>();
        }

        public override void Spawn(Vector3 spawnPosition, Vector3 runnerPosition, BoxCollider platformCollider)
        {
            gameObject.SetActive(true);
            transform.position = spawnPosition;
            RigidBody.useGravity = true;
            RigidBody.linearVelocity = Vector3.back * 50f;
        }
    }
}