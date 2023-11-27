using UnityEngine;
using TMPro;
using static Codice.Client.Common.WebApi.WebApiEndpoints;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEngine.Device;

// Joe + Mackenzie

public class notification_manager : MonoBehaviour
{
    private bool isNotificationShowing;
    private float displayTime = 0.1f;
    private float displayStartTime;

    private TMP_Text mText;
    private UnityEngine.UI.Image mImage;

    void Start()
    {
        mText = GetComponentInChildren<TextMeshProUGUI>().GetComponent<TMP_Text>();
        mImage = GetComponent<UnityEngine.UI.Image>();
        mImage.enabled = false;
    }

    public void repeatNotification()
    {
        // Set the notification message
        isNotificationShowing = true;
        mText.enabled = true;
        mImage.enabled = true;

        // Record start time
        displayStartTime = Time.timeSinceLevelLoad;
    }

    public void showNotification(string message)
    {
        // Set the notification message
        mText.SetText(message);

        repeatNotification();
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
    }

    private void Update()
    {
        if (isNotificationShowing && Time.timeSinceLevelLoad >= displayTime + displayStartTime)
        {
            // close the notification after .5 seconds if any key or mouse button is held down
            if (Input.anyKey)
            {
                disableNotification();
            }
        }
    }
}

