﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadingSystem : MonoBehaviour
{
    static ReadingSystem instance;

    //Sonido al abrir la nota
    public AudioClip noteOpenSound;

    //Sonido al cerrar la nota
    public AudioClip noteCloseSound;

    public AudioSource audioSource;

    //Canvas de la nota
    public Canvas noteCanvas;

    //Texto intercambiable de la nota
    public TextMeshProUGUI text;

    //Barra de scroll de la derecha
    public Scrollbar scrollbar;


    private void Awake()
    {
        //Singleton
        if (instance != null)
            Debug.LogWarning("Reading System is a singleton. Kept: " + instance.gameObject.name + " Discarded: " + gameObject.name);
        Destroy(this);
    }

}