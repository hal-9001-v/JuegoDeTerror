using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Profiles", menuName = "ScriptableObjects/SpawnSoundProfile", order = 1)]
public class SoundProfile : ScriptableObject
{

    //Sounds for pursuer when closeby
    public AudioClip[] roars;

    //Steps for pursuer when closeby
    public AudioClip[] steps;


    //Low, and static noises
    public AudioClip[] ambients;

    //Noises are aggressive and dangerous sounds
    public AudioClip[] noises;
}