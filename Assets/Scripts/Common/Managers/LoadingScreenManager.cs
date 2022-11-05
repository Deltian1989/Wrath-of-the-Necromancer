using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WotN.Common.Utils;

namespace WotN.Common.Managers
{
    public class LoadingScreenManager : GenericSingleton<LoadingScreenManager>
    {
        [SerializeField]
        [ChildGameObjectsOnly]
        private Canvas loadingCanvas;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Image loadingProgressImage;

        private AsyncOperation asyncLoad = null;

        public void LoadGameScene(int gameSceneID)
        {
            loadingCanvas.enabled = true;
            loadingProgressImage.fillAmount = 0;

            asyncLoad = new AsyncOperation();
            StartCoroutine(AsyncLoad(gameSceneID));
        }

        IEnumerator AsyncLoad(int sceneIndex)
        {
            asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

            while (!asyncLoad.isDone)
            {
                loadingProgressImage.fillAmount = asyncLoad.progress / 1f;

                yield return null;
            }

            // whatever scene is loaded, set always time scale to 1 in case, when the player paused the game and exited the game to let time scale be 0, until the main menu scene is completely loaded
            Time.timeScale = 1;

            ResetLoadingCanvas();
        }

        private void ResetLoadingCanvas()
        {
            asyncLoad = null;
            loadingCanvas.enabled = false;
            loadingProgressImage.fillAmount = 0;
        }
    }
}

