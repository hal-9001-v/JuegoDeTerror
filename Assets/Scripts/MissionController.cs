using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class MissionController : MonoBehaviour
{
    [TextArea(0, 3)] public List<string> spanishMissionList = new List<string>();
    [TextArea(0, 3)] public List<string> englishMissionList = new List<string>();

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
            NextMission(LanguageController.language);
        }
    }

    public void NextMission(int languageCode)
    {
        if (languageCode == 0)
        {
            if ((missionIndex + 1) < englishMissionList.Count())
            {
                missionIndex++;
                missionCanvasText.text = englishMissionList[missionIndex];
            }
        }
        else
        {
            if ((missionIndex + 1) < spanishMissionList.Count())
            {
                missionIndex++;
                missionCanvasText.text = spanishMissionList[missionIndex];
            }
        }
    }
}
