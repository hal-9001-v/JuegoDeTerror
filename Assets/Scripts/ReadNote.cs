using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ReadNote : MonoBehaviour
{
    public Transform Player;
    public float minDist;//Distancia mínima para que pueda interactuar con la nota
    public AudioClip noteOpenSound;
    public AudioClip noteCloseSound;
    public Canvas noteCanvas;//Cnvas de la nota
    public TextMeshProUGUI text;//Texto intercambiable de la nota
    public Scrollbar bar;
    public string textKey;
    [TextArea(0, 40)] public List<string> contentPages = new List<string>(); //Lista de todos los textos. Cada posicion es una hoja

    private int currentPage = 0;//Página actual
    private AudioSource audioSource;
    private float dist;//Distancia actual entre Player y Nota

    private void Start()
    {
        PlayerMovement.sharedInstance.isReading = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        dist = Vector3.Distance(Player.position, this.transform.position);

        if (dist <= minDist)
        {
            //Entrar al modo Nota
            if (Input.GetButtonDown("Interact") && PlayerMovement.sharedInstance.isReading == false)
            {
                currentPage = 0;
                PlayerMovement.sharedInstance.isReading = true;
                Debug.Log("EH");
                audioSource.PlayOneShot(noteOpenSound);
                Page(contentPages[currentPage]);
                noteCanvas.enabled = true;

            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                bar.value += 0.1f;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                bar.value -= 0.1f;
            }

            if (contentPages.Count != 0 && PlayerMovement.sharedInstance.isReading)
            {
                //Salir del modo Nota
                if (Input.GetButtonDown("Exit") && PlayerMovement.sharedInstance.isReading)
                {
                    ExitPage();
                }
            }
        }
    }

    public void Page(string page)
    {
        text.text = page;
    }

    //Método para salir del estado "Leer Nota"
    public void ExitPage()
    {
        PlayerMovement.sharedInstance.isReading = false;
        audioSource.PlayOneShot(noteCloseSound);
        noteCanvas.enabled = false;
    }
}