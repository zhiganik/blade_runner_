using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    public class TestCylinder : DynamicObstacle
    {
        private MeshCollider boxCollider;
        private void OnEnable()
        {
            RigidBody = GetComponent<Rigidbody>();
            boxCollider = GetComponent<MeshCollider>();
        }

        public override void Spawn(Vector3 spawnPosition, Vector3 runnerPosition, BoxCollider platformCollider)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = spawnPosition;
            RigidBody.useGravity = true;
            RigidBody.linearVelocity = Vector3.back * 50f;
            RigidBody.AddTorque(-500f, 0f, 0f, ForceMode.Acceleration);
        }
    }
}