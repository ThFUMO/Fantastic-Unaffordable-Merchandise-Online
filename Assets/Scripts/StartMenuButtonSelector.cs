using UnityEngine;
using UnityEngine.InputSystem;

namespace THFUMO
{
    public class StartMenuButtonSelector : MonoBehaviour
    {
        [SerializeField]
        private ButtonNavigatorBase buttonNavigator;

        private Controls controls;

        [SerializeField]
        private AudioClip confirmationSoundEffect;

        [SerializeField]
        private AudioClip cancellationSoundEffect;

        [SerializeField]
        private AudioManagerBase audioManager;

        private void Start()
        {
            controls = new();
            controls.Enable();
            controls.UI.Confirm.performed += Confirm_performed;
            controls.UI.Cancel.performed += Cancel_performed;
        }

        private void Confirm_performed(InputAction.CallbackContext obj)
        {
            audioManager.PlaySoundEffect(confirmationSoundEffect);
        }

        private void Cancel_performed(InputAction.CallbackContext obj)
        {
            audioManager.PlaySoundEffect(cancellationSoundEffect);
            buttonNavigator.CurrentButton = buttonNavigator.ButtonCount - 1;
        }
    }
}