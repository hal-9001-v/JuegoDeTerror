using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

public class ReferenceLoader : MonoBehaviour
{
    static ReferenceLoader instance;

    private void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Object.Destroy(this);
        }

    }


    private void Start()
    {


    }

    class AssetRequest {
        GameObject go;
        AssetReference reference;
        
    }

}
