using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace THFUMO
{
    public class MainMenuButtonNavigator : ButtonNavigatorBase
    {
        private List<TextMeshProUGUI> childTexts = new();

        private int currentButtonIndex = 0;

        public override int CurrentButtonIndex
        {
            get => currentButtonIndex;
            set
            {
                currentButtonIndex = value;
                currentButtonIndex = Utilities.Repeat(currentButtonIndex, 0, childTexts.Count - 1);
                UpdateHighlight();
            }
        }

        public override ButtonId CurrentButtonId
        {
            get
            {
                ButtonIdHolder buttonIdHolder = childTexts[CurrentButtonIndex].gameObject.GetComponent<ButtonIdHolder>();
                if (buttonIdHolder == null)
                {
                    Debug.LogError($"{childTexts[CurrentButtonIndex].gameObject.name} has no {nameof(ButtonIdHolder)} attached.");
                    return ButtonId.None;
                }
                return childTexts[CurrentButtonIndex].gameObject.GetComponent<ButtonIdHolder>().ButtonId;
            }
            set
            {
                for (int i = 0; i < childTexts.Count; i++)
                {
                    ButtonIdHolder buttonIdHolder = childTexts[i].gameObject.GetComponent<ButtonIdHolder>();
                    if (buttonIdHolder != null && buttonIdHolder.ButtonId == value)
                    {
                        CurrentButtonIndex = i;
                        UpdateHighlight();
                        return;
                    }
                }
                Debug.LogError($"No child button with the {nameof(ButtonId)} of {value} is found.");
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
            CurrentButtonIndex--;
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
            CurrentButtonIndex++;
            hasPressedArrowKey = true;
            audioManager.PlaySoundEffect(selectionSoundEffect);
            UpdateHighlight();
        }

        private void UpdateHighlight()
        {
            if (childTexts[CurrentButtonIndex] == null)
            {
                Debug.LogError($"{childTexts[CurrentButtonIndex].gameObject.name} has no {nameof(TextMeshProUGUI)} attached.");
            }
            else
            {
                childTexts[CurrentButtonIndex].outlineColor = outlineColor;
                foreach (TextMeshProUGUI text in childTexts)
                {
                    text.outlineWidth = 0;
                }
                childTexts[CurrentButtonIndex].outlineWidth = outlineWidth;
                foreach (TextMeshProUGUI text in childTexts)
                {
                    text.gameObject.Reactivate();
                }
            }
            hasPressedArrowKey = false;
        }
    }
}

