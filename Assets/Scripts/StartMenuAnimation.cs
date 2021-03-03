using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuAnimation : MonoBehaviour
{
    public Light[] myLights;

    [Header("Colors")]
    public Color safeColor;
    public Color vaultColor;
    public Color dangerColor;

    [Space(5)]
    [Range(1, 60)]
    public float changeTime;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(colorChanger());
    }

    IEnumerator colorChanger()
    {
        
        foreach (Light l in myLights)
        {
            l.intensity = 0;

        }

        for (int i = 0; i < 1000; i++)
        {

            foreach (Light l in myLights)
            {
                l.intensity += 0.001f;

            }



            yield return new WaitForSeconds(0.01f);

        }

        while (true)
        {

            switch (Random.Range(0, 3))
            {
                case 0:
                    foreach (Light l in myLights)
                    {
                        l.color = safeColor;
                    }

                    break;

                case 1:
                    foreach (Light l in myLights)
                    {
                        l.color = vaultColor;
                    }

                    break;

                case 2:
                    foreach (Light l in myLights)
                    {
                        l.color = dangerColor;
                    }

                    break;

                default:
                    break;
            }

            yield return new WaitForSeconds(changeTime);
        }

    }

}
