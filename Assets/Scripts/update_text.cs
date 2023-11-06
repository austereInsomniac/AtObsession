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
    TMP_Text moneyText;
    TMP_Text repText;
    TMP_Text subText;

    void Awake() 
    {
        // grab text objects
        dayText = GameObject.Find("Day Text").GetComponent<TMP_Text>();
        timeText = GameObject.Find("Time Text").GetComponent<TMP_Text>();
        moneyText = GameObject.Find("Money Text").GetComponent<TMP_Text>();

        // add delegates
        GetComponent<game_state>().addOnTimeChange(updateTimeText);
        GetComponent<game_state>().addOnMoneyChange(updateMoneyText);

        // dev text
        if (GameObject.Find("Wellness Text") != null)
        {
            wellnessText = GameObject.Find("Wellness Text").GetComponent<TMP_Text>();
            repText = GameObject.Find("Reputation Text").GetComponent<TMP_Text>();
            subText = GameObject.Find("Subscribers Text").GetComponent<TMP_Text>();
        }
    }

    private void Start()
    {
        // run all displays immediately
        updateTimeText(GetComponent<game_state>().getTime(), GetComponent<game_state>().getTime());
        updateMoneyText(GetComponent<game_state>().getMoney(), GetComponent<game_state>().getMoney());
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

    void updateMoneyText(double oldM, double newM)
    {
        string updateText = "Money: " + GetComponent<game_state>().getMoney();
        moneyText.SetText(updateText);
    }

    // temp displays
    private void Update()
    {
        if (GameObject.Find("Wellness Text") != null)
        {
            string updateText = "Rep: " + GetComponent<game_state>().getReputation();
            repText.SetText(updateText);

            updateText = "Subs: " + GetComponent<game_state>().getSubscribers();
            subText.SetText(updateText);

            updateText = "Wellness: " + GetComponent<game_state>().getWellness();
            wellnessText.SetText(updateText);
        }
    }
}
