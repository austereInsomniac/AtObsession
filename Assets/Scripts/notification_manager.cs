using UnityEngine;
using TMPro;

using System.Collections.Generic;

// Joe + Mackenzie

public class notification_manager : MonoBehaviour
{
    private bool isNotificationShowing;
    private float displayTime = 0.1f;
    private float displayStartTime;

    private TMP_Text mText;
    private UnityEngine.UI.Image mImage;

    private Queue<string> notificationQueue;

    private UnityEngine.UI.Image menuBlocker;
    private BoxCollider2D menuCollider;

    private CanvasGroup hudCanvas;

    void Start()
    {
        mText = GetComponentInChildren<TextMeshProUGUI>().GetComponent<TMP_Text>();
        mImage = GetComponent<UnityEngine.UI.Image>();
        mImage.enabled = false;
        notificationQueue = new Queue<string>();

        menuBlocker = GameObject.Find("Menu Click Blocker").GetComponent<UnityEngine.UI.Image>();
        menuCollider = GameObject.Find("Menu Click Blocker").GetComponent<BoxCollider2D>();
        hudCanvas = GameObject.Find("Menu Click Blocker").GetComponent<CanvasGroup>();
    }

    public void repeatNotification()
    {
        // Set the notification message
        isNotificationShowing = true;
        mText.enabled = true;
        mImage.enabled = true;

        // Record start time
        displayStartTime = Time.timeSinceLevelLoad;

        // block menus
        menuBlocker.enabled = true;
        menuCollider.enabled = true;
        hudCanvas.blocksRaycasts = true;
    }

    public void showNotification(string message)
    {
        // Set the notification message
        mText.SetText(message);

        repeatNotification();
    }

    public void queNotification(string message)
    {
        notificationQueue.Enqueue(message);
    }

    public void showWellnessNotification(string action, int newW)
    {
        string text = action;

        // Set the notification message
        if (newW <= 20)
        {
            text += "\nYou feel really dizzy...";
        }
        else if(newW <= 40)
        {
            text += "\nYou don't feel very good";
        }
        else if(newW <= 60)
        {
            text += "\nYou feel normal";
        }
        else if (newW <= 80)
        {
            text += "\nYou feel excited.";
        }
        else
        {
            text += "\nYou feel amazing!";
        }

        showNotification(text);
    }

    public void disableNotification()
    {
        isNotificationShowing = false;
        mText.enabled = false;
        mImage.enabled = false;

        menuBlocker.enabled = false;
        menuCollider.enabled = false;
        hudCanvas.blocksRaycasts = false;
    }

    private void Update()
    {
        if (isNotificationShowing && Time.timeSinceLevelLoad >= displayTime + displayStartTime)
        {
            // close the notification after .5 seconds if any key or mouse button is pressed
            if (Input.anyKeyDown)
            {
                disableNotification();

                if(notificationQueue.Count > 0)
                {
                    showNotification(notificationQueue.Dequeue());
                }
            }
        }
    }
}

