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
    [Header("Roars")]
    [Range(1, 10)]
    public int roarDistance = 1;
    [Range(1, 20)]
    public float roarDelay = 1;

    [Space(3)]
    [Header("Steps")]
    [Range(1, 10)]
    public int stepDistance = 1;
    [Range(1, 20)]
    public float stepDelay = 1;

    [Space(3)]
    [Header("Noises")]
    [Range(1, 10)]
    public int noiseDistance = 1;
    [Range(1, 20)]
    public float noiseDelay = 1;

    [Space(3)]

    [Range(1, 30)]
    [Header("Ambient")]
    public int ambientDistance = 1;
    [Range(1, 20)]
    public float ambientDelay = 1;

    float noiseCounter = 0;
    float ambientCounter = 0;
    float roarCounter = 0;
    float stepCounter = 0;



    Pursuer myPursuer;

    PlayerTracker tracker;


    [Space(5)]
    [Header("Player")]
    public AudioClip slowHeartBeats;
    public AudioClip fastHeartBeats;
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

        StartCoroutine(soundUpdater());

    }

    public void restartSound() {
        manager.stopSounds();

        playerSource.Stop();
    }

    SoundProfile getProfile()
    {
        SoundProfile profile;

        if (tracker != null && tracker.currentRoom != null)
        {
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

        }
        else
        {
            profile = corridorProfile;
        }



        return profile;
    }

    public void playRoar(int distance)
    {
        if (roarCounter > roarDelay)
        {
            roarCounter = 0;
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

                Debug.Log("Roar");
            }
        }

    }

    public void playSteps(int distance)
    {
        if (stepCounter > stepDelay)
        {
            stepCounter = 0;
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

                Debug.Log("Step");
            }
        }

    }

    public void playNoises(int distance)
    {

        if (noiseCounter > noiseDelay)
        {
            noiseCounter = 0;
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

                Debug.Log("Noise");
            }
        }

    }

    public void playAmbients(int distance)
    {

        if (ambientCounter > ambientDelay)
        {
            ambientCounter = 0;
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
            else
            {
                Debug.Log("Ambient Null");
            }
        }

    }


    public void playFastHeartbeats()
    {
        if (playerSource != null && !playerSource.clip != fastHeartBeats)
        {
            playerSource.clip = fastHeartBeats;
            playerSource.Play();
        }
    }

    public void playSlowHeartbeats()
    {
        if (playerSource != null && playerSource.clip != slowHeartBeats)
        {
            playerSource.clip = slowHeartBeats;
            playerSource.Play();
        }
    }


    public void updatePursuer()
    {
        switch (myPursuer.currentState)
        {
            case (int)Pursuer.pursuerStates.Inactive:
                playAmbients(ambientDistance);
                break;

            case (int)Pursuer.pursuerStates.Patrol:
                playAmbients(ambientDistance);
                break;

            case (int)Pursuer.pursuerStates.Pursue:


                if (myPursuer.isKilling)
                {
                    playRoar(roarDistance);
                    playSteps(stepDistance);
                    playFastHeartbeats();
                }
                else
                {
                    playNoises(noiseDistance);
                    playAmbients(ambientDistance);
                    playSlowHeartbeats();
                }


                break;

        }
    }

    IEnumerator soundUpdater()
    {
        while (true)
        {

            //Update Times
            #region
            if (noiseCounter < noiseDelay)
            {
                noiseCounter += Time.deltaTime;
            }

            if (ambientCounter < ambientDelay)
            {
                ambientCounter += Time.deltaTime;
            }

            if (roarCounter < roarDelay)
            {
                roarCounter += Time.deltaTime;
            }

            if (stepCounter < stepDelay)
            {
                stepCounter += Time.deltaTime;
            }
            #endregion

            updatePursuer();
            yield return null;
        }


    }






}
