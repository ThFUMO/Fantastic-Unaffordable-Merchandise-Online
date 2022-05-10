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

        [SerializeField]
        private AudioManagerBase audioManager;

        [SerializeField]
        private Color outlineColor;

        [SerializeField]
        private float outlineWidth;

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
        }

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
                    childTexts[currentButton].outlineColor = outlineColor;
                    foreach (TextMeshProUGUI text in childTexts)
                    {
                        text.outlineWidth = 0;
                    }
                    childTexts[currentButton].outlineWidth = outlineWidth;
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

