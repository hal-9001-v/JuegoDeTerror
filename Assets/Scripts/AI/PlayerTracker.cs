﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public Room currentRoom;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Room")
        {
            currentRoom = collider.GetComponent<Room>();

            if (currentRoom != null)
                currentRoom.atEnter.Invoke();
            else
            {
                Debug.LogError("Object" + collider.gameObject.name + " is tagged as Room but has no Room Component");
            }
        }
        else if (collider.gameObject.tag == "RoomCollider")
        {
            currentRoom = collider.GetComponent<RoomCollider>().myRoom;

            if (currentRoom != null)
                currentRoom.atEnter.Invoke();
            else
            {
                Debug.LogError("Object: " + collider.gameObject.name + " is tagged as Room Collider but has no RoomCollider Component");
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Room")
        {
            Room myRoom;

            myRoom = other.gameObject.GetComponent<Room>();

            if (myRoom == null)
            {
                Debug.LogWarning("Room has no Room Component");
                return;
            }

            currentRoom = myRoom;

        }
        else if (other.tag == "RoomCollider")
        {

            Room myRoom;

            myRoom = other.gameObject.GetComponent<RoomCollider>().myRoom;

            if (myRoom != null)
            {
                currentRoom = myRoom;


            }
            else
            {
                Debug.LogWarning("Room has no RoomCollider Component");

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Room")
        {
            other.gameObject.GetComponent<Room>().atExit.Invoke();
        }
        else if (other.tag == "RoomCollider")
        {
            other.gameObject.GetComponent<RoomCollider>().myRoom.atExit.Invoke();
        }


    }


}
