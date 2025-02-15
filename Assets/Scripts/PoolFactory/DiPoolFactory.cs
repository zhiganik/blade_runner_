using UnityEngine;
using Zenject;

namespace Assets.PoolFactory
{
    public class DiPoolFactory<T> : IPoolFactory<T> where T : MonoBehaviour
    {
        private readonly GameObject _objectPrefab;
        private readonly string _objectName;
        private readonly PlaceholderFactory<T> _factory;
        
        private int _index = 0;
        private GameObject _parent;

        private const string ParentName = "PoolHandler";

        public DiPoolFactory(GameObject objectPrefab, PlaceholderFactory<T> factory)
        {
            _objectPrefab = objectPrefab;
            _objectName = objectPrefab.name;
            _factory = factory;
            CreateDefaultParent(objectPrefab.name);
        }

        public T Create()
        {
            var currentGameObject = _factory.Create();
            currentGameObject.transform.SetParent(_parent.transform);
            currentGameObject.name = _objectName + _index;
            currentGameObject.gameObject.SetActive(false);
            _index++;
            return currentGameObject;
        }

        private void CreateDefaultParent(string objectName)
        {
            _parent = new GameObject(objectName + ParentName);
        }
    }
}