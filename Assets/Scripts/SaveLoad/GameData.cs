using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public Vector3 GetVector3()
    {
        return new Vector3(x, y, z);
    }
}

[Serializable]
public class GameData
{
    public PlayerData myPlayerData;

    public PlayerInventory myInventory;

    public PursuerData myPursuerData;

    public InteractableData[] myInteractablesData;

    public int safeTask;

    public GameData()
    {

    }
}

[Serializable]
public class StatsData {
    public float gamePadSens;
    public float mouseSens;
    public int language;

    public float volume;

}

[Serializable]
public class PlayerData
{
    public SerializableVector3 playerPosition;
    public SerializableVector3 playerRotation;
    public SerializableVector3 cameraRotation;
    

    public PlayerData(Vector3 position, Vector3 rotation, Vector3 camRotation)
    {
        this.playerPosition.x = position.x;
        this.playerPosition.y = position.y;
        this.playerPosition.z = position.z;

        playerRotation.x = rotation.x;
        playerRotation.y = rotation.y;
        playerRotation.z = rotation.z;

        cameraRotation.x = camRotation.x;
        cameraRotation.y = camRotation.y;
        cameraRotation.z = camRotation.z;

    }
}

[Serializable]
public class PlayerInventory
{
    public string[] objectNames = new string[5];

    public PlayerInventory(string[] names)
    {
        this.objectNames[0] = names[0];
        this.objectNames[1] = names[1];
        this.objectNames[2] = names[2];
        this.objectNames[3] = names[3];
        this.objectNames[4] = names[4];
    }
}


[Serializable]
public class PursuerData
{
    public string currentRoomName;
    public int currentState;

    public PursuerData(Room currentRoom, int currentState)
    {
        if (currentRoom != null)
        {
            currentRoomName = currentRoom.name;
        }


        this.currentState = currentState;
    }

}

[Serializable]
public class InteractableData {
    public string interactableName { get; private set; }
    public bool interactionDone { get; private set; }
    public bool readyForInteraction { get; private set; }

    public InteractableData(string name, bool done, bool ready) {
        interactableName = name;
        interactionDone = done;
        readyForInteraction = ready;
    }
}
