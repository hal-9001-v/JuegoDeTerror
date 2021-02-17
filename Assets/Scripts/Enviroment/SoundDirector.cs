using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SoundManager))]
public class SoundDirector : MonoBehaviour
{
    SoundManager manager;

    [Header("Pursuer")]
    public AudioClip[] roars;
    [Range(1,10)]
    public int roarDistance = 1;

    [Space(2)]
    public AudioClip[] steps;
    [Range(1, 10)]
    public int stepDistance = 1;

    [Space(2)]
    public AudioClip[] noises;
    [Range(1, 10)]
    public int noiseDistance = 1;

    [Space(10)]
    [Range(1, 50)]
    public float updateTime = 5;
    float auxTime;

    Pursuer myPursuer;


    [Space(5)]
    [Header("Player")]
    public AudioClip heartBeats;
    public AudioSource playerSource;


    private void Start()
    {
        manager = GetComponent<SoundManager>();
        if (myPursuer == null)
        {
            myPursuer = FindObjectOfType<Pursuer>();
        }

        StartCoroutine(PursuerUpdater());

    }

    public void playRoar(int distance)
    {
        AudioClip clip = roars[Random.Range(0, roars.Length)];
        Debug.Log(clip.name);

        if (clip != null)
        {
            int number = Random.Range(0, 10);

            if (number < 4)
                manager.playSoundInDirection(SoundManager.PositionToPlayer.Behind, distance, clip);
            else if (number >= 4 && number < 6)
                manager.playSoundInDirection(SoundManager.PositionToPlayer.Forward, distance, clip);
            else if (number >= 6 && number < 8)
                manager.playSoundInDirection(SoundManager.PositionToPlayer.Right, distance, clip);
            else if (number >= 8 && number < 9)
                manager.playSoundInDirection(SoundManager.PositionToPlayer.Left, distance, clip);
        }
    }

    public void playSteps(int distance)
    {
        AudioClip clip = steps[Random.Range(0, steps.Length)];

        if (clip != null)
        {
            int number = Random.Range(0, 10);

            if (number < 4)
                manager.playSoundInDirection(SoundManager.PositionToPlayer.Behind, distance, clip);
            else if (number >= 4 && number < 6)
                manager.playSoundInDirection(SoundManager.PositionToPlayer.Forward, distance, clip);
            else if (number >= 6 && number < 8)
                manager.playSoundInDirection(SoundManager.PositionToPlayer.Right, distance, clip);
            else if (number >= 8 && number < 9)
                manager.playSoundInDirection(SoundManager.PositionToPlayer.Left, distance, clip);
        }
    }

    public void playNoises(int distance)
    {
        AudioClip clip = noises[Random.Range(0, noises.Length)];

        if (clip != null)
        {
            int number = Random.Range(0, 10);

            if (number < 4)
                manager.playSoundInDirection(SoundManager.PositionToPlayer.Behind, distance, clip);
            else if (number >= 4 && number < 6)
                manager.playSoundInDirection(SoundManager.PositionToPlayer.Forward, distance, clip);
            else if (number >= 6 && number < 8)
                manager.playSoundInDirection(SoundManager.PositionToPlayer.Right, distance, clip);
            else if (number >= 8 && number < 9)
                manager.playSoundInDirection(SoundManager.PositionToPlayer.Left, distance, clip);
        }
    }

    public void playHeartbeats()
    {
        if (playerSource != null)
        {
            playerSource.clip = heartBeats;
            playerSource.Play();
        }
    }

    public void updatePursuer()
    {
        switch (myPursuer.currentState)
        {
            case (int)Pursuer.pursuerStates.Inactive:
                playNoises(noiseDistance);
                break;

            case (int)Pursuer.pursuerStates.Patrol:
                playNoises(noiseDistance);
                break;

            case (int)Pursuer.pursuerStates.Pursue:

                if (myPursuer.isKilling) {
                    playRoar(roarDistance);
                    playHeartbeats();
                }
                
                else
                    playSteps(stepDistance);
                break;

        }
    }

    IEnumerator PursuerUpdater()
    {

        while (true)
        {
            auxTime = updateTime;

            updatePursuer();
            yield return new WaitForSeconds(auxTime);
        }


    }



}
