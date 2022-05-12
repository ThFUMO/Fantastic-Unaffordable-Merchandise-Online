using UnityEngine;

namespace THFUMO
{
    public abstract class AssetHolder<TKey, TValue> : MonoBehaviour
    {
        public abstract TValue GetAsset(TKey key);
    }
}
