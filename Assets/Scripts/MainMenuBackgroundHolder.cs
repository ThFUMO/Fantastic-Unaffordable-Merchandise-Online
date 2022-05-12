using UnityEngine;

namespace THFUMO
{
    public class MainMenuBackgroundHolder : AssetHolder<AssetKey, Sprite>
    {
        [SerializeField]
        private Sprite cirnoBackground;

        [SerializeField]
        private Sprite youmuBackground;
        
        public override Sprite GetAsset(AssetKey key)
        {
            return key switch
            {
                AssetKey.CirnoBackground => cirnoBackground,
                AssetKey.YoumuBackground => youmuBackground,
                _ => null
            };
        }
    }
}