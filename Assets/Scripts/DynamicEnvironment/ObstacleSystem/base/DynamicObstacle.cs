using UnityEngine;
using PoolFactory;

namespace BladeRunner
{
    public enum DynamicObstacleType
    {
        TestSphere,
        TestCube,
        TestCylinder,
    }
    public abstract class DynamicObstacle : MonoBehaviour, IPoolObject
    {
        [SerializeField] protected DynamicObstacleType type;
        [SerializeField] protected int startCountInPool;
        public RythmSpawnSettings rythmSpawnSettings;
        public int PoolID { get; set; }
        public int StartCountInPool => startCountInPool;
        public DynamicObstacleType Type => type;
        public Rigidbody RigidBody { get;  protected set; }
        public abstract void Spawn(Vector3 spawnPosition, Vector3 runnerPosition, BoxCollider platformCollider);
        public void Reset()
        {
            gameObject.SetActive(false);

            if (RigidBody is not null) RigidBody.useGravity = false;

            gameObject.transform.position = Vector3.zero;
        }
    }
}
