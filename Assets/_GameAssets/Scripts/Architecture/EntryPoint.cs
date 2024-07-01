using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClickerTest.Architecture
{
    public class EntryPoint : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene("Game");
        }
    }
}