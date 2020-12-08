using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTracker : MonoBehaviour
{
    public string[] text {get; private set;}
    public float delay { get; private set; }

    private void Awake()
    {
        text = new string[1] ;

        text[0] = "...";
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "RadioZone") {
            RadioZone zone = collision.gameObject.GetComponent<RadioZone>();

            if (zone != null) {
                text = zone.Text;
                delay = zone.delay;
            }

        }
    }
}
