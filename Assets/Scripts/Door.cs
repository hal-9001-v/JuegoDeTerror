using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Range(1, 3)]
    public float activationDistance;
    private Vector3 center;
    
    Animator myAnimator;
    Transform player;

    public KeyCode openKey;


    // Start is called before the first frame update
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null) {
            Debug.LogWarning("No player in Scene");
        }

        if (myAnimator == null) {
            Debug.LogWarning("No Animator Component in Object");
        }
        
    }

    private void Start()
    {
        center = GetComponent<Renderer>().bounds.center;
    
    }

    private void Update()
    {
        if (Vector3.Distance(center, player.position) < activationDistance) {
            if (Input.GetKeyDown(openKey)) {
                    myAnimator.SetTrigger("OpenCloseDoor");

            }
        }
    }


    private void OnDrawGizmos()
    {
        center = GetComponent<Renderer>().bounds.center;

        Gizmos.DrawWireSphere(center, activationDistance);
    }
}
