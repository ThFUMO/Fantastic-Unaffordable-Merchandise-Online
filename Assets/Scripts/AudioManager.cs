using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THFUMO
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioSource music;
        private static AudioSource soundEffect;

        private void Start()
        {
            music = gameObject.AddComponent<AudioSource>();
            soundEffect = gameObject.AddComponent<AudioSource>();
        }

        public static void PlayMusic(AudioClip audioClip, float volume = 1)
        {
            music.PlayOneShot(audioClip, volume);
        }

        public static void PlaySoundEffect(AudioClip audioClip, float volume = 1)
        {
            soundEffect.PlayOneShot(audioClip, volume);
        }

        public static void SetMusicVolume(float volume)
        {
            music.volume = volume;
        }

        public static void SetSoundEffectVolume(float volume)
        {
            soundEffect.volume = volume;
        }
    }
}
