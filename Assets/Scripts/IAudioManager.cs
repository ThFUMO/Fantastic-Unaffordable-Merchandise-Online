using UnityEngine;

namespace THFUMO
{
    public interface IAudioManager
    {
        public void PlayMusic(AudioClip audioClip, float volume);

        public void PlaySoundEffect(AudioClip audioClip, float volume);
    }
}
