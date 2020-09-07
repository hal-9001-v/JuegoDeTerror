using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightedObject : MonoBehaviour
{
    public Transform playerPosition;
    public float minDist = 3.0f;
    public Light light;

    public static bool openObject = false;
    private float dist;
    private bool alreadyHighlighting = false;

    // Start is called before the first frame update
    void Start()
    {
        light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (openObject == false)
            {
                dist = Vector3.Distance(playerPosition.position, this.transform.position);

                if (dist <= minDist && alreadyHighlighting == false)
                {
                    alreadyHighlighting = true;
                    light.enabled = true;
                    StartCoroutine(Highlighting());
                }

                if (dist > minDist && alreadyHighlighting)
                {
                    alreadyHighlighting = false;
                    light.enabled = false;
                    StopCoroutine(Highlighting());
                }
            }
            else
            {
                light.enabled = false;
                Destroy(this);
            }
        }
    }

    public IEnumerator Highlighting()
    {
        bool direction = true;

        light.intensity = 1.0f;

        do
        {
            if(light.intensity > 0.0f && light.intensity < 100.0f)
            {
                if (direction)
                {
                    light.intensity += 1.0f;
                }
                else
                {
                    light.intensity -= 1.0f;
                }
            }
            else
            {
                if (direction)
                {
                    light.intensity -= 1.0f;
                }
                else
                {
                    light.intensity += 1.0f;
                    yield return new WaitForSeconds(1.0f);
                }

                direction = !direction;
            }
            yield return new WaitForSeconds(0.02f);
        } while (dist <= minDist);
    }

    public void SetOpenObject(bool set)
    {
        openObject = set;
    }
}
