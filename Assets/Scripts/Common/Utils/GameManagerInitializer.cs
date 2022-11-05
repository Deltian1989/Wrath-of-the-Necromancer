using Sirenix.OdinInspector;
using UnityEngine;
using WotN.Common.Managers;

namespace WotN.Common.Utils
{
    public class GameManagerInitializer : MonoBehaviour
    {
        [SerializeField]
        [AssetsOnly]
        private GameManager gameManager;

        [SerializeField]
        [AssetsOnly]
        private LoadingScreenManager loadingScreenManager;

        void Awake()
        {
            if (!GameManager.Instance)
                Instantiate(gameManager);

            if (!LoadingScreenManager.Instance)
                Instantiate(loadingScreenManager);
        }
    }
}

