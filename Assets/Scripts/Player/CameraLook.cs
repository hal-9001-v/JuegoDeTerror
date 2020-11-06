using UnityEngine;

public class CameraLook : StatsComponent
{
    public static CameraLook sharedInstance;

    [Range(0.1f, 10)]
    public float mouseSensitivity;

    [Range(0.1f, 10)]
    public float gamePadSensitivity;
    public GameObject playerBody;

    Vector2 gamePadAim;
    Vector2 mouseAim;
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
        mouseSensitivity = 2.0f;
    }

    void FixedUpdate()
    {
        cameraRotation();
    }

    public void cameraRotation()
    {
        if (playerBody.GetComponent<PlayerMovement>().isReading == false)
        {
            Vector2 aim;

            if (gamePadAim == Vector2.zero)
            {
                aim = mouseAim * mouseSensitivity;
            }
            else
            {
                aim = gamePadAim * gamePadSensitivity;
            }

            xRotation -= aim.y;
            xRotation = Mathf.Clamp(xRotation, -70, 70f);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            playerBody.transform.Rotate(Vector3.up * aim.x);

        }

    }

    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.Aim.performed += ctx =>
        {
            gamePadAim = ctx.ReadValue<Vector2>();
        };

        pc.Normal.MouseAim.performed += ctx =>
        {
            mouseAim = ctx.ReadValue<Vector2>();
        };

        pc.Normal.Aim.canceled += ctx => gamePadAim = Vector2.zero;

        pc.Normal.MouseAim.canceled += ctx => mouseAim = Vector2.zero;

    }

    public override void setStats(StatsData data)
    {
        mouseSensitivity = data.mouseSens;
        gamePadSensitivity = data.gamePadSens;
    }
}
