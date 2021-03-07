using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingScreenAnimation : MonoBehaviour
{

    public TextMeshProUGUI text;
    public float changeTime = 0.2f;

    private void Start()
    {
        if (text != null)
            StartCoroutine(animateLoading());
    }

    IEnumerator animateLoading()
    {

        while (true)
        {
            text.text = "Loading";
            yield return new WaitForSeconds(changeTime);
            text.text = "Loading.";
            yield return new WaitForSeconds(changeTime);
            text.text = "Loading..";
            yield return new WaitForSeconds(changeTime);
            text.text = "Loading...";
            yield return new WaitForSeconds(changeTime);

        }



    }

}
