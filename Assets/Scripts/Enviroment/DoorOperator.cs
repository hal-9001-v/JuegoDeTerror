using System.Collections.Generic;
using UnityEngine;

public class DoorOperator : MonoBehaviour
{
    public List<Door> doors;

    public void closeDoors(bool exclude)
    {
        if (!exclude)
        {
            foreach (Door d in doors)
            {
                d.closeDoor();
            }
        }
        else
        {
            foreach (Door d in FindObjectsOfType<Door>())
            {
                if (!doors.Contains(d))
                    d.closeDoor();
            }
        }
    }

    public void openDoors(bool exclude)
    {
        if (!exclude)
        {
            foreach (Door d in doors)
            {
                d.openDoor();
            }
        }
        else
        {
            foreach (Door d in FindObjectsOfType<Door>())
            {
                if (!doors.Contains(d))
                    d.openDoor();
            }
        }

    }

    public void lockDoors(bool exclude)
    {
        if (!exclude)
        {
            foreach (Door d in doors)
            {
                d.setLock(true);
            }
        }
        else
        {
            foreach (Door d in FindObjectsOfType<Door>())
            {
                if (!doors.Contains(d))
                    d.setLock(true);
            }
        }
    }


    public void superLockDoors(bool exclude)
    {
        if (!exclude)
        {
            foreach (Door d in doors)
            {
                d.setSuperLock(true);
            }
        }
        else
        {
            foreach (Door d in FindObjectsOfType<Door>())
            {
                if (!doors.Contains(d))
                    d.setSuperLock(true);
            }
        }
    }


    public void unlockDoors(bool exclude)
    {
        if (!exclude)
        {
            foreach (Door d in doors)
            {
                d.setLock(false);
            }
        }
        else
        {
            foreach (Door d in FindObjectsOfType<Door>())
            {
                if (!doors.Contains(d))
                    d.setLock(false);
            }
        }

    }

    public void unlockAndOpenDoors(bool exclude)
    {
        unlockDoors(exclude);
        openDoors(exclude);
    }

}
