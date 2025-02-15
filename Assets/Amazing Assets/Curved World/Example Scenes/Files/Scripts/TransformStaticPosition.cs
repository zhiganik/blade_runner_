using Assets.Amazing_Assets.Curved_World.Scripts.CurvedWorld;
using UnityEngine;

namespace Assets.Amazing_Assets.Curved_World.Example_Scenes.Files.Scripts
{
    public class TransformStaticPosition : MonoBehaviour
    {
        public CurvedWorldController curvedWorldController;

        Vector3 originalPosition;
        Quaternion originalRotation;

        Vector3 forward;
        Vector3 right;

        private void Start()
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;

            forward = transform.forward;
            right = transform.right;
        }

        void Update()
        {
            if (curvedWorldController != null)
            {
                //Transform position
                transform.position = curvedWorldController.TransformPosition(originalPosition);

                //Transform normal (calcualte rotation)
                transform.rotation = curvedWorldController.TransformRotation(originalPosition, forward, right);
            }
        }
    }
}