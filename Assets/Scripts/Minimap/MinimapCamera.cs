using UnityEngine;

namespace WotN.Minimap
{
    public class MinimapCamera : MonoBehaviour
    {
        [SerializeField]
        private float movementsSmoothing = 6;

        [SerializeField]
        private GameObject _player;

        private Camera camera;

        void Awake()
        {
            camera=GetComponent<Camera>();
        }

        void Update()
        {
            if (_player != null)
                transform.position = Vector3.Lerp(transform.position, new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z), movementsSmoothing * Time.deltaTime);
        }

        public void SetPlayerToFollow(GameObject player)
        {
            _player = player;
            transform.position = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        }

        public Camera GetCamera()
        {
            if (!camera)
                camera = GetComponent<Camera>();

            return camera;
        }
    }
}

