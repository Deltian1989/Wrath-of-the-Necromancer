using UnityEngine;
using WotN.Common.Managers;

namespace WotN.Common.Utils
{
    public class GameSceneDisclaimer : MonoBehaviour
    {
        void Start()
        {
            if (GameManager.Instance != null)
            {
                Destroy(gameObject);
            }
        }
    }
}


