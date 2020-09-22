using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    private void Start()
    {

        if (type == OptionType.language)
        {
            text = this.GetComponent<TextMeshProUGUI>();
            currentPosition = PlayerPrefs.GetInt("language");
            text.text = posibilities[currentPosition];
        }
        else if(type == OptionType.sensibility)
        {
            currentPosition = (int)PlayerPrefs.GetFloat("sensibility");
            if(currentPosition == 0)
            {
                currentPosition = 100;
            }

            this.GetComponent<Slider>().value = currentPosition;
        }
    }

    public void Next()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.menu)
        {
            if(type == OptionType.language)
            {
                currentPosition = ManagePosition(true);
                text.text = posibilities[currentPosition];
            }
        }
    }

    public void Previous()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.menu)
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
        if(currentPosition == (posibilities.Count - 1))
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
        else if(currentPosition == 0)
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
    public void SetSensibility(float newSensibility)
    {
        PlayerPrefs.SetFloat("sensibility", newSensibility);
        CameraLook.sharedInstance.mouseSensitivity = newSensibility;
    }
}
