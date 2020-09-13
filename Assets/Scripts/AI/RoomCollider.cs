using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class RoomCollider : MonoBehaviour
{
    public Room myRoom { get; private set; }

    private void Start()
    {
        if (gameObject.transform.parent != null)
        {
            myRoom = gameObject.transform.parent.GetComponent<Room>();

            if (myRoom == null)
            {
                Debug.LogError("ROOMCOLLIDER: " + gameObject.name + "'s parent has no Room component");
            }
        }
        else
        {
            Debug.LogError("ROOMCOLLIDER: " + gameObject.name + " has no parent GameObject");
        }

        if (gameObject.GetComponent<Collider>() == null)
        {
            Debug.LogError("ROOMCOLLIDER: " + gameObject.name + " has no collider attached");
        }

    }



}
