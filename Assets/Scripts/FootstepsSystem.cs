using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSystem : MonoBehaviour
{
    public static FootstepsSystem sharedInstance;
    public List<GroundType> type = new List<GroundType>();
    public string currentGround;

    private void Awake()
    {
        sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentGround = type[0].name;
    }

    public AudioClip ChooseSound(string tag)
    {
        AudioClip sound;
        GroundType tagType = null;
        for(int i = 0; i < type.Count; i++)
        {
            if(tag == type[i].name)
            {
                tagType = type[i];
            }
        }

        sound = SetGroundType(tagType);

        return sound;
    }

    public AudioClip SetGroundType(GroundType newGround)
    {
        AudioClip sound = null;
        if (newGround == null)
        {
            Debug.LogError("FootstepsSystem: Null Ground");
        }
        else
        {
            if (this.currentGround != newGround.name)
            {
                this.currentGround = newGround.name;
            }

            sound = RandomSound(newGround);
        }

        return sound;
    }

    public AudioClip RandomSound(GroundType newGround)
    {
        AudioClip sound;
        int random;

        random = Random.Range(0, newGround.sounds.Length);
        sound = newGround.sounds[random];

        return sound;
    }
}

[System.Serializable]
public class GroundType
{
    public string name;
    public AudioClip[] sounds;
}
