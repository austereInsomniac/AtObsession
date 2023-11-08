using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wake_up_notification : MonoBehaviour
{
    public notification_manager notification;
    private int notifNum;
    GameObject player;
    public bool wakeUpTriggered = false;
    private int initialSubs;
    private int currentDay;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        initialSubs = player.GetComponent<game_state>().getSubscribers();
        currentDay = player.GetComponent<game_state>().getDay();
    }

    void WakeUp()
    {
        int wellness = player.GetComponent<game_state>().getWellness();
        int day = player.GetComponent<game_state>().getDay();
        List<string> wellNotifications;
        wellNotifications = new List<string>();
        wellNotifications.Add("You feel great!");
        wellNotifications.Add("Yesterday was a great day!");
        wellNotifications.Add("You don't feel too good!");
        wellNotifications.Add("Yesterday wasn't a very good day!");
        if (wellness >= 60)
        {
            notifNum = Random.Range(0, 1);
        }
        else
        {
            notifNum = Random.Range(2, 3);
        }
        string notifMessage1 = wellNotifications[notifNum];

        int currentSubs = player.GetComponent<game_state>().getSubscribers();
        int oldSubs = initialSubs;
        List<string> subNotifications;
        subNotifications = new List<string>();
        if (currentSubs > oldSubs)
        {
            subNotifications.Add("You gained " + (currentSubs - oldSubs) + " subscribers! Congratulations!");
        }
        else if (currentSubs < oldSubs)
        {
            subNotifications.Add("You lost " + (oldSubs - currentSubs) + " subscribers. Keep working on it!");
        }
        string notifMessage2 = "";
        if (subNotifications.Count > 0)
        {
            notifMessage2 = subNotifications[0];
        }
        // Display both notifications if there is a change in subscribers
        if (notification != null && subNotifications.Count > 0)
        {
            notification.showNotification(notifMessage1 + " " + notifMessage2);
        }
        else if (notification != null)
        {
            notification.showNotification(notifMessage1);
        }
        initialSubs = currentSubs;
        currentDay = day;
    }

    // Update is called once per frame
    void Update()
    {
        int time = player.GetComponent<game_state>().getTime();
        int day = player.GetComponent<game_state>().getDay();
        if (time == 480 && day > currentDay && !wakeUpTriggered)
        {
            wakeUpTriggered = true;
            WakeUp();
        }
    }
}
