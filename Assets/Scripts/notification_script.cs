using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class notification_script : MonoBehaviour
{
    public GameObject notificationManager;
    public Transform notificationParent;
    private List<notification_info> notifications = new List<notification_info> ();
    GameObject player;
    
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    public void ShowNotifications(string message, float displayTime)
    {
        GameObject notification = Instantiate(notificationManager, notificationParent);
        notification.SetActive(true);

        // Attach a script to the notification to track its display start time
        notification_info notificationInfo = notification.AddComponent<notification_info>();
        notificationInfo.displayStartTime = player.GetComponent<game_state>().getTime();
        notificationInfo.displayTime = displayTime;
        notificationInfo.Notification = notification;

        // Set the notification message
        TMP_Text mText = notification.GetComponent<TMP_Text>();
        mText.SetText(message);

        // Add the notification to the list
        notifications.Add(notificationInfo);
    }
     void Update()
    {
        // Check for notifications that have exceeded their display time.
        for (int i = notifications.Count - 1; i >= 0; i--)
        {
            notification_info notificationInfo = notifications[i];
            if (Time.time + notificationInfo.displayStartTime >= notificationInfo.displayStartTime + notificationInfo.displayTime)
            {
                // Hide the notification GameObject and remove from the list
                Debug.Log(notificationInfo.Notification);
                notificationInfo.Notification.SetActive(false);
                notifications.RemoveAt(i);
            }
        }
    }
}

