using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THFUMO
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        private static AudioSource music;

        private static AudioSource soundEffect;

        private void Start()
        {
            music = gameObject.AddComponent<AudioSource>();
            soundEffect = gameObject.AddComponent<AudioSource>();
        }

        public void PlayMusic(AudioClip audioClip, float volume = 1)
        {
            music.PlayOneShot(audioClip, volume);
        }

        public void PlaySoundEffect(AudioClip audioClip, float volume = 1)
        {
            soundEffect.PlayOneShot(audioClip, volume);
        }
    }
}
