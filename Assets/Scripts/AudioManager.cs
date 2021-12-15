using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THFUMO
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioSource AudioSource { get; set; }

        void Start()
        {
            AudioSource = GetComponent<AudioSource>();
            if (AudioSource == null)
            {
                Debug.LogWarning($"{name} has no {nameof(AudioSource)} attached.");
            }
        }
    }
}
