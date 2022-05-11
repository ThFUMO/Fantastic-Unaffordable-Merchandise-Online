using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace THFUMO
{
    public class MainMenuButtonNavigator : ButtonNavigatorBase
    {
        private List<TextMeshProUGUI> childTexts = new();

        public override int CurrentButton { get; set; } = 0;

        private bool hasPressedArrowKey = false;

        [SerializeField]
        private AudioClip select;

        [SerializeField]
        private AudioManagerBase audioManager;

        [SerializeField]
        private Color outlineColor;

        [SerializeField]
        private float outlineWidth;

        private Inputs inputs;

        private void Start()
        {
            foreach (Transform child in transform)
            {
                childTexts.Add(child.GetComponent<TextMeshProUGUI>());
            }
            if (childTexts.Count != 0)
            {
                childTexts[0].outlineColor = outlineColor;
                childTexts[0].outlineWidth = outlineWidth;
                childTexts[0].gameObject.Reactivate();
            }
            inputs = new();
            inputs.Enable();
            inputs.UI.MoveUp.performed += MoveUp;
            inputs.UI.MoveDown.performed += MoveDown;
        }

        private void MoveUp(InputAction.CallbackContext context)
        {
            CurrentButton--;
            hasPressedArrowKey = true;
            UpdateHighlight();
        }

        private void MoveDown(InputAction.CallbackContext context)
        {
            CurrentButton++;
            hasPressedArrowKey = true;
            UpdateHighlight();
        }

        private void UpdateHighlight()
        {
            audioManager.PlaySoundEffect(select);
            CurrentButton = Utilities.Repeat(CurrentButton, 0, childTexts.Count - 1);
            if (childTexts[CurrentButton] == null)
            {
                Debug.LogWarning($"{childTexts[CurrentButton].gameObject.name} has no {nameof(TextMeshProUGUI)} attached.");
            }
            else
            {
                childTexts[CurrentButton].outlineColor = outlineColor;
                foreach (TextMeshProUGUI text in childTexts)
                {
                    text.outlineWidth = 0;
                }
                childTexts[CurrentButton].outlineWidth = outlineWidth;
                foreach (TextMeshProUGUI text in childTexts)
                {
                    text.gameObject.Reactivate();
                }
            }
            hasPressedArrowKey = false;
        }
    }
}

