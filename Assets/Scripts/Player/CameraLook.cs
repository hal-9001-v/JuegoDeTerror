using UnityEngine;

public class CameraLook : PlayerComponent
{
    public static CameraLook sharedInstance;

    [Range(0.1f, 10)]
    public float mouseSensitivity;
    public GameObject playerBody;

    Vector2 aim;
    bool haveToAim;

    float xRotation = 0.0f;

    private void Awake()
    {
        sharedInstance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        /*
        if (PlayerPrefs.GetFloat("sensibility") >= 50)
        {
            mouseSensitivity = PlayerPrefs.GetFloat("sensibility");
        }*/
    }

    void FixedUpdate()
    {
        cameraRotation();
    }

    public void cameraRotation()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame && playerBody.GetComponent<PlayerMovement>().isReading == false)
        {
            xRotation -= aim.y * mouseSensitivity;
            xRotation = Mathf.Clamp(xRotation, -80, 80f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            //playerBody.transform.Rotate(Vector3.right * aim.y);
            playerBody.transform.Rotate(Vector3.up * aim.x * mouseSensitivity);

        }

    }

    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.Aim.performed += ctx => {
            aim = ctx.ReadValue<Vector2>();
        };
        pc.Normal.Aim.canceled += ctx => aim = Vector2.zero;

    }
}
