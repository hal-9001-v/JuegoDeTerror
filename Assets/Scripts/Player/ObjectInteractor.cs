using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectInteractor : PlayerComponent
{
    public Camera myCamera;

    [Range(1, 200)]
    public float range;
    //public float minInteractDistance = 10.0f;

    //private float interactDistance;
    Interactable selectedObject;
    Ray ray;

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        if (!myCamera)
        {
            myCamera = FindObjectOfType<Camera>();

            if (!myCamera) Debug.LogWarning("No camera assigned");
            else
                Debug.LogWarning("Took camera " + myCamera.name + " as player camera");
        }
    }

    public void Update()
    {
        checkForObject();

        if (selectedObject != null && selectedObject.readyForInteraction && text != null)
        {
            text.text = "[E] Interact";

        }
        else
        {
            text.text = "";
        }
    }

    public void checkForObject()
    {
        ray = myCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * range);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, range))
        {
            Debug.DrawRay(transform.position, ray.direction);
            if (hit.collider.tag == "Interactable")
            {

                Interactable auxInteractable = hit.collider.gameObject.GetComponentInParent<Interactable>();
                if (selectedObject != auxInteractable)
                {
                    if (auxInteractable != null)
                    {

                        if (selectedObject != null)
                        {
                            selectedObject.selectForInteraction(false);
                        }

                        selectedObject = auxInteractable;

                        selectedObject.selectForInteraction(true);
                    }
                }

                //Dont turn null selectedObject
                return;

            }
        }

        if (selectedObject != null)
        {
            selectedObject.selectForInteraction(false);
            selectedObject = null;
        }

    }

    private void interact()
    {

        if (this.enabled)
        {
            if (selectedObject != null && selectedObject.readyForInteraction)
            {
                selectedObject.invokeInteractionActions();
            }

        }

    }

    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.Interaction.performed += ctx => interact();

    }
}
