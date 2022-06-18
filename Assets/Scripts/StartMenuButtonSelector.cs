using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

namespace THFUMO
{
    public class StartMenuButtonSelector : MonoBehaviour
    {
        [SerializeField]
        private ButtonNavigatorBase buttonNavigator;

        private Controls controls;

        private AudioClip confirmationSoundEffect;

        private AudioClip cancellationSoundEffect;

        [SerializeField]
        private AudioManagerBase audioManager;

        [SerializeField]
        private AssetHolder<AssetKey, AudioClip> audioHolder;

        [SerializeField]
        private GameObject optionsMenu;

        [SerializeField]
        private RectTransform optionsMenuRect;

        [SerializeField]
        private GameObject startMenu;

        [SerializeField]
        private CanvasGroup startMenuCanvasGroup;

        [SerializeField]
        private CanvasGroup optionsMenuCanvasGroup;

        [SerializeField]
        private float optionsMenuPopInSpeed;

        [SerializeField]
        private float startMenuFadeOutSpeed;

        [SerializeField]
        private MonoBehaviour coroutineRunner;

        private void Start()
        {
            controls = new();
            controls.Enable();
            controls.UI.Confirm.performed += Confirm_performed;
            controls.UI.Cancel.performed += Cancel_performed;
            confirmationSoundEffect = audioHolder.GetAsset(AssetKey.UIConfirmationSoundEffect);
            cancellationSoundEffect = audioHolder.GetAsset(AssetKey.UICancelSoundEffect);
        }

        private void Confirm_performed(InputAction.CallbackContext context)
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            switch (buttonNavigator.CurrentButton.Id)
            {
                case ButtonId.MainMenuOptions:
                    audioManager.PlaySoundEffect(confirmationSoundEffect);
                    optionsMenu.SetActive(true);
                    Vector3 originalPos = optionsMenuRect.position;
                    optionsMenuRect.position = new(optionsMenuRect.position.x - optionsMenuRect.rect.width, optionsMenuRect.position.y, optionsMenuRect.position.z);
                    coroutineRunner.StartCoroutine(Utilities.TranslateSmoothly(value => startMenuCanvasGroup.alpha = value, 1, 0, startMenuFadeOutSpeed));
                    coroutineRunner.StartCoroutine(optionsMenuRect.TranslateSmoothly(optionsMenuRect.position, originalPos, optionsMenuPopInSpeed));
                    optionsMenuCanvasGroup.alpha = 1;
                    startMenu.SetActive(false);
                    break;
                case ButtonId.MainMenuExit:
                    audioManager.PlaySoundEffect(cancellationSoundEffect);
                    Application.Quit();
                    break;
            }
        }

        private void Cancel_performed(InputAction.CallbackContext context)
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            audioManager.PlaySoundEffect(cancellationSoundEffect);
            if (buttonNavigator.CurrentButton.Id is ButtonId.MainMenuExit && context.control.path is "/Keyboard/escape")
            {
                Application.Quit();
            }
            buttonNavigator.CurrentButton = buttonNavigator.Buttons.First(button => button.Id == ButtonId.MainMenuExit);
        }
    }
}