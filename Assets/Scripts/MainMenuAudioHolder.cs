using UnityEngine;

namespace THFUMO
{
    public class MainMenuAudioHolder : AssetHolder<AssetKey, AudioClip>
    {
        [SerializeField]
        private AudioClip selectionSoundEffect;

        [SerializeField]
        private AudioClip confirmationSoundEffect;

        [SerializeField]
        private AudioClip cancellationSoundEffect;
        
        public override AudioClip GetAsset(AssetKey key)
        {
            return key switch
            {
                AssetKey.UISelectionSoundEffect => selectionSoundEffect,
                AssetKey.UIConfirmationSoundEffect => confirmationSoundEffect,
                AssetKey.UICancelSoundEffect => cancellationSoundEffect,
                _ => null
            };
        }
    }
}