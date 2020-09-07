using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightedObject : MonoBehaviour
{
    public bool isHighlighted = false;
    public bool openObject = false;
    private Light light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if(isHighlighted == false && openObject == false)
            {
                //StartCoroutine(Highlighting());
                isHighlighted = true;
                light.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                openObject = true;
            }

            if(openObject)
            {
                //StopCoroutine(Highlighting());
                isHighlighted = false;
                light.enabled = false;
            }
        }
    }

    /*public IEnumerator Highlighting()
    {

    }*/
}
