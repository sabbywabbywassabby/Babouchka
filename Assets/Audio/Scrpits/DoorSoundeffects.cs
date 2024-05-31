using UnityEngine;

namespace Audio.Scripts
{
    public class DoorSoundEffects : MonoBehaviour
    {
        public AudioClip openingSound;
        public AudioClip closingSound;
        private bool open;
        private AudioSource audioSource;

        void Start()
        {
            open = false;
            audioSource = GetComponent<AudioSource>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!open && audioSource != null)
                {
                    audioSource.clip = openingSound;
                    audioSource.Play();
                    open = true;
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (open && audioSource != null)
                {
                    audioSource.Stop();
                    audioSource.clip = closingSound;
                    audioSource.Play();
                    open = false;
                }
            }
        }
    }
}