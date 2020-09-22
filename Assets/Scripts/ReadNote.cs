using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ReadNote : MonoBehaviour
{
    public Transform Player;                                                  //Posioción del jugador
    public float minDist;                                                     //Distancia mínima para que pueda interactuar con la nota
    public AudioClip noteOpenSound;                                           //Sonido al abrir la nota
    public AudioClip noteCloseSound;                                          //Sonido al cerrar la nota
    public Canvas noteCanvas;                                                 //Canvas de la nota
    public TextMeshProUGUI text;                                              //Texto intercambiable de la nota
    public Scrollbar scrollbar;                                               //Barra de scroll de la derecha
    public string textKey;                                                    //Letras en común de las claves de los párrafos de la Hashtable
    public List<string> paragraphs = new List<string>();                      //Lista de todos los párrafos

    private int counter = 1;
    private string provisional;
    private string result = "";                                               //Resultado de todos los párrafos de la nota
    private AudioSource audioSource;
    private float dist;                                                       //Distancia actual entre Player y Nota
    private bool highlighted = true;                                          //Booleano que guarda si el objeto está remarcado

    private void Start()
    {
        PlayerMovement.sharedInstance.isReading = false;
        audioSource = GetComponent<AudioSource>();

        provisional = textKey + "P" + counter;

        while (LanguageController.GetTextInLanguage(provisional) != provisional)
        {
            paragraphs.Add(LanguageController.GetTextInLanguage(provisional));
            counter++;
            provisional = textKey + "P" + counter;
        }

        for(int i = 0; i < paragraphs.Count; i++)
        {
            result += paragraphs[i] + "\n\n";
        }
    }

    private void Update()
    {
        dist = Vector3.Distance(Player.position, this.transform.position);

        if (dist <= minDist)
        {
            
            //Entrar al modo Nota
            if (Input.GetButtonDown("Interact") && PlayerMovement.sharedInstance.isReading == false)
            {
                scrollbar.value = 1;
                PlayerMovement.sharedInstance.isReading = true;
                audioSource.PlayOneShot(noteOpenSound);
                Page(result);
                noteCanvas.enabled = true;

                if (highlighted == true)
                {
                    this.GetComponent<HighlightedObject>().SetOpenObject(true);
                    highlighted = false;
                }

                Inventory.sharedInstance.AddItem(GetComponent<Item>());
            }

            //Subir texto de la nota
            if (Input.GetKeyDown(KeyCode.DownArrow) && PlayerMovement.sharedInstance.isReading)
            {
                scrollbar.value -= 0.1f;
            }

            //Bajar texto de la nota
            if (Input.GetKeyDown(KeyCode.UpArrow) && PlayerMovement.sharedInstance.isReading)
            {
                scrollbar.value += 0.1f;
            }

            if (PlayerMovement.sharedInstance.isReading)
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

    public void DoAfterHighlighted()
    {
        this.GetComponent<HighlightedObject>().enabled = false;
    }
}