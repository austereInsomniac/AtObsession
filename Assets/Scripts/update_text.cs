using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class update_text : MonoBehaviour
{
    // get text boxes
    GameObject player;
    TMP_Text dayText;
    TMP_Text timeText;
    TMP_Text wellnessText;

    void Awake() {
        dayText = GameObject.Find("Day Text").GetComponent<TMP_Text>();
        timeText = GameObject.Find("Time Text").GetComponent<TMP_Text>();
        wellnessText = GameObject.Find("Wellness Text").GetComponent<TMP_Text>();

        // add delegates
        GetComponent<game_state>().addOnWellnessChange(updateWellnessText);
        GetComponent<game_state>().addOnTimeChange(updateTimeText);
    }

    void updateWellnessText(int oldW, int newW)
    {
        // update the text
        string updateText = "Wellness: " + GetComponent<game_state>().getWellness();
        wellnessText.SetText(updateText);
    }

    void updateTimeText(int oldTime, int newTime)
    {
        // update the time text
        string updateText = "" + (newTime / 60) + ":" 
                            + (newTime % 60);
        timeText.SetText(updateText);
        // issue: minute ending in 0 will only display one digit

        // update the day text
        updateText = "" + GetComponent<game_state>().getDay();
        dayText.SetText(updateText);
    }

}
