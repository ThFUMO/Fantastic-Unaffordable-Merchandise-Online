using UnityEngine;

namespace THFUMO
{
    public abstract class AudioManagerBase : MonoBehaviour
    {
        public abstract void PlayMusic(AudioClip audioClip, float volume = 1);

        public abstract void PlaySoundEffect(AudioClip audioClip, float volume = 1);
    }
}
