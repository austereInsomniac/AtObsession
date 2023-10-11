using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class do_an_action_script : MonoBehaviour
{
    public string notificationMessage = "Your wellness decreased!";

    public notification_script notification;

    // increase in each statistic
    [SerializeField]
    private int changeWellness;
    [SerializeField]
    private int changeTime;
    [SerializeField]
    private int changeRep;
    [SerializeField]
    private int changeSubs;
    [SerializeField]
    private int changeEnd;
    [SerializeField]
    private double changeMoney;

    GameObject player;

    void Awake()
    {
        // "Player" is the name of the Game Object with the game_state script
        player = GameObject.Find("Player");

        notification = FindObjectOfType<notification_script>();

        changeTime = 60;
        player.GetComponent<game_state>().addOnTimeChange(player.GetComponent<force_sleep>().forceSleep);
    }

    public void doAnAction()
    {
        // update each statistic
        player.GetComponent<game_state>().updateWellness(changeWellness);
        player.GetComponent<game_state>().updateTime(changeTime);
        player.GetComponent<game_state>().updateReputation(changeRep);
        player.GetComponent<game_state>().updateSubscribers(changeSubs);
        player.GetComponent<game_state>().updateMoney(changeMoney);
        player.GetComponent<game_state>().updateEnding(changeEnd);
    }

    public void ShowNotificationOnClick()
    {
       if (notification != null)
        {
            notification.ShowNotifications(notificationMessage);
        }
    }
}
