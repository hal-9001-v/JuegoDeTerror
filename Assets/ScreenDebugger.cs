using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenDebugger : MonoBehaviour
{
    public TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        if (Debug.isDebugBuild)
            DontDestroyOnLoad(this);
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        if(Debug.isDebugBuild)
            Application.logMessageReceived += Log;
    }

    public void Log(string logString, string tackTrace, LogType type)
    {
        if (Debug.isDebugBuild)
            tmp.text = logString + "\n" + tmp.text;

    }

    private void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

}
