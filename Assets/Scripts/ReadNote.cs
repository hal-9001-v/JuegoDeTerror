using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadNote : MonoBehaviour
{
    public Transform Player;
    public float minDist;
    public bool isReading;
    public AudioClip noteOpenSound;
    public AudioClip noteCloseSound;
    public Canvas noteCanvas;
    public TextMeshProUGUI text;
    public Image nextPageArrow;
    public Image lastPageArrow;

    private string contentFirstPage;
    private string contentSecondPage;
    private bool moreThanOnePage;
    private AudioSource audioSource;
    private float dist = 5.0f;

    private void Start()
    {
        isReading = false;
        audioSource = GetComponent<AudioSource>();
        moreThanOnePage = GetComponent<Collectable>().moreThanOnePage;
    }

    private void Update()
    {
        dist = Vector3.Distance(Player.position, this.transform.position);

        if (dist <= minDist)
        {
            //Entrar al modo Nota
            if (Input.GetButtonDown("ReadNote") && isReading == false)
            {
                isReading = true;
                audioSource.PlayOneShot(noteOpenSound);
                contentFirstPage = GetComponent<Collectable>().firstPageText;

                if(moreThanOnePage)
                {
                    contentSecondPage = GetComponent<Collectable>().secondPageText;
                    nextPageArrow.enabled = true;
                }

                Page(contentFirstPage);

                noteCanvas.enabled = true;

                Debug.Log("Estás leyendo una nota");
            }

            //Si hay segunda página...
            if(Input.GetKeyDown(KeyCode.RightArrow) && isReading && moreThanOnePage)
            {
                Page(contentSecondPage);
                lastPageArrow.enabled = true;
                nextPageArrow.enabled = false;
            }

            //Si hay página anterior...
            if (Input.GetKeyDown(KeyCode.LeftArrow) && isReading && moreThanOnePage)
            {
                Page(contentFirstPage);
                lastPageArrow.enabled = false;
                nextPageArrow.enabled = true;
            }

            //Salir del modo Nota
            if (Input.GetButtonDown("Exit") && isReading)
            {
                isReading = false;
                audioSource.PlayOneShot(noteCloseSound);
                noteCanvas.enabled = false;

                Debug.Log("Has dejado de leer");
            }
        }
    }

    public void Page(string page)
    {
        text.text = page;
    }
}
