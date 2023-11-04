using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class notification_manager : MonoBehaviour
{
    public GameObject notificationManager;
    private bool isNotificationShowing;
    private float displayTime = 3.0f;
    private float displayStartTime;

    void Awake()
    {
        notificationManager.SetActive(false);
    }

    public void ShowNotifications(string message)
    {
        // Show the notification
        notificationManager.SetActive(true);

        // Set the notification message
        TMP_Text mText = notificationManager.GetComponent<TMP_Text>();
        mText.SetText(message);

        // Record start time
        displayStartTime = Time.timeSinceLevelLoad;
        isNotificationShowing = true;
    }

    public void disableNotification()
    {
        notificationManager.SetActive(false);
        isNotificationShowing = false;
    }

    void Update()
    {
        if (isNotificationShowing && Time.timeSinceLevelLoad >= displayTime + displayStartTime)
        {
            disableNotification();
        }
    }
}

