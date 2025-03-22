using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }

        public void OpenOptions()
        {
            SceneManager.LoadScene(2);
        }

        public void OpenMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void ExitGame()
        {
            Debug.Log("Exit");
            Application.Quit();
        }
    }
}
