using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in FindObjectsOfType<GameObject>())
        {

            if (go != gameObject)
                Destroy(go);
        }


        Debug.Log("Objects have been erased");


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(0);
    }
}
