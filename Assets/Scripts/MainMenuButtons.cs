using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THFUMO
{
    public class MainMenuButtons : MonoBehaviour
    {
        private List<TextMeshProUGUI> childTexts = new();

        private int currentButton = 0;

        private bool hasPressedArrowKey = false;

        [SerializeField]
        private AudioClip select;

        public MonoBehaviour AudioManager;

        private IAudioManager audioManager;

        private void OnValidate()
        {
            if (AudioManager is not IAudioManager)
            {
                Debug.LogError($"The {nameof(AudioManager)} field on {nameof(MainMenuButtons)} attatched to {gameObject.name} is not an instance of {nameof(IAudioManager)}");
            }
        }

        private void Start()
        {
            foreach (Transform child in transform)
            {
                childTexts.Add(child.GetComponent<TextMeshProUGUI>());
            }
            if (childTexts.Count != 0)
            {
                childTexts[0].outlineColor = Color.blue;
                childTexts[0].outlineWidth = 0.1f;
                childTexts[0].gameObject.Reactivate();
            }
            if (AudioManager is IAudioManager manager)
            {
                audioManager = manager;
            }
            else
            {
                Debug.LogError($"The {nameof(AudioManager)} field on {nameof(MainMenuButtons)} attatched to {gameObject.name} is not an instance of {nameof(IAudioManager)}");
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentButton--;
                hasPressedArrowKey = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentButton++;
                hasPressedArrowKey = true;
            }
            if (hasPressedArrowKey && childTexts.Count != 0)
            {
                audioManager.PlaySoundEffect(select);
                currentButton = Utilities.Repeat(currentButton, 0, childTexts.Count - 1);
                if (childTexts[currentButton] == null)
                {
                    Debug.LogWarning($"{childTexts[currentButton].gameObject.name} has no {nameof(TextMeshProUGUI)} attached.");
                }
                else
                {
                    childTexts[currentButton].outlineColor = Color.blue;
                    foreach (TextMeshProUGUI text in childTexts)
                    {
                        text.outlineWidth = 0;
                    }
                    childTexts[currentButton].outlineWidth = 0.1f;
                    foreach (TextMeshProUGUI text in childTexts)
                    {
                        text.gameObject.Reactivate();
                    }
                }
                hasPressedArrowKey = false;
            }
        }
    }
}

