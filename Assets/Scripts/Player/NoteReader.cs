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

    public AudioSource audioSource;

    //Canvas de la nota
    public Canvas noteCanvas;

    //Texto intercambiable de la nota
    public TextMeshProUGUI textMesh;

    //Barra de scroll de la derecha
    public Scrollbar scrollbar;


    private void Awake()
    {
        //Singleton
        if (instance != null)
        {
            Debug.LogWarning("Note Reader is a singleton. Kept: " + instance.gameObject.name + " Discarded: " + gameObject.name);
            Destroy(this);
        }
        else {
            instance = this;
        }


    }

    public void startReading(string text) {
        //Entrar al modo Nota
        if (PlayerMovement.sharedInstance.isReading == false)
        {
            setText(text);
            scrollbar.value = 1;
            PlayerMovement.sharedInstance.isReading = true;
            audioSource.PlayOneShot(noteOpenSound);

            noteCanvas.enabled = true;
            
        }
    }

    void stopReading() {
        PlayerMovement.sharedInstance.isReading = false;
        audioSource.PlayOneShot(noteCloseSound);
        noteCanvas.enabled = false;
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

        pc.Normal.Cancel.performed += ctx => {
            if (PlayerMovement.sharedInstance.isReading)
                stopReading();
        };

      

    }

}
