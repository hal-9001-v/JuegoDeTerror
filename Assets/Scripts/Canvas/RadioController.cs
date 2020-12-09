using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadioController : MonoBehaviour
{
    public RawImage image;
    public TextMeshProUGUI textMesh;

    private void Awake()
    {
        hide();
    }

    public void show()
    {
        if (image != null && textMesh != null)
        {
            image.enabled = true;
            textMesh.enabled = true;
        }

    }

    public void hide()
    {
        if (image != null && textMesh != null)
        {
            image.enabled = false;
            textMesh.enabled = false;
        }

    }

}
