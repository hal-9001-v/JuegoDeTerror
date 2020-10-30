using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoadScene : MonoBehaviour
{
    GameManager gameManager;
    public AudioSource audioSource;
    public AudioClip preLoadSound;

    void Start()
    {
        StartCoroutine(PreLoad());
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    IEnumerator PreLoad()
    {
        audioSource.PlayOneShot(preLoadSound);
        yield return new WaitForSeconds(7);
        gameManager.pauseGame();
        SceneManager.LoadScene("MainMneu");

        yield return null;
    }
}
