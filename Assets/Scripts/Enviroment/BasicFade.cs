using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicFade : MonoBehaviour
{
    public UnityEvent atEndEvent;

    CutsceneController controller;

    public float fadeStartTime;
    public float fadeFrameTime;

    private void Start()
    {
        controller = FindObjectOfType<CutsceneController>();

        if (controller == null) {
            Debug.LogWarning("No CutsceneController in Scene!");
        }


    }

    public void triggerFade() {
        if (controller != null)
            controller.fadeScreen(false, fadeStartTime, fadeFrameTime, atEndEvent);
    }

}
