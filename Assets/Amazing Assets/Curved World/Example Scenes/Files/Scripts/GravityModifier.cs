using UnityEngine;

namespace Assets.Amazing_Assets.Curved_World.Example_Scenes.Files.Scripts
{
    public class GravityModifier : MonoBehaviour
    {
        public Vector3 gravity = new Vector3(0, -9.8f, 0);
      

        void Start()
        {
            Physics.gravity = gravity;
        }

        private void OnDisable()
        {
            //Restore
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }
    }
}