using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTracker : MonoBehaviour
{
    public RadioZone currentZone;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "RadioZone")
        {
            
            currentZone = collision.gameObject.GetComponent<RadioZone>();

            if (!currentZone.readyForInteraction || (currentZone.onlyOnce && currentZone.done)) {
                currentZone = null;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RadioZone") {
            currentZone = null;
        }
    }

    //Ensure there is no active room
    public void setZoneNull() {
        currentZone = null;
    }

}
