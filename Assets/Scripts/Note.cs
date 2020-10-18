using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Note : Interactable
{

    public static ReadingSystem myReadingSystem;

    public string textKey;                                                    //Letras en común de las claves de los párrafos de la Hashtable
    public List<string> paragraphs = new List<string>();                      //Lista de todos los párrafos


    private AudioClip noteOpenSound;                                           //Sonido al abrir la nota
    private AudioClip noteCloseSound;                                          //Sonido al cerrar la nota
    private Canvas noteCanvas;                                                 //Canvas de la nota
    private TextMeshProUGUI text;                                              //Texto intercambiable de la nota
    private Scrollbar scrollbar;                                               //Barra de scroll de la derecha
    private AudioSource audioSource;

    private int counter = 1;
    private string provisional;
    private string result = "";                                               //Resultado de todos los párrafos de la nota
    private bool highlighted = true;                                          //Booleano que guarda si el objeto está remarcado

    private void Awake()
    {
        myReadingSystem = FindObjectOfType<ReadingSystem>();

        noteOpenSound = myReadingSystem.noteOpenSound;
        noteCloseSound = myReadingSystem.noteCloseSound;
        noteCanvas = myReadingSystem.noteCanvas;
        text = myReadingSystem.text;
        scrollbar = myReadingSystem.scrollbar;

        audioSource = myReadingSystem.audioSource;

    }

    private void Start()
    {
        PlayerMovement.sharedInstance.isReading = false;

        provisional = textKey + "P" + counter;

        while (LanguageController.GetTextInLanguage(provisional) != provisional)
        {
            paragraphs.Add(LanguageController.GetTextInLanguage(provisional));
            counter++;
            provisional = textKey + "P" + counter;
        }


        for (int i = 0; i < paragraphs.Count; i++)
        {
            result += paragraphs[i] + "\n\n";
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


    public override void interact()
    {
        //Entrar al modo Nota
        if (PlayerMovement.sharedInstance.isReading == false)
        {
            scrollbar.value = 1;
            PlayerMovement.sharedInstance.isReading = true;
            audioSource.PlayOneShot(noteOpenSound);
            Page(result);
            noteCanvas.enabled = true;

            if (highlighted == true)
            {
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