using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNote : MonoBehaviour
{
    public Transform Player;
    public float minDist;
    public bool isReading;
    private float dist = 5.0f;

    private void Start()
    {
        isReading = false;
    }

    private void Update()
    {
        dist = Vector3.Distance(Player.position, this.transform.position);

        if(dist <= minDist)
        {
            if(Input.GetButtonDown("ReadNote") && isReading == false)
            {
                isReading = true;
                Debug.Log("Estás leyendo una nota");
            }
            
            if(Input.GetButtonDown("Exit") && isReading)
            {
                isReading = false;
                Debug.Log("Has dejado de leer");
            }
        }
    }
}
