using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace THFUMO
{
    public class MainMenuButtonNavigator : MonoBehaviour
    {
        private AudioClip selectionSoundEffect;

        [SerializeField]
        private AudioManagerBase audioManager;

        [SerializeField]
        private AssetHolder<AssetKey, AudioClip> audioHolder;

        [SerializeField]
        private Color outlineColor;

        [SerializeField]
        private float outlineWidth;

        [SerializeField]
        private ButtonNavigatorBase buttonNavigator;

        private Controls controls;

        private void Start()
        {
            TextMeshProUGUI initialText = buttonNavigator.CurrentButton.GetComponent<TextMeshProUGUI>();
            if (initialText != null)
            {
                initialText.outlineColor = outlineColor;
                initialText.outlineWidth = outlineWidth;
                initialText.gameObject.Reactivate();
            }
            controls = new();
            controls.Enable();
            controls.UI.MoveUp.performed += MoveUp_performed;
            controls.UI.MoveDown.performed += MoveDown_performed;
            selectionSoundEffect = audioHolder.GetAsset(AssetKey.UISelectionSoundEffect);
            buttonNavigator.PositionChanged += UpdateHighlight;
        }

        private void MoveUp_performed(InputAction.CallbackContext context)
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            buttonNavigator.MoveUp();
            audioManager.PlaySoundEffect(selectionSoundEffect);
        }

        private void MoveDown_performed(InputAction.CallbackContext context)
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            buttonNavigator.MoveDown();
            audioManager.PlaySoundEffect(selectionSoundEffect);
        }

        private void UpdateHighlight(object sender, ThButtonPositionChangedEventArgs e)
        {
            foreach (ThButton button in buttonNavigator.Buttons)
            {
                TextMeshProUGUI text = button.GetComponent<TextMeshProUGUI>();
                if (text != null)
                {
                    text.outlineWidth = 0;
                }
            }
            TextMeshProUGUI currentButtonText = buttonNavigator.CurrentButton.GetComponent<TextMeshProUGUI>();
            if (currentButtonText != null)
            {
                currentButtonText.outlineWidth = outlineWidth;
                currentButtonText.outlineColor = outlineColor;
            }
        }
    }
}
