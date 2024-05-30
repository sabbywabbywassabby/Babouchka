using UnityEngine;

namespace Audio.Scrpits
{
    public class AudioBackground : MonoBehaviour
    {
        public AudioSource backgroundAudio;

        void Start()
        {
            backgroundAudio.Play();   
        }
    }
}
