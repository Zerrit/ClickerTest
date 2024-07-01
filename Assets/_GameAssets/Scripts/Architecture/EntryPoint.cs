using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;

namespace ClickerTest.Architecture
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private AssetReference _gameScene;

        private void Start()
        {
            SceneManager.LoadScene("Game");
            //Addressables.LoadSceneAsync(_gameScene);
        }
    }
}