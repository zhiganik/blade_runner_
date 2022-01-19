using UnityEngine;

namespace AnimationComponents
{
    public class CircleMove : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float width;
        [SerializeField] private float height;

        [SerializeField] private GameObject[] objects;
        

        private float _timeCounter = 0;
        
        private void Update()
        {
            DoCircleMove();
        }

        private void DoCircleMove()
        {
            //var targetObject = objects[Random.Range(0, objects.Length)];
            var targetTransform = transform;
            
            _timeCounter += Time.deltaTime;

            var x = Mathf.Cos(_timeCounter) * width;
            var y = Mathf.Sin(_timeCounter) * height;

            targetTransform.position = new Vector3(x, y, targetTransform.position.z);
            targetTransform.rotation = Quaternion.Euler(x, y, targetTransform.rotation.z);
        }
    }
}