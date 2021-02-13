using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.ProBuilder.MeshOperations;

public enum OptionType
{
    language,
    volume,
    sensibility
}
public class MenuOptionsController : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int currentPosition;
    public OptionType type;
    public List<string> posibilities = new List<string>();

    public Slider gamePadSlider;
    public Slider mouseSlider;
    public Slider volumeSlider;

    SaveManager mySaveManager;

    StatsData myData;

    private void Start()
    {
        mySaveManager = FindObjectOfType<SaveManager>();

        if (type == OptionType.language)
        {
            text = this.GetComponent<TextMeshProUGUI>();
            currentPosition = PlayerPrefs.GetInt("language");
            text.text = posibilities[currentPosition];
        }

        else if (type == OptionType.sensibility)
        {
            currentPosition = (int)PlayerPrefs.GetFloat("sensibility");
            if (currentPosition == 0)
            {
                currentPosition = 100;
            }

            this.GetComponent<Slider>().value = currentPosition;
        }
    }

    StatsData loadStatsObject()
    {
        return mySaveManager.loadStats();
    }

    void saveStatsObject(StatsData data)
    {
        mySaveManager.saveStats(myData);
    }

    public void loadSettingsValues()
    {
        myData = loadStatsObject();

        gamePadSlider.value = myData.gamePadSens;
        mouseSlider.value = myData.mouseSens;
        volumeSlider.value = myData.volume;

    }

    public void saveSettingsValues()
    {
        saveStatsObject(myData);
    }

    public void Next()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.pause)
        {
            if (type == OptionType.language)
            {
                currentPosition = ManagePosition(true);
                text.text = posibilities[currentPosition];
            }
        }
    }

    public void Previous()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.pause)
        {
            if (type == OptionType.language)
            {
                currentPosition = ManagePosition(false);
                text.text = posibilities[currentPosition];
            }
        }
    }

    public int ManagePosition(bool direction)
    {
        if (currentPosition == (posibilities.Count - 1))
        {
            if (direction)
            {
                currentPosition = 0;
            }
            else
            {
                currentPosition--;
            }
        }
        else if (currentPosition == 0)
        {
            if (direction)
            {
                currentPosition++;
            }
            else
            {
                currentPosition = (posibilities.Count - 1);
            }
        }
        else
        {
            if (direction)
            {
                currentPosition++;
            }
            else
            {
                currentPosition--;
            }
        }

        if (type == OptionType.language)
        {
            PlayerPrefs.SetInt("language", currentPosition);
        }
        return currentPosition;
    }

    //Change Sensibility
    public void setGamepadSens(float newSensibility)
    {
        if (myData != null)
            myData.gamePadSens = newSensibility;
        else
            Debug.LogWarning("No data is loaded!");
    }

    public void setMouseSens(float newSensibility)
    {
        if (myData != null)
            myData.mouseSens = newSensibility;
        else
            Debug.LogWarning("No data is loaded!");
    }

    public void setVolume(float newVolume)
    {
        if (myData != null)
        {
            myData.volume = newVolume;
        }

        else Debug.LogWarning("No data is loaded");



    }

    public float getGamePadSens()
    {
        if (myData != null)
            return myData.gamePadSens;
        else
            return 0;
    }

    public float getMouseSens()
    {
        if (myData != null)
            return myData.mouseSens;
        else
            return 0;
    }


}
