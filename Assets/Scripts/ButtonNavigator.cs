using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MoreLinq.Extensions;

namespace THFUMO
{
    public class ButtonNavigator : ButtonNavigatorBase
    {
        [SerializeField]
        private ThButton initialButton;

        public override ThButton CurrentButton { get; set; }

        public override IEnumerable<ThButton> Buttons
        {
            get
            {
                List<ThButton> buttons = new();
                foreach (Transform transform in transform)
                {
                    ThButton button = transform.GetComponent<ThButton>();
                    if (button != null)
                    {
                        buttons.Add(button);
                    }
                }
                return buttons;
            }
        }

        public override event EventHandler<ThButtonPositionChangedEventArgs> PositionChanged;

        private void OnPositionChanged()
        {
            PositionChanged?.Invoke(this, new(CurrentButton));
        }

        public override void MoveUp()
        {
            IEnumerable<ThButton> buttons = Buttons.Where(button => button.Position.x == CurrentButton.Position.x);
            ThButton minY = buttons.MinBy(button => button.Position.y).First(), maxY = buttons.MaxBy(button => button.Position.y).First();
            if (CurrentButton.Position.y - 1 < minY.Position.y)
            {
                CurrentButton = maxY;
                OnPositionChanged();
                return;
            }
            else
            {
                CurrentButton = buttons.Where(button => button.Position.y < CurrentButton.Position.y).MaxBy(button => button.Position.y).First();
                OnPositionChanged();
                return;
            }
        }

        public override void MoveDown()
        {
            IEnumerable<ThButton> buttons = Buttons.Where(button => button.Position.x == CurrentButton.Position.x);
            ThButton minY = buttons.MinBy(button => button.Position.y).First(), maxY = buttons.MaxBy(button => button.Position.y).First();
            if (CurrentButton.Position.y + 1 > maxY.Position.y)
            {
                CurrentButton = minY;
                OnPositionChanged();
                return;
            }
            else
            {
                CurrentButton = buttons.Where(button => button.Position.y > CurrentButton.Position.y).MinBy(button => button.Position.y).First();
                OnPositionChanged();
                return;
            }
        }

        public override void MoveLeft()
        {
            IEnumerable<ThButton> buttons = Buttons.Where(button => button.Position.y == CurrentButton.Position.y);
            ThButton minX = buttons.MinBy(button => button.Position.x).First(), maxX = buttons.MaxBy(button => button.Position.x).First();
            if (CurrentButton.Position.x - 1 < minX.Position.x)
            {
                CurrentButton = maxX;
                OnPositionChanged();
                return;
            }
            else
            {
                CurrentButton = buttons.Where(button => button.Position.x < CurrentButton.Position.x).MaxBy(button => button.Position.x).First();
                OnPositionChanged();
                return;
            }
        }

        public override void MoveRight()
        {
            IEnumerable<ThButton> buttons = Buttons.Where(button => button.Position.y == CurrentButton.Position.y);
            ThButton minX = buttons.MinBy(button => button.Position.x).First(), maxX = buttons.MaxBy(button => button.Position.x).First();
            if (CurrentButton.Position.x + 1 > maxX.Position.x)
            {
                CurrentButton = minX;
                OnPositionChanged();
                return;
            }
            else
            {
                CurrentButton = buttons.Where(button => button.Position.x > CurrentButton.Position.x).MinBy(button => button.Position.x).First();
                OnPositionChanged();
                return;
            }
        }

        private void Awake()
        {
            CurrentButton = initialButton;
        }
    }
}
