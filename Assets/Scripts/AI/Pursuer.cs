﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : MonoBehaviour
{
    PlayerTracker player;
    EnviromentManager myEM;
    CutsceneController myCutsceneController;

    public Stack<Room> rooms;
    public Room currentRoom;

    public int currentState;

    public InactiveState myInactiveState;
    public PursueIdleState myPursueIdleState;
    public RandomIdleState myRandomIdleState;

    public KillPlayerState myKillPlayerState;
    public DecideActionState myDecideActionState;

    private enum pursuerStates
    {
        Inactive = 0,
        Patrol = 1,
        Pursue = 2
    }

    public PursuerSave saveDataOfPursuer()
    {
        return new PursuerSave(currentRoom, currentState);
    }

    public void LoadDataOfPursuer(PursuerSave ps)
    {
        currentState = ps.currentState;

        bool roomFound = false;

        foreach (Room r in FindObjectsOfType<Room>())
        {
            if (r.GetInstanceID() == ps.currentRoomID)
            {
                currentRoom = r;
                roomFound = true;
                break;
            }

        }

        if (roomFound)
        {
            switch (currentState)
            {

                case (int)pursuerStates.Inactive:
                    myPursueIdleState.enter();
                    break;


                case (int)pursuerStates.Patrol:
                    myRandomIdleState.enter();
                    break;


                case (int)pursuerStates.Pursue:
                    myPursueIdleState.enter();
                    break;

            }
        }
        else
        {
            Debug.LogError("Room wasn't found");
        }



    }

    [System.Serializable]
    public class PursuerSave
    {

        public int currentRoomID;
        public int currentState;

        public PursuerSave(Room currentRoom, int currentState)
        {
            this.currentRoomID = currentRoom.GetInstanceID();
            this.currentState = currentState;
        }

    }


    //Time Waiting on every room
    public float patrolTime = 2;
    public float pursueTime = 1;
    public float timeForKill = 2;

    public int spottingDistance = 2;

    private void Awake()
    {
        initialize();

    }

    // Start is called before the first frame update
    void Start()
    {
        myInactiveState.enter();
    }

    private void initialize()
    {
        player = FindObjectOfType<PlayerTracker>();
        myEM = FindObjectOfType<EnviromentManager>();
        myCutsceneController = FindObjectOfType<CutsceneController>();

        myInactiveState = new InactiveState(this);
        myPursueIdleState = new PursueIdleState(this, pursueTime);

        myRandomIdleState = new RandomIdleState(this, patrolTime);

        myKillPlayerState = new KillPlayerState(this, timeForKill);

        myDecideActionState = new DecideActionState(this);

    }

    public void killPlayer()
    {
        Debug.Log("Kill Player");
        myCutsceneController.killPlayer();
    }
    public Room moveToNextRoom()
    {
        currentRoom.setSafeRoom();

        foreach (Room r in currentRoom.neighbourRooms)
        {
            r.setSafeRoom();
        }

        currentRoom = rooms.Pop();

        currentRoom.setDangerColor();

        foreach (Room r in currentRoom.neighbourRooms)
        {
            r.setNearbyRoom();
        }

        //Move to Room(Just for debugging)
        transform.position = currentRoom.transform.position;

        return currentRoom;
    }

    public void startPursuing(Room startingRoom)
    {
        if (player.currentRoom != null)
        {
            //Stack the path to player
            rooms = myEM.myRoomMap.getPath(startingRoom, player.currentRoom);
            currentRoom = startingRoom;

            myPursueIdleState.enter();
            Debug.LogWarning("PURSUER: Start Pursuing");
        }
        else
        {
            Debug.LogWarning("Player is in no room");
        }

    }

    public void startPatrol(Room startingRoom)
    {

        //Stack the path to player
        rooms = myEM.myRoomMap.getPath(startingRoom, myEM.myRoomMap.getRandomRoom());
        currentRoom = startingRoom;

        myRandomIdleState.enter();
        //Debug.Log("PURSUER: Start Patrol");
    }
    public void updatePursuing()
    {
        //Stack the path to player
        rooms = myEM.myRoomMap.getPath(currentRoom, myEM.myRoomMap.getRandomRoom());
    }

    public int distanceToPlayer()
    {
        return myEM.myRoomMap.getPath(currentRoom, player.currentRoom).Count;
    }

    public class InactiveState : State
    {
        Pursuer myPursuer;
        public InactiveState(Pursuer myPursuer)
        {
            this.myPursuer = myPursuer;

        }

        public void enter()
        {
            myPursuer.currentState = (int)pursuerStates.Inactive;
        }


        public void execute()
        {

        }

        public void exit()
        {

        }


    }

    public class PursueIdleState : State
    {
        public float holdingTime;
        public float timeCounter;

        Pursuer myPursuer;
        PursueMoveState myPursueMoveState;

        public PursueIdleState(Pursuer myPursuer, float holdingTime)
        {
            this.myPursuer = myPursuer;
            this.holdingTime = holdingTime;

            myPursueMoveState = new PursueMoveState(myPursuer);

        }

        public void enter()
        {
            timeCounter = 0;
            myPursuer.currentState = (int)pursuerStates.Pursue;
            execute();
        }

        private IEnumerator Execute()
        {
            yield return new WaitForSeconds(holdingTime * myPursuer.currentRoom.weight);

            exit();
        }

        public void execute()
        {
            myPursuer.StartCoroutine(Execute());
        }

        public void exit()
        {
            {
                if (myPursuer.rooms.Count != 0)
                {
                   myPursueMoveState.enter();
                }
                else
                {
                    myPursuer.myDecideActionState.enter();
                }

            }

        }
    }

    public class PursueMoveState : State
    {

        Pursuer myPursuer;

        public PursueMoveState(Pursuer pursuer)
        {
            myPursuer = pursuer;
        }

        public void enter()
        {
            execute();
        }

        public void execute()
        {
            myPursuer.moveToNextRoom();
            exit();
        }

        public void exit()
        {
            myPursuer.myPursueIdleState.enter();

        }
    }

    public class RandomIdleState : State
    {
        public float holdingTime;
        public float timeCounter;

        Pursuer myPursuer;

        RandomMoveState myRandomMoveState;

        public RandomIdleState(Pursuer myPursuer, float holdingTime)
        {
            this.myPursuer = myPursuer;
            this.holdingTime = holdingTime;

            myRandomMoveState = new RandomMoveState(myPursuer);
        }

        public void enter()
        {
            myPursuer.currentState = (int)pursuerStates.Patrol;

            timeCounter = 0;

            execute();
        }

        private IEnumerator Execute()
        {
            yield return new WaitForSeconds(holdingTime * myPursuer.currentRoom.weight);

            exit();
        }

        public void execute()
        {
            myPursuer.StartCoroutine(Execute());
        }

        public void exit()
        {
            if (myPursuer.distanceToPlayer() <= myPursuer.spottingDistance)
            {
                myPursuer.startPursuing(myPursuer.currentRoom);
            }
            else
            {
                if (myPursuer.rooms.Count != 0)
                {
                    myRandomMoveState.enter();
                }
                else
                {
                    myPursuer.startPatrol(myPursuer.currentRoom);
                }

            }

        }

    }

    public class RandomMoveState : State
    {
        Pursuer myPursuer;

        public RandomMoveState(Pursuer pursuer)
        {
            myPursuer = pursuer;
        }

        public void enter()
        {

            execute();
        }

        public void execute()
        {
            myPursuer.moveToNextRoom();
            exit();
        }

        public void exit()
        {
            myPursuer.myRandomIdleState.enter();

        }
    }

    public class KillPlayerState : State
    {
        Pursuer myPursuer;
        float timeToKill;

        public KillPlayerState(Pursuer pursuer, float timeToKill)
        {
            myPursuer = pursuer;
            this.timeToKill = timeToKill;

        }

        public void enter()
        {
            execute();
        }

        public void execute()
        {
            myPursuer.StartCoroutine(Execute());
        }

        private IEnumerator Execute()
        {

            yield return new WaitForSeconds(timeToKill * myPursuer.currentRoom.weight);

            exit();
        }

        public void exit()
        {
            if (myPursuer.currentRoom == myPursuer.player.currentRoom)
            {
                myPursuer.killPlayer();
                myPursuer.myInactiveState.enter();
            }
            else
            {
                myPursuer.myDecideActionState.enter();
            }
        }
    }

    public class DecideActionState : State
    {
        Pursuer myPursuer;

        public DecideActionState(Pursuer myPursuer)
        {
            this.myPursuer = myPursuer;
        }

        public void enter()
        {
            execute();
        }

        public void execute()
        {
            exit();
        }

        public void exit()
        {
            if (myPursuer.currentRoom == myPursuer.player.currentRoom)
            {
                myPursuer.myKillPlayerState.enter();
                return;
            }

            if (myPursuer.distanceToPlayer() < myPursuer.spottingDistance)
            {
                myPursuer.startPursuing(myPursuer.currentRoom);
                return;
            }
            else
            {
                myPursuer.startPatrol(myPursuer.currentRoom);
                return;
            }

        }
    }
    public interface State
    {
        void enter();

        void execute();

        void exit();
    }
}




