using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadNote : MonoBehaviour
{
    public Transform Player;
    public float minDist;
    public bool isReading;
    public AudioClip noteOpenSound;
    public AudioClip noteCloseSound;

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

        if (dist <= minDist)
        {
            if (Input.GetButtonDown("ReadNote") && isReading == false)
            {
                isReading = true;
                audioSource.PlayOneShot(noteOpenSound);

                Debug.Log("Estás leyendo una nota");
            }

            if (Input.GetButtonDown("Exit") && isReading)
            {
                isReading = false;
                audioSource.PlayOneShot(noteCloseSound);
                Debug.Log("Has dejado de leer");
            }
        }
    }

    void OnGUI()
    {
        if (isReading)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "LEYENDO NOTA DE UN POLICÍA ALIEN");
        }
    }
}
