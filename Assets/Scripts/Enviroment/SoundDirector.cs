using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SoundManager))]
public class SoundDirector : MonoBehaviour
{
    SoundManager manager;

    public SoundProfile labProfile;
    public SoundProfile corridorProfile;
    public SoundProfile storageProfile;
    public SoundProfile officeProfile;

    [Header("Pursuer")]
    public int roarDistance = 1;

    [Range(1, 10)]
    public int stepDistance = 1;

    [Range(1, 10)]
    public int noiseDistance = 1;

    [Range(1, 50)]
    public float updateTime = 5;
    float auxTime;

    Pursuer myPursuer;

    PlayerTracker tracker;


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

        if (tracker == null)
            tracker = FindObjectOfType<PlayerTracker>();

        StartCoroutine(PursuerUpdater());

    }

    SoundProfile getProfile()
    {

        SoundProfile profile;

        switch (tracker.currentRoom.profile)
        {
            case Room.SoundProfile.Corridor:
                profile = corridorProfile;
                break;

            case Room.SoundProfile.Laboratory:
                profile = labProfile;
                break;

            case Room.SoundProfile.Office:
                profile = officeProfile;
                break;

            case Room.SoundProfile.Storage:
                profile = storageProfile;
                break;

            default:
                return null;

        }

        return profile;
    }

    public void playRoar(int distance)
    {
        SoundProfile profile = getProfile();


        if (profile == null)
            return;

        AudioClip clip = profile.roars[Random.Range(0, profile.roars.Length)];

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
        SoundProfile profile = getProfile();


        if (profile == null)
            return;

        AudioClip clip = profile.steps[Random.Range(0, profile.steps.Length)];

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
        SoundProfile profile = getProfile();


        if (profile == null)
            return;

        AudioClip clip = profile.noises[Random.Range(0, profile.noises.Length)];

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

    public void playAmbients(int distance)
    {
        SoundProfile profile = getProfile();


        if (profile == null)
            return;

        AudioClip clip = profile.ambients[Random.Range(0, profile.ambients.Length)];

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

                if (myPursuer.isKilling)
                {
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

    AudioClip[] getClips(SoundProfile profile, TypeOfSound type)
    {

        if (profile == null)
            return null;

        AudioClip[] clips = null;


        switch (type)
        {
            case TypeOfSound.Noise:
                if (profile.noises != null)
                {
                    clips = profile.noises;
                }

                break;

            case TypeOfSound.Roar:
                if (profile.roars != null)
                {
                    clips = profile.noises;
                }

                break;

            case TypeOfSound.Steps:

                if (profile.steps != null)
                {
                    clips = profile.steps;
                }

                break;

            case TypeOfSound.Ambient:

                if (profile.ambients != null)
                {
                    clips = profile.ambients;
                }

                break;



        }



        return clips;

    }

}
