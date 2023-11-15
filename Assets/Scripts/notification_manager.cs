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

        GameObject.Find("Player").GetComponent<game_state>().addOnWellnessChange(showWellnessNotification);
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
        isNotificationShowing = true;

        mText.enabled = true;
        mImage.enabled = true;

        // Record start time
        displayStartTime = Time.timeSinceLevelLoad;
    }

    public void showWellnessNotification(int oldW, int newW)
    {
        // Set the notification message
        if (newW - oldW >= 20)
        {
            mText.SetText("You feel a lot better than before!");
        }
        else if(newW - oldW >= 0)
        {
            mText.SetText("You feel better than before!");
        }
        else if(newW - oldW == 0)
        {
            mText.SetText("");
        }
        else if (newW - oldW <= -20)
        {
            mText.SetText("You feel terrible after that");
        }
        else
        {
            mText.SetText("You feel worse than before");
        }

        // Set the notification message
        isNotificationShowing = true;
        mText.enabled = true;
        mImage.enabled = true;

        // Record start time
        displayStartTime = Time.timeSinceLevelLoad;
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

