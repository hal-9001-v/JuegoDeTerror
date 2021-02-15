using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{

    AudioSource source;

    [Range(0, 1)]
    public float volume;
    [Range(0, 1)]
    public float runningVolume;

    [Range(0, 1)]
    public float volumeVariation;

    [Space(5)]

    [Range(0, 5)]
    public float pitch;
    [Range(0, 5)]
    public float runningPitch;
    [Range(0, 3)]
    public float pitchVariation;

    // Start is called before the first frame update
    private void Awake()
    {
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    public void playWalking()
    {
        if (!source.isPlaying)
        {
            source.volume = Random.Range(volume - volumeVariation, volume + volumeVariation);
            source.pitch = Random.Range(pitch - pitchVariation, pitch + pitchVariation);


            source.Play();

        }

    }

    public void playRunning() {
        
        if (!source.isPlaying)
        {
            source.volume = Random.Range(runningVolume - volumeVariation, runningVolume + volumeVariation);
            source.pitch = Random.Range(runningPitch - pitchVariation, runningPitch + pitchVariation);


            source.Play();

        }
    }
}
