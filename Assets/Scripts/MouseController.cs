using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float mouseSensevitiy = 10f;
    public Transform playerBody;

    float xRotation = 0f;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        float xInput = Input.GetAxis("Mouse X") * mouseSensevitiy;
        float yInput = Input.GetAxis("Mouse Y") * mouseSensevitiy;

        xRotation -= yInput;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * xInput);



    }
}