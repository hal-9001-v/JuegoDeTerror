using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


[RequireComponent(typeof(AudioSource))]
public class DigitalDisplay : Interactable
{
    public string unlockCode;
    public TextMeshProUGUI text;

    public Transform player;
    public Canvas displayCnavas;
    public AudioClip clickSound;


    private AudioSource audioSource;
    private bool displayActivated = false;
    private string sequence = "";
    private bool unlocked = false;

    public UnityEvent doneActions;

    public ParticleSystem particleSystem;

    GameManager gameManager;

    private void Start()
    {
        displayCnavas.enabled = false;
        audioSource = GetComponent<AudioSource>();


    }


    public void AddDigitToSequence(int number)
    {
        if (sequence.Length <= unlockCode.Length)
        {
            sequence += number;
            text.text = sequence;
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void DeleteSequence()
    {
        sequence = "";
        text.text = sequence;
    }

    public void CheckCode()
    {
        if (unlockCode == sequence)
        {
            doneActions.Invoke();
            Debug.Log("Código correcto");
            readyForInteraction = false;
            Exit();
        }
        else
        {
            sequence = "";
            text.text = "";
            Debug.Log("Code is :" + unlockCode + ". Inserted is: " + sequence);
        }
    }

    public void Exit()
    {
        displayActivated = false;
        displayCnavas.enabled = false;
        PlayerMovement.sharedInstance.isReading = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        sequence = "";
        text.text = sequence;

        if (unlocked == true)
        {
            this.GetComponent<DigitalDisplay>().enabled = false;
        }
    }

    private void Update()
    {
        if (particleSystem != null)
        {
            if (hideWhenDone && done || !readyForInteraction)
            {
                if (particleSystem.isPlaying)
                {
                    particleSystem.Stop();
                    particleSystem.Clear();
                }
            }
            else
            {
                if (!particleSystem.isPlaying)
                {
                    particleSystem.Play();

                }
            }
        }
    }

    public override void interact()
    {

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {


            if (displayActivated == false)
            {

                GameManager.sharedInstance.digitalCode();
                displayActivated = true;
                displayCnavas.enabled = true;


                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

        }
    }
}
