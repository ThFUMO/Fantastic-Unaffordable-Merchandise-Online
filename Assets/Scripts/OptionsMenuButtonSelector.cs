using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

namespace THFUMO
{
    public class OptionsMenuButtonSelector : MonoBehaviour
    {
        [SerializeField]
        private AudioManagerBase audioManager;

        [SerializeField]
        private AssetHolder<AssetKey, AudioClip> audioHolder;

        private Controls controls;

        private AudioClip confirmationSoundEffect;

        private AudioClip cancellationSoundEffect;

        [SerializeField]
        private GameObject startMenu;

        [SerializeField]
        private GameObject optionsMenu;

        [SerializeField]
        private RectTransform optionsMenuRect;

        [SerializeField]
        private float optionsMenuClosingSpeed;

        [SerializeField]
        private CanvasGroup optionsMenuCanvasGroup;

        [SerializeField]
        private float startMenuFadeInSpeed;

        [SerializeField]
        private CanvasGroup startMenuCanvasGroup;

        [SerializeField]
        private MonoBehaviour coroutineRunner;

        private void Start()
        {
            controls = new();
            controls.Enable();
            controls.UI.Cancel.performed += Cancel_performed;
            confirmationSoundEffect = audioHolder.GetAsset(AssetKey.UIConfirmationSoundEffect);
            cancellationSoundEffect = audioHolder.GetAsset(AssetKey.UICancelSoundEffect);
        }

        private void Cancel_performed(InputAction.CallbackContext context)
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            coroutineRunner.StartCoroutine(CloseOptionsMenu());
            coroutineRunner.StartCoroutine(FadeInStartMenu());
        }

        private IEnumerator CloseOptionsMenu()
        {
            audioManager.PlaySoundEffect(cancellationSoundEffect);
            Vector3 dest = new(optionsMenuRect.position.x - optionsMenuRect.rect.width, optionsMenuRect.position.y, optionsMenuRect.position.z);
            yield return optionsMenuRect.TranslateSmoothly(optionsMenuRect.position, dest, optionsMenuClosingSpeed);
            optionsMenuCanvasGroup.alpha = 0;
            optionsMenu.transform.position = startMenu.transform.position;
            optionsMenu.SetActive(false);
        }

        private IEnumerator FadeInStartMenu()
        {
            startMenu.SetActive(true);
            yield return Utilities.TranslateSmoothly(value => startMenuCanvasGroup.alpha = value, 0, 1, startMenuFadeInSpeed);
        }
    }
}