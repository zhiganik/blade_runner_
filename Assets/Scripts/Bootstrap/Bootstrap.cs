using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            InitializeGameServices();;
        }

        private async void InitializeGameServices()
        {
            await InitializeGameAsync();
        }

        private async UniTask InitializeGameAsync()
        {
            await SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
    }
}