using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolFactory;

namespace BladeRunner
{
    public enum ObstacleType
    {
        TestSphere,
        TestCube,
    }
    public abstract class Obstacle : MonoBehaviour, IPoolObject
    {
        [SerializeField] protected ObstacleType type;
        [SerializeField] protected int startCountInPool;
        public int PoolID { get; set; }
        public ObstacleType Type => type;
        public int StartCountInPool => startCountInPool;
        protected Rigidbody RigidBody { get;  set; }
        public abstract void Spawn(Vector3 spawnPosition, Vector3 runnerPosition,
                                   Vector3 forceDir, BoxCollider platformCollider);
        public void Reset()
        {
            gameObject.SetActive(false);

            if (RigidBody != null) RigidBody.useGravity = false;

            gameObject.transform.position = Vector3.zero;
        }
    }
}
