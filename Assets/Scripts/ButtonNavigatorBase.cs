using UnityEngine;

namespace THFUMO
{
    public abstract class ButtonNavigatorBase : MonoBehaviour
    {
        public abstract int CurrentButtonIndex { get; set; }

        public abstract ButtonId CurrentButtonId { get; set; }

        public abstract int ButtonCount { get; }
    }
}
