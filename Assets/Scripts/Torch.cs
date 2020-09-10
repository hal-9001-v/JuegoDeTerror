using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject torchAsset;
    public Transform player;
    public float minDist = 5.0f;
    public AudioClip torchOnOffSound;
    private Light torch;
    private AudioSource audioSource;
    private float dist;
    private bool hasPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        torch = GetComponent<Light>();
        torch.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (hasPlayer)
            {
                if (Input.GetButtonDown("Torch"))
                {
                    torch.enabled = !torch.enabled;
                    audioSource.PlayOneShot(torchOnOffSound);
                }
            }
            else
            {
                dist = Vector3.Distance(player.position, torchAsset.transform.position);
                if (dist <= minDist)
                {
                    if (Input.GetButtonDown("Interact"))
                    {
                        hasPlayer = true;
                        torchAsset.GetComponent<HighlightedObject>().SetOpenObject(true);
                        torchAsset.SetActive(false);
                    }
                }
            }
        }
    }
}
