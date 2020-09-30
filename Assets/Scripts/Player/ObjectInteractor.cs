using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractor : MonoBehaviour
{
    public Camera myCamera;

    [Range(1, 200)]
    public float range;

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

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        checkForObject();

    }

    public void checkForObject()
    {
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, range))
        {

            if (hit.collider.tag == "Interactable")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interactable myInteractable = hit.collider.gameObject.GetComponent<Interactable>();

                    if (myInteractable != null)
                    {
                        myInteractable.interact();

                    }
                    else
                    {
                        myInteractable = hit.collider.gameObject.GetComponentInChildren<Interactable>();

                        if (myInteractable != null)
                        {
                            myInteractable.interact();
                        }
                        else
                        {
                            Debug.LogWarning("Object " + hit.collider.gameObject.name + " is tagged as Interactable but has no Interactable component!");
                        }
                    }
                }
            }
        }
    }

}
