using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasButton : MonoBehaviour
{
    public Canvas canvas;
    public string buttonKey;

    private TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.enabled == true)
        {
            if (LanguageController.language == 0)
            {
                buttonText.text = LanguageController.GetTextInLanguage(buttonKey);
            }
            else if(LanguageController.language == 1)
            {
                buttonText.text = LanguageController.GetTextInLanguage(buttonKey);
            }
        }
    }
}
