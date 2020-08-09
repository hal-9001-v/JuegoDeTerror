using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNote : MonoBehaviour
{
    public Transform Player;
    public float minDist;
    public bool isReading;
    public AudioClip noteSound;

    private AudioSource audioSource;
    private float dist = 5.0f;

    private void Start()
    {
        isReading = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        dist = Vector3.Distance(Player.position, this.transform.position);

        if(dist <= minDist)
        {
            if(Input.GetButtonDown("ReadNote") && isReading == false)
            {
                isReading = true;
                audioSource.PlayOneShot(noteSound);
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
