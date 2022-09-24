using UnityEngine;

namespace WotN.Player
{
    public class PlayerSFXHandler : MonoBehaviour
    {
        private const string CobblestoneTag = "Cobblestone";
        private const string GrassTag = "Grass";
        private const string DirtTag = "Dirt";
        private const string StoneTag = "Stone";
        private const string DefaultMaskName = "Default";

        [SerializeField]
        private AudioClip[] footstepSoundsOnGrass;

        [SerializeField]
        private AudioClip[] footstepsSoundsOnCobblestone;

        [SerializeField]
        private AudioClip[] footstepsSoundsOnDirtPathway;

        [SerializeField]
        private AudioClip[] footstepsSoundsOnStonePathway;

        private AudioSource audioSource;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayFootstepSound()
        {
            AudioClip footstepSound = null;

            if (Physics.Raycast(transform.position,Vector3.down, out var hit, 3f,LayerMask.GetMask(DefaultMaskName)))
            {
                if (hit.transform.CompareTag(CobblestoneTag))
                {
                    var i = Random.Range(0, footstepsSoundsOnCobblestone.Length);

                    footstepSound = footstepsSoundsOnCobblestone[i];
                } else if (hit.transform.CompareTag(GrassTag))
                {
                    var i = Random.Range(0, footstepSoundsOnGrass.Length);

                    footstepSound = footstepSoundsOnGrass[i];
                } else if (hit.transform.CompareTag(DirtTag))
                {
                    var i = Random.Range(0, footstepsSoundsOnDirtPathway.Length);

                    footstepSound = footstepsSoundsOnDirtPathway[i];
                }
                else if (hit.transform.CompareTag(StoneTag))
                {
                    var i = Random.Range(0, footstepsSoundsOnStonePathway.Length);

                    footstepSound = footstepsSoundsOnStonePathway[i];
                }
            }

            audioSource.PlayOneShot(footstepSound);
        }
    }
}


