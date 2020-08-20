using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class MissionController : MonoBehaviour
{
    [TextArea(0, 3)] public List<string> missionList = new List<string>();
    private TextMeshProUGUI missionCanvasText;

    private int missionIndex;

    private void Start()
    {
        missionCanvasText = GetComponent<TextMeshProUGUI>();
        missionIndex = -1;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            NextMission();
        }
    }

    public void NextMission()
    {
        if((missionIndex + 1) < missionList.Count())
        {
            missionIndex++;
            missionCanvasText.text = missionList[missionIndex];
        }
    }
}
