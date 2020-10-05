using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTry : MonoBehaviour
{
    PlayerControls myControls;

    // Start is called before the first frame update
    void Start()
    {
        
        myControls = new PlayerControls();

        myControls.Normal.Move.performed += ctx => hoi();


        myControls.Normal.Enable();

        Debug.Log("HERE");
    }


    void hoi()
    {
        Debug.Log("HOI");
    }



}
