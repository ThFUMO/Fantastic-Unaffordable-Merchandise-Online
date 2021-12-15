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
        private bool hasPressedArrowKey = true;

        void Start()
        {
            foreach (Transform child in transform)
            {
                childTexts.Add(child.GetComponent<TextMeshProUGUI>());
            }
        }

        // Update is called once per frame
        void Update()
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
                        text.gameObject.Restart();
                    }
                }
                hasPressedArrowKey = false;
            }
        }
    }
}

