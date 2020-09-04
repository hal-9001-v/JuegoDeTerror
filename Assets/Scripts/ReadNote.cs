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
    public bool isReading;
    public AudioClip noteOpenSound;
    public AudioClip noteCloseSound;
    public Canvas noteCanvas;//Cnvas de la nota
    public TextMeshProUGUI text;//Texto intercambiable de la nota
    public Image nextPageArrow;
    public Image lastPageArrow;

    [TextArea(0, 40)] public List<string> contentPages = new List<string>(); //Lista de todos los textos. Cada posicion es una hoja

    private int currentPage = 0;//Página actual
    private AudioSource audioSource;
    private float dist;//Distancia actual entre Player y Nota

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
            //Entrar al modo Nota
            if (Input.GetButtonDown("Interact") && isReading == false)
            {
                currentPage = 0;
                isReading = true;
                audioSource.PlayOneShot(noteOpenSound);
                Page(contentPages[currentPage]);
                noteCanvas.enabled = true;

            }

            if (contentPages.Count != 0 && isReading)
            {
                ArrowManager();

                //Si hay siguiente página...
                if (Input.GetKeyDown(KeyCode.RightArrow) && isReading && nextPageArrow.enabled)
                {
                    NextPage();
                }

                //Si hay página anterior...
                if (Input.GetKeyDown(KeyCode.LeftArrow) && isReading && lastPageArrow.enabled)
                {
                    PreviousPage();
                }

                //Salir del modo Nota
                if (Input.GetButtonDown("Exit") && isReading)
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

    //Método encargado de pasar a la página siguiente (si la hubiera)
    public void NextPage()
    {
        currentPage++;
        Page(contentPages[currentPage]);
    }

    //Método encargado de pasar a la página anterior (si la hubiera)
    public void PreviousPage()
    {
        currentPage--;
        Page(contentPages[currentPage]);
    }

    //Método encargado de mostrar u ocultar las flechas de página siguiente y anterior
    public void ArrowManager()
    {
        if (contentPages.Count() > currentPage + 1)
        {
            nextPageArrow.enabled = true;
        }
        else
        {
            nextPageArrow.enabled = false;
        }

        if (currentPage - 1 >= 0)
        {
            lastPageArrow.enabled = true;
        }
        else
        {
            lastPageArrow.enabled = false;
        }
    }

    //Método para salir del estado "Leer Nota"
    public void ExitPage()
    {
        isReading = false;
        audioSource.PlayOneShot(noteCloseSound);
        noteCanvas.enabled = false;
    }
}