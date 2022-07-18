using System;
using System.Collections.Generic;
using UnityEngine;

namespace THFUMO
{
    public abstract class ButtonNavigatorBase : MonoBehaviour
    {
        public abstract ThButton CurrentButton { get; set; }

        public abstract IEnumerable<ThButton> Buttons { get; }

        public abstract void MoveUp();

        public abstract void MoveDown();

        public abstract void MoveLeft();

        public abstract void MoveRight();

        public abstract event EventHandler<ThButtonPositionChangedEventArgs> PositionChanged;
    }
}
