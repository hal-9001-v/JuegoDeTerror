using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractor : PlayerComponent
{
    public Camera myCamera;

    [Range(1, 200)]
    public float range;

    Interactable selectedObject;

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
        
    }



    public void checkForObject()
    {
        Ray ray = myCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, range))
        {

            if (hit.collider.tag == "Interactable")
            {
                Interactable auxInteractable = hit.collider.gameObject.GetComponent<Interactable>();
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

            }
        }
        else
        {
            if (selectedObject != null)
            {
                selectedObject.selectForInteraction(false);
                selectedObject = null;
            }

        }
    }

    private void interact()
    {

        if (this.enabled)
        {
            if (selectedObject != null)
            {
                selectedObject.interact();

            }

        }

    }




    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.Interaction.performed += ctx => interact();

    }
}
