using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public GameObject playerBody;

    float xRotation = 0.0f;

    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame && playerBody.GetComponent<PlayerMovement>().isReading == false)
        {
            CameraRotation();
        }
    }

    public void CameraRotation()
    {
        mouseX = Input.GetAxis("MouseX") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("MouseY") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.transform.Rotate(Vector3.up * mouseX);
    }
}
