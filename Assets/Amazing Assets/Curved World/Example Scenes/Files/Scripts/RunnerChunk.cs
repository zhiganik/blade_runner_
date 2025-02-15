using UnityEngine;

namespace Assets.Amazing_Assets.Curved_World.Example_Scenes.Files.Scripts
{
    public class RunnerChunk : MonoBehaviour
    {
        public ChunkSpawner spawner;
        

        void Update()
        {
            transform.Translate(spawner.moveDirection * spawner.movingSpeed * Time.deltaTime);
        }

        void FixedUpdate()
        {
            switch (spawner.axis)
            {
                case ChunkSpawner.AXIS.XPositive:
                    if (transform.position.x > spawner.destoryZone)
                        spawner.DestroyChunk(this);
                    break;

                case ChunkSpawner.AXIS.XNegative:
                    if (transform.position.x < -spawner.destoryZone)
                        spawner.DestroyChunk(this);
                    break;

                case ChunkSpawner.AXIS.ZPositive:
                    if (transform.position.z > spawner.destoryZone)
                        spawner.DestroyChunk(this);
                    break;

                case ChunkSpawner.AXIS.ZNegative:
                    if (transform.position.z < -spawner.destoryZone)
                        spawner.DestroyChunk(this);
                    break;
            }
            
        }
    }
}