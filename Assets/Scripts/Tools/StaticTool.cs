using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTool : MonoBehaviour
{
    static public T GetComponentInAll<T>(GameObject go)
    {
        T component;
        component = go.GetComponent<T>();

        if (component == null)
        {
            component = go.GetComponentInChildren<T>();
        }

        return component;
    }

    static public bool DoesTagExist(string tag)
    {
        try
        {
            GameObject.FindGameObjectWithTag(tag);
            return true;
        }
        catch
        {
            return false;
        }
    }


    static public void FindInSceneWithLog<T>(T myComponent) where T : MonoBehaviour
    {

        if (myComponent == null)
        {
            myComponent = FindObjectOfType<T>();

            if (myComponent == null)
            {
                Debug.LogWarning("No " + typeof(T).Name + " in Scene");
            }

        }
    }
}
