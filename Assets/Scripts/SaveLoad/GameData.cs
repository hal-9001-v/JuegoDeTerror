﻿using System.Collections;
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

    public InventoryData myInventory;

    public PursuerData myPursuerData;

    public InteractableData[] myInteractablesData;

    public TriggerData[] myTriggerData;

    public RadioZoneData[] myRadioZoneData;

    public int safeTask;

    public GameData()
    {

    }
}

[Serializable]
public class StatsData
{
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

    public bool torchReadyToUse;
    public bool torchIsLit;



    public PlayerData(Vector3 position, Vector3 rotation, Vector3 camRotation, bool ready, bool lit)
    {
       playerPosition.x = position.x;
       playerPosition.y = position.y;
       playerPosition.z = position.z;

        playerRotation.x = rotation.x;
        playerRotation.y = rotation.y;
        playerRotation.z = rotation.z;

        cameraRotation.x = camRotation.x;
        cameraRotation.y = camRotation.y;
        cameraRotation.z = camRotation.z;

        torchReadyToUse = ready;
        torchIsLit = lit;

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
public class InteractableData
{
    public string interactableName { get; private set; }
    public bool interactionDone { get; private set; }
    public bool readyForInteraction { get; private set; }

    //Dont judge me please
    public bool doorLocked;
    public bool doorOpen;

    public InteractableData(string name, bool done, bool ready)
    {
        interactableName = name;
        interactionDone = done;
        readyForInteraction = ready;
    }

}

[Serializable]
public class TriggerData
{
    public string triggerName { get; private set; }
    public bool triggerDone { get; private set; }
    public bool triggerReady { get; private set; }


    public TriggerData(string name, bool done, bool readyForInteraction) {
        triggerName = name;
        triggerDone = done;
        triggerReady = readyForInteraction;

    }

}

[Serializable]
public class InventoryData {
    public string[] itemNames;

}

[Serializable]
public class RadioZoneData
{
    public string radioName { get; private set; }
    public bool done { get; private set; }
    public bool readyForInteraction { get; private set; }

    public RadioZoneData(string name, bool d, bool ready) {
        radioName = name;
        done = d;
        readyForInteraction = ready;
    }

}
