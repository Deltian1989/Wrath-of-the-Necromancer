using UnityEngine;
using WotN.Common.Managers;

namespace WotN.Common.Utils
{
    public class GameManagerInitializer : MonoBehaviour
    {
        [SerializeField]
        private GameManager gameManager;

        [SerializeField]
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

