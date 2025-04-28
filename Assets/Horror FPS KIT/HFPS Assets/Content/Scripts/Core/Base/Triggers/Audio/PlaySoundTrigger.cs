using UnityEngine;

namespace HFPS.Systems
{

    public class PlaySoundTrigger : MonoBehaviour
    {
        [Header("Audio Settings")]
        public AudioClip soundClip;           
        [Range(0f, 1f)]
        public float volume = 0.8f;           

        [Header("Trigger Settings")]
        public bool playOnce = true;           
        public bool requirePlayerTag = true;  

        private bool hasPlayed = false;       
        private AudioSource audioSource;

        void Start()
        {
            
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = soundClip;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1f; 
        }

        void OnTriggerEnter(Collider other)
        {
           
            if (requirePlayerTag && !other.CompareTag("Player"))
                return;

            
            if (playOnce && hasPlayed)
                return;

            PlaySound();
        }

        void PlaySound()
        {
            if (soundClip != null)
            {
                audioSource.volume = volume;
                audioSource.Play();
                hasPlayed = true;
            }
            else
            {
                Debug.LogWarning("PlaySoundTrigger: Œ¥∑÷≈‰ AudioClip");
            }
        }
    }

}
