using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCollider : MonoBehaviour
{
    public string tagOfCollision;
    public int animationID = -1;
    public bool hasToCollide;

    public bool alwaysReady;

    public bool isReady { get; private set; }
    private bool hasCollided = false;


    private void Awake()
    {
        if (alwaysReady)
        {
            isReady = true;
            
        }
        else {
            Collider myCollider = GetComponent<Collider>();

            if (myCollider == null)
            {
                Debug.LogWarning("No collider is attached in " + gameObject.name);
            }
            else if (!myCollider.isTrigger)
            {
                Debug.LogWarning("Collider is not Trigger in " + gameObject.name);
            }

            if (!StaticTool.DoesTagExist(tagOfCollision))
            {
                Debug.LogWarning("Tag does not exist");
            }
        }
        

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Equals(tagOfCollision))
            hasCollided = true;
    }

    private void Update()
    {
        //Update goes after CollisionChecking. hasCollided turns to false on every frame so when it is true on Update, a collision has occurred
        if (hasCollided)
        {
            if (hasToCollide)
                isReady = true;

            if (!hasToCollide)
                isReady = false;
        }
        else
        {
            if (hasToCollide)
                isReady = false;

            if (!hasToCollide)
                isReady = true;

        }

        hasCollided = false;
    }
}
