using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigitalDisplay : MonoBehaviour
{
    public int unlockCode = 0000;
    public int codeLength = 4;
    public TextMeshProUGUI text;
    public float minDist;
    public float dist;
    public Transform player;
    public Canvas displayCnavas;
    public AudioClip clickSound;
    public TextMeshProUGUI deleteText;
    public TextMeshProUGUI doneText;
    public TextMeshProUGUI exitText;
    private AudioSource audioSource;
    private bool displayActivated = false;
    private string secuence = "";
    private bool unlocked = false;
    
    private void Start()
    {
        displayCnavas.enabled = false;
        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (displayActivated == false)
            {
                dist = Vector3.Distance(player.position, this.transform.position);
                if (dist <= minDist)
                {
                    if (Input.GetButtonDown("Interact"))
                    {
                        displayActivated = true;
                        displayCnavas.enabled = true;
                        PlayerMovement.sharedInstance.isReading = true;
                        Cursor.lockState = CursorLockMode.None;
                    }
                }
            }
        }
    }

    public void AddDigitToSequence(int number)
    {
        if (secuence.Length < codeLength)
        {
            secuence += number;
            text.text = secuence;
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void DeleteSecuence()
    {
        secuence = "";
        text.text = secuence;
    }

    public void CheckCode()
    {
        if(unlockCode.ToString() == secuence)
        {
            unlocked = true;
            Debug.Log("Código correcto");
            Exit();
        }
    }

    public void Exit()
    {
        displayActivated = false;
        displayCnavas.enabled = false;
        PlayerMovement.sharedInstance.isReading = false;
        Cursor.lockState = CursorLockMode.Locked;
        secuence = "";
        text.text = secuence;

        if (unlocked == true)
        {
            this.GetComponent<DigitalDisplay>().enabled = false;
        }
    }
}
