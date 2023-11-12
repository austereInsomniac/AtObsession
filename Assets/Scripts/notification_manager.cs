using UnityEngine;
using TMPro;

// Joe + Mackenzie

public class notification_manager : MonoBehaviour
{
    private bool isNotificationShowing;
    private float displayTime = 2.0f;
    private float displayStartTime;
    private TMP_Text mText;

    void Start()
    {
        mText = GetComponentInChildren<TextMeshProUGUI>().GetComponent<TMP_Text>();
        GameObject.Find("Player").GetComponent<game_state>().addOnWellnessChange(showWellnessNotification);
    }

    public void repeatNotification()
    {
        // Set the notification message
        isNotificationShowing = true;
        mText.enabled = true;

        // Record start time
        displayStartTime = Time.timeSinceLevelLoad;
    }

    public void showNotification(string message)
    {
        // Set the notification message
        mText.SetText(message);
        isNotificationShowing = true;
        mText.enabled = true;

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

        // Record start time
        displayStartTime = Time.timeSinceLevelLoad;
    }

    public void disableNotification()
    {
        isNotificationShowing = false;
        mText.enabled = false;
    }

    private void Update()
    {
        if (isNotificationShowing && Time.timeSinceLevelLoad >= displayTime + displayStartTime)
        {
            disableNotification();
        }
    }
}

