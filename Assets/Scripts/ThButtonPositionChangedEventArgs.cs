using System;
using UnityEngine;

namespace THFUMO
{
    public class ThButtonPositionChangedEventArgs : EventArgs
    {
        public ThButton CurrentButton { get; set; }

        public ThButtonPositionChangedEventArgs(ThButton currentButton)
        {
            CurrentButton = currentButton;
        }
    }
}