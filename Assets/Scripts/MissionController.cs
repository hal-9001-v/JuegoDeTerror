using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class MissionController : MonoBehaviour
{
    private TextMeshProUGUI missionCanvasText;
    private int missionNumber = 1;
    public int totalMissions = 10;

    private void Start()
    {
        missionCanvasText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
      /*  if(Input.GetKeyDown(KeyCode.P))
        {
            NextMission();
        }
        */
    }

    public void NextMission()
    {
        missionCanvasText.text = LanguageController.GetTextInLanguage("Mission" + missionNumber);
        if (missionNumber < totalMissions)
        {
            missionNumber++;
        }
    }
}
