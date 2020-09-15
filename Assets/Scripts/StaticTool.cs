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

    static public void move()
    {
        

    }

    IEnumerator Move()
    {
        yield return null;
    }

}
