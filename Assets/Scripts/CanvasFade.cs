using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFade : MonoBehaviour
{
    public Image darkFade;
    public bool inOut; //true = FadeIn and false = FadeOut
    public float time = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartFade();
    }

    public void StartFade()
    {
        darkFade.enabled = true;

        if (inOut)
        {
            darkFade.canvasRenderer.SetAlpha(1.0f);
            FadeOut();
        }
        else
        {
            darkFade.canvasRenderer.SetAlpha(0.0f);
            FadeIn();
        }
        StartCoroutine(Enable());
    }

    public void FadeOut()
    {
        darkFade.CrossFadeAlpha(0.0f, time, false);
    }

    public void FadeIn()
    {
        darkFade.CrossFadeAlpha(1.0f, time, false);
    }

    public IEnumerator Enable()
    {
        yield return new WaitForSeconds(time);
        darkFade.enabled = false;
        yield return null;
    }
}
