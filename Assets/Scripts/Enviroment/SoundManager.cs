using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridMap))]
public class SoundManager : MonoBehaviour
{
    public Transform cameraTransform;
    GridMap myGridMap;

    public AudioSource sourceReference;

    [Range(1, 100)]
    public int instances = 1;

    AudioSource[] sources;

    /*
    * North -> Z+
    * South -> Z-
    * East  -> X+
    * West  -> X-
    */

    public enum Direction
    {
        North,
        South,
        East,
        West,
        None
    }

    public enum PositionToPlayer
    {
        Behind,
        Forward,
        Right,
        Left

    }

    private void Awake()
    {
        myGridMap = GetComponent<GridMap>();

        sources = new AudioSource[instances];
        sources[0] = sourceReference;

        for (int i = 1; i < sources.Length; i++)
        {
            sources[i] = Instantiate(sourceReference);
            sources[i].name = "Sound Manager Source " + i;
            sources[i].transform.parent = sourceReference.transform;
        }
    }

    public void stopSounds()
    {
        foreach (AudioSource source in sources)
        {
            source.Stop();

        }
    }


    Direction getFacingDirection()
    {
        Direction d = Direction.None;

        if (cameraTransform != null)
        {
            //Y indicates Direction
            float y = cameraTransform.rotation.eulerAngles.y;

            //0 -> North
            if (y > 315 || y <= 45)
                d = Direction.North;
            ////90 -> East
            if (y > 45 && y <= 135)
                d = Direction.East;

            //180 -> South
            if (y > 135 && y <= 225)
                d = Direction.South;

            //270 -> West
            if (y > 225 && y <= 315)
                d = Direction.West;

        }

        return d;


    }

    AudioSource getFreeAudioSource()
    {
        if (sources != null)
            foreach (AudioSource s in sources)
            {
                if (s != null && !s.isPlaying)
                {
                    return s;
                }
            }

        return null;
    }

    public bool playSoundOffset(int x, int y, AudioClip clip, AudioSource source)
    {
        if (source != null)
        {
            source.transform.position = myGridMap.getGridPositionWithOffset(cameraTransform.position, x, y);
            source.clip = clip;
            source.Play();

            return true;
        }
        else
        {
            return false;
        }

    }

    public bool playSoundInDirection(PositionToPlayer position, int range, AudioClip clip)
    {
        AudioSource source = getFreeAudioSource();

        if (source != null)
        {

            Direction direction = getFacingDirection();
            int x = 0;
            int y = 0;


            //x->x y->z
            switch (direction)
            {
                case Direction.North:

                    switch (position)
                    {
                        case PositionToPlayer.Forward:
                            x = 0;
                            y = 1;

                            break;

                        case PositionToPlayer.Behind:
                            x = 0;
                            y = -1;
                            break;

                        case PositionToPlayer.Right:
                            x = 1;
                            y = 0;
                            break;

                        case PositionToPlayer.Left:
                            x = -1;
                            y = 0;
                            break;

                    }

                    break;

                case Direction.South:

                    switch (position)
                    {
                        case PositionToPlayer.Forward:
                            x = 0;
                            y = -1;
                            break;

                        case PositionToPlayer.Behind:
                            x = 0;
                            y = 1;
                            break;

                        case PositionToPlayer.Right:
                            x = -1;
                            y = 0;
                            break;

                        case PositionToPlayer.Left:
                            x = 1;
                            y = 0;
                            break;

                    }



                    break;

                case Direction.East:

                    switch (position)
                    {
                        case PositionToPlayer.Forward:
                            x = -1;
                            y = 0;
                            break;

                        case PositionToPlayer.Behind:
                            x = 1;
                            y = 0;
                            break;

                        case PositionToPlayer.Right:
                            x = 0;
                            y = 1;
                            break;

                        case PositionToPlayer.Left:
                            x = 0;
                            y = -1;
                            break;

                    }

                    break;

                case Direction.West:

                    switch (position)
                    {
                        case PositionToPlayer.Forward:
                            x = 1;
                            y = 0;
                            break;

                        case PositionToPlayer.Behind:
                            x = -1;
                            y = 0;
                            break;

                        case PositionToPlayer.Right:
                            x = 0;
                            y = -1;
                            break;

                        case PositionToPlayer.Left:
                            x = 0;
                            y = 1;
                            break;

                    }

                    break;

                default:
                    break;
            }

            playSoundOffset(x * range, y * range, clip, source);
            return true;

        }
        else
        {
            return false;
        }
    }

}
