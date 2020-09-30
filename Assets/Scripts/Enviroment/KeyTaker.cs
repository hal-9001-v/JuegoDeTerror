using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTaker : MonoBehaviour
{
    public List<Key> takenKeys;

    private void Awake()
    {
        if (takenKeys == null)
            takenKeys = new List<Key>();
    }

    public void AddKey(Key key)
    {
        if (!takenKeys.Contains(key))
        {
            takenKeys.Add(key);

        }
    }

}
