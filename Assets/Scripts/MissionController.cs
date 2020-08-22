using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class MissionController : MonoBehaviour
{
    private TextMeshProUGUI missionCanvasText;

    private void Start()
    {
        missionCanvasText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            NextMission(LanguageController.language);
        }
    }

    public void NextMission(int languageIndex)
    {
        missionCanvasText.text = LanguageController.GetTextInLanguage("Mission3", languageIndex);
    }
}
