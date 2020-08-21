using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class MissionController : MonoBehaviour
{
    private List<string>[] languageArray = new List<string>[LanguageController.totalLanguages];
    [TextArea(0, 3)] public List<string> spanishMissionList = new List<string>();
    [TextArea(0, 3)] public List<string> englishMissionList = new List<string>();
    private TextMeshProUGUI missionCanvasText;

    private int missionIndex;

    private void Start()
    {
        missionCanvasText = GetComponent<TextMeshProUGUI>();
        missionIndex = -1;

        for(int i = 0; i < LanguageController.totalLanguages; i++)
        {
            languageArray[i] = 
        }
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
