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

        if (notification == null)
        {
            Debug.LogError("Script Not Found.");
        }
    }

    public void doAnAction()
    {
        // update each statistic
        player.GetComponent<game_state>().setWellness(player.GetComponent<game_state>().getWellness() + changeWellness);
        player.GetComponent<game_state>().setTime(player.GetComponent<game_state>().getTime() + changeTime);
        player.GetComponent<game_state>().setReputation(player.GetComponent<game_state>().getReputation() + changeRep);
        player.GetComponent<game_state>().setSubscribers(player.GetComponent<game_state>().getSubscribers() + changeSubs);
        player.GetComponent<game_state>().setMoney(player.GetComponent<game_state>().getMoney() + changeMoney);
        player.GetComponent<game_state>().setEnding(player.GetComponent<game_state>().getEnding() + changeEnd);
    }

    public void ShowNotificationOnClick()
    {
       if (notification != null)
        {
            notification.ShowNotifications(notificationMessage);
        }
    }
}
