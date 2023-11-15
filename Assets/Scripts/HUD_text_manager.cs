using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

// Mackenzie

public class HUD_text_manager : MonoBehaviour
{
    // get text boxes
    TMP_Text dayText;
    TMP_Text timeText;
    TMP_Text moneyText;

    // Developer Mode only
    TMP_Text wellnessText;
    TMP_Text repText;
    TMP_Text subText;

    private void Start()
    {
        // grab text objects
        dayText = GameObject.Find("Day Text").GetComponent<TMP_Text>();
        timeText = GameObject.Find("Time Text").GetComponent<TMP_Text>();
        moneyText = GameObject.Find("Money Text").GetComponent<TMP_Text>();

        // add delegates
        GetComponent<game_state>().addOnTimeChange(updateTimeText);
        GetComponent<game_state>().addOnMoneyChange(updateMoneyText);

        // dev mode text
        if (GameObject.Find("subscribers Text") != null)
        {
            subText = GameObject.Find("Subscribers Text").GetComponent<TMP_Text>();
        }

        // run all displays immediately
        updateTimeText(GetComponent<game_state>().getTime(), GetComponent<game_state>().getTime());
        updateMoneyText(GetComponent<game_state>().getMoney(), GetComponent<game_state>().getMoney());
    }

    void updateTimeText(int oldTime, int newTime)
    {
        // update the time text
        DateTime d = new DateTime(1, 1, 1, newTime / 60, newTime % 60, 0);
        string updateText = d.ToString("hh:mm tt"); // add tt to the end of the quotes to add AM/PM
        timeText.SetText(updateText);

        // update the day text
        updateText = "" + GetComponent<game_state>().getDay();
        dayText.SetText(updateText);
    }

    void updateMoneyText(double oldM, double newM)
    {
        string updateText = "" + GetComponent<game_state>().getMoney();
        moneyText.SetText(updateText);
    }

    // temp displays
    private void Update()
    {
        if (GameObject.Find("subscribers Text") != null)
        {
            string updateText = "Subs: " + GetComponent<game_state>().getSubscribers();
            subText.SetText(updateText);
        }
    }
}
