using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    private SpriteRenderer mySP;
    EnviromentManager myEM;

    [SerializeField]
    public static Color safeColor;

    // Start is called before the first frame update
    void Start()
    {
        myEM = FindObjectOfType<EnviromentManager>();

        mySP = GetComponent<SpriteRenderer>();

        if (mySP == null)
        {
            Debug.LogError("No Sprite Renderer Attached");
        }

        if (myEM == null)
        {
            Debug.LogError("No EnviromentManager in Scene");
        }


    }


    public void setSafe()
    {

        if (myEM != null)
            mySP.color = myEM.safeColor;

    }

    public void setNearby()
    {
        if (myEM != null)
            mySP.color = myEM.nearbyColor;

    }

    public void setDanger()
    {
        if (myEM != null)
            mySP.color = myEM.dangerColor;
    }
}
