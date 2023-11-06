using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.VersionControl;

public class notification_manager : MonoBehaviour
{
    public TextMeshProUGUI notification;
    private bool isNotificationShowing;
    private float displayTime = 3.0f;
    private float displayStartTime;

    void Awake()
    {
        notification = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowNotifications(string message)
    {
        // Set the notification message
        TMP_Text mText = notification.GetComponent<TMP_Text>();
        mText.SetText(message);

        // Record start time
        displayStartTime = Time.timeSinceLevelLoad;
        isNotificationShowing = true;
    }

    public void disableNotification()
    {
        TMP_Text mText = notification.GetComponent<TMP_Text>();
        mText.SetText("");
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

