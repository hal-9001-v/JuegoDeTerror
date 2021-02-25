using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBox : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    public CanvasGroup group;

    private void Awake()
    {
        hide();
    }

    public void show()
    {
        if (group)
        {
            group.alpha = 1;
        }

    }

    public void hide()
    {
        if (group)
        {
            group.alpha = 0;
        }

    }

}
