using PoolFactory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    public class TestSphere : Obstacle
    {
        private void OnEnable()
        {
            RigidBody = gameObject.GetComponent<Rigidbody>();
        }
        private void Start()
        {
            StartCoroutine(Disable());
        }
        public override void Spawn(Vector3 spawnPosition, Vector3 runnerPosition, Vector3 forceDir, BoxCollider platformCollider)
        {
            gameObject.SetActive(true);
            transform.position = spawnPosition;
            RigidBody.useGravity = true;

            RigidBody.velocity = (runnerPosition - transform.position).normalized * 50f;
        }

        private IEnumerator Disable()
        {
            yield return new WaitForSeconds(2f);
            Reset();
        }
    }
}