using UnityEngine;

namespace Assets.Amazing_Assets.Curved_World.Example_Scenes.Files._2D.Scripts
{
    public class Perspective2D_Restarter : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                //SceneManager.LoadScene(Application.loadedLevelName);
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }
}
