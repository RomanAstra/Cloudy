using UnityEngine;

namespace Cloudy
{
    public sealed class WeaponHierarchy : MonoBehaviour
    {
        public Transform FirePoint;
        public ParticleSystem FireParticles;
        public AudioClip FireAudioClip;

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
    }
}
