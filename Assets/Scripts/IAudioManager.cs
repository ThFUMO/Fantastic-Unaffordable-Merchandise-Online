using UnityEngine;

namespace THFUMO
{
    public interface IAudioManager
    {
        public void PlayMusic(AudioClip audioClip, float volume = 1);

        public void PlaySoundEffect(AudioClip audioClip, float volume = 1);
    }
}
