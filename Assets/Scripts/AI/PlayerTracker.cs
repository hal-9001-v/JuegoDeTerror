using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public Room currentRoom;

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag != "Room") return;

        Room myRoom;

        myRoom = collider.gameObject.GetComponent<Room>();

        if (myRoom == null)
        {
            Debug.LogWarning("Room has no Room Component");
            return;
        }

        currentRoom = myRoom;

        currentRoom.atEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Room") return;

        other.gameObject.GetComponent<Room>().atExit.Invoke();
    }


}
