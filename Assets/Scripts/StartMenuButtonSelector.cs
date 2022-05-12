using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        private GameObject background;
        
        [SerializeField]
        private Image backgroundImage;

        [SerializeField]
        private AssetHolder<AssetKey, Sprite> backgroundImageHolder;

        [SerializeField]
        private GameObject logo;

        private RectTransform optionsMenuRect;

        [SerializeField]
        private float optionsMenuPopInSpeed;

        [SerializeField]
        private MonoBehaviour coroutineRunner;

        private void Start()
        {
            controls = new();
            controls.Enable();
            controls.UI.Confirm.performed += Confirm_performed;
            controls.UI.Cancel.performed += Cancel_performed;
            optionsMenuRect = optionsMenu.GetComponent<RectTransform>();
            confirmationSoundEffect = audioHolder.GetAsset(AssetKey.UIConfirmationSoundEffect);
            cancellationSoundEffect = audioHolder.GetAsset(AssetKey.UICancelSoundEffect);
        }

        private void Confirm_performed(InputAction.CallbackContext context)
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            audioManager.PlaySoundEffect(confirmationSoundEffect);
            if (buttonNavigator.CurrentButton is 1)
            {
                optionsMenu.SetActive(true);
                Vector3 originalPos = optionsMenuRect.position;
                optionsMenuRect.position = new(optionsMenuRect.position.x - optionsMenuRect.rect.width, optionsMenuRect.position.y, optionsMenuRect.position.z);
                coroutineRunner.StartCoroutine(optionsMenuRect.TranslateSmoothly(optionsMenuRect.position, originalPos, optionsMenuPopInSpeed));
                backgroundImage.sprite = backgroundImageHolder.GetAsset(AssetKey.YoumuBackground);
                logo.SetActive(false);
                gameObject.SetActive(false);
            }
        }

        private void Cancel_performed(InputAction.CallbackContext context)
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            audioManager.PlaySoundEffect(cancellationSoundEffect);
            buttonNavigator.CurrentButton = buttonNavigator.ButtonCount - 1;
        }
    }
}