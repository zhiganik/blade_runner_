using UnityEngine;

namespace PoolFactory
{
    public class PrefabPoolFactory<T> : IPoolFactory<T> where T : MonoBehaviour {

        private readonly GameObject _objectPrefab;
        private readonly string _objectName;

        private GameObject poolParent;
        private int _index = 0;
        private GameObject _parent;

        private const string ParentName = "PoolHandler";
        
        public PrefabPoolFactory(GameObject objectPrefab, GameObject parent)
        {
            _objectPrefab = objectPrefab;
            poolParent = parent;
            CreateDefaultParent(objectPrefab.name);
        }

        public T Create()
        {
            var currentGameObject = Object.Instantiate(_objectPrefab, _parent.transform);
            currentGameObject.name = _objectName + _index;
            var objectOfType = currentGameObject.GetComponent<T>();
            _index++;
            objectOfType.gameObject.SetActive(false);
            return objectOfType;
        }

        private void CreateDefaultParent(string objectName)
        {
            _parent = new GameObject(objectName + ParentName);
            _parent.transform.parent = poolParent.transform;
        }
    }
}