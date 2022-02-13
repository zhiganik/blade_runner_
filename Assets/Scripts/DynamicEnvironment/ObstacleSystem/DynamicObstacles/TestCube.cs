using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    public class TestCube : DynamicObstacle
    {
        private BoxCollider boxCollider;
        private void OnEnable()
        {
            RigidBody = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
        }

        public override void Spawn(Vector3 spawnPosition, Vector3 runnerPosition, BoxCollider platformCollider)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = spawnPosition;
            RigidBody.useGravity = true;

            RigidBody.velocity = Vector3.back * 50f;
        }
    }
}
