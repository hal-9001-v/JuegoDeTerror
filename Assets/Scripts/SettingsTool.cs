using UnityEngine;
using UnityEngine.UI;

public class SettingsTool : MonoBehaviour
{

    public Slider gamePadSlider;
    public Slider mouseSlider;
    public Slider volumeSlider;

    StatsData myData;

    SaveManager mySaveManager;

    private void Start()
    {
        mySaveManager = FindObjectOfType<SaveManager>();
    }

    StatsData loadStatsObject()
    {
        return mySaveManager.loadStats();
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
        if (myData != null) {
            mySaveManager.saveStats(myData);

        }
            
        else
            Debug.LogWarning("Stats Data has not been loaded!");
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
