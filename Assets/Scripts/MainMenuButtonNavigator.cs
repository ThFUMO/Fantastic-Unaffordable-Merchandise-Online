using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace THFUMO
{
    public class MainMenuButtonNavigator : ButtonNavigatorBase
    {
        private List<TextMeshProUGUI> childTexts = new();

        private int currentButton = 0;

        public override int CurrentButton
        {
            get => currentButton;
            set
            {
                currentButton = value;
                currentButton = Utilities.Repeat(currentButton, 0, childTexts.Count - 1);
                UpdateHighlight();
            }
        }

        public override int ButtonCount
        {
            get => childTexts.Count;
        }

        private bool hasPressedArrowKey = false;

        private AudioClip selectionSoundEffect;

        [SerializeField]
        private AudioManagerBase audioManager;

        [SerializeField]
        private AssetHolder<AssetKey, AudioClip> audioHolder;

        [SerializeField]
        private Color outlineColor;

        [SerializeField]
        private float outlineWidth;

        private Controls controls;

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
            controls = new();
            controls.Enable();
            controls.UI.MoveUp.performed += MoveUp_performed;
            controls.UI.MoveDown.performed += MoveDown_performed;
            selectionSoundEffect = audioHolder.GetAsset(AssetKey.UISelectionSoundEffect);
        }

        private void MoveUp_performed(InputAction.CallbackContext context)
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            CurrentButton--;
            hasPressedArrowKey = true;
            audioManager.PlaySoundEffect(selectionSoundEffect);
            UpdateHighlight();
        }

        private void MoveDown_performed(InputAction.CallbackContext context)
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            CurrentButton++;
            hasPressedArrowKey = true;
            audioManager.PlaySoundEffect(selectionSoundEffect);
            UpdateHighlight();
        }

        private void UpdateHighlight()
        {
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

