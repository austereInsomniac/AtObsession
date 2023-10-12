using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics.CodeAnalysis;

public class make_video_get_subscriber : MonoBehaviour
{

    [SerializeField]
    private int subscribers = 1000;
    [SerializeField]
    public int upgradeLevel = 1;
    [SerializeField]
    private double subscriberIncreasePercentage = 0;
    [SerializeField]
    private int reputation = 20;

    GameObject player;
    void Awake()
    {
        player = GameObject.Find("Player");
    }
    public void makeVideoGetSubscriber(int starCount)
    {
        //increase subscribers by: (3*starCount + reputation)/5 % to (3*starCount + reputation+10)/5%
        //10% of new sub as money
        // -3 Wellness per hour
        int subsTemp = subscribers;

        switch (upgradeLevel)
        {
            case 1:
                subscriberIncreasePercentage = 0.1;
               break;
            case 2:
                subscriberIncreasePercentage = 0.2;
                break;
            case 3:
                subscriberIncreasePercentage = 0.3;
                break;
            case 4:
                subscriberIncreasePercentage = 0.4;
                break;
            case 5:
                subscriberIncreasePercentage = 0.5;
                break;
        }
        int r = Random.Range(0, 10);
        int newSubscribers = (int)(3*starCount) + (reputation);


        newSubscribers = (int)newSubscribers/5;

        int changeInSubscribers = (int)(subscribers * subscriberIncreasePercentage);
        newSubscribers += changeInSubscribers;

        subscribers += newSubscribers;

        int money = subscribers ;
        //Debug.Log(money);
        money = (int)(money * .10);
        //Debug.Log(money);
        player.GetComponent<game_state>().updateSubscribers(newSubscribers);
        player.GetComponent<game_state>().updateMoney(money);
        player.GetComponent<game_state>().updateReputation(3);

        //Debug.Log("Subscribers: " + player.GetComponent<game_state>().getSubscribers());
        //Debug.Log("Gained Subscribers: " + (newSubscribers));
        //Debug.Log("Money: " + player.GetComponent<game_state>().getMoney());
        //Debug.Log("Reputation: " + player.GetComponent<game_state>().getReputation());

    }
    public int getSubscribers()
    {
        return subscribers;
    }

    public int getUpgradeLevel()
    {
        return upgradeLevel;
    }
    public int getReputation()
    {
        return reputation;
    }
    public double getSubscriberIncreasePercentage()
    {
        return subscriberIncreasePercentage;
    }

    public void upgradeComputer()
    {
        upgradeLevel++;
    }
    public void setUpgradeLevel(int upgradedLevel)
    {
        upgradeLevel = upgradedLevel;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
