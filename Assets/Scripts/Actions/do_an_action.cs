using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class do_an_action_script : MonoBehaviour
{
    public notification_script notification;
    public int notifNum;

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
        //player.GetComponent<make_video_get_subscriber>().makeVideoGetSubscriber(1);
    }

    public void ShowNotificationOnClick()
    {
        List<string> notifications;
        notifications = new List<string>();
        notifications.Add("That didn't make you feel very good");
        notifications.Add("You feel refreshed");
        if (notification != null)
        {
            notification.ShowNotifications(notifications[notifNum]);
        }
    }
}
