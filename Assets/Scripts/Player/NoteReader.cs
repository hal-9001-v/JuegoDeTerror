using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteReader : PlayerComponent
{
    static NoteReader instance;

    //Sonido al abrir la nota
    public AudioClip noteOpenSound;

    //Sonido al cerrar la nota
    public AudioClip noteCloseSound;

    private AudioSource audioSource;

    //Texto intercambiable de la nota
    public TextMeshProUGUI textMesh;

    //Barra de scroll de la derecha
    public Scrollbar scrollbar;

    public CanvasManager myCanvasManager;

    GameManager myGameManager;

    private void Awake()
    {
        //Singleton
        if (instance != null)
        {
            Debug.LogWarning("Note Reader is a singleton. Kept: " + instance.gameObject.name + " Discarded: " + gameObject.name);
            Destroy(this);
        }
        else
        {
            instance = this;
        }


    }

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        myGameManager = FindObjectOfType<GameManager>();
    }

    public void startReading(string text)
    {
        //Entrar al modo Nota
        if (PlayerMovement.sharedInstance.isReading == false)
        {
            setText(text);
            scrollbar.value = 1;

            myGameManager.displayNote();

            audioSource.PlayOneShot(noteOpenSound);
        }
    }

    void stopReading()
    {

        if (PlayerMovement.sharedInstance.isReading)
        {
            myGameManager.resumeGame();
            audioSource.PlayOneShot(noteCloseSound);
        }


    }

    private void scrollInput(Vector2 input)
    {

        //Up Text
        if (input.y > 0)
        {
            scrollbar.value += 0.1f;
        }
        //Down Text
        else if (input.y < 0)
        {
            scrollbar.value -= 0.1f;
        }

    }

    public void setText(string page)
    {
        textMesh.text = page;
    }

    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.KeyAxis.performed += ctx =>
        {
            if (PlayerMovement.sharedInstance.isReading)
            {
                Vector2 input = ctx.ReadValue<Vector2>();

                scrollInput(input);

            }
        };

        pc.Normal.Cancel.performed += ctx =>
        {
            stopReading();
        };



    }

}
