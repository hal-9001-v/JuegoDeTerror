using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigitalDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float minDist;
    public float dist;
    public Transform player;
    public Canvas displayCnavas;
    public AudioClip clickSound;
    private AudioSource audioSource;
    private bool displayActivated = false;
    private string secuence = "";

    private void Start()
    {
        displayCnavas.enabled = false;
        audioSource = this.GetComponent<AudioSource>();
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
        secuence += number;
        text.text = secuence;
        audioSource.PlayOneShot(clickSound);
    }

    public void DeleteSecuence()
    {
        secuence = "";
        text.text = secuence;
    }
}
