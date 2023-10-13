using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics.CodeAnalysis;

public class make_video_get_subscriber : MonoBehaviour
{

    [SerializeField]
    private int subscribers = 500;
    [SerializeField]
    public int upgradeLevel;
    [SerializeField]
    private double subscriberIncreasePercentage = 0;
    [SerializeField]
    private int reputation = 50;

    public void makeVideoGetSubscriber()
    {

        switch (upgradeLevel)
        {
            case 1:
                subscriberIncreasePercentage = 0.1;
                reputation += (int)(reputation * 0.05); break;
            case 2:
                subscriberIncreasePercentage = 0.2;
                reputation += (int)(reputation * 0.05); break;
            case 3:
                subscriberIncreasePercentage = 0.3;
                reputation += (int)(reputation * 0.05); break;
            case 4:
                subscriberIncreasePercentage = 0.4;
                reputation += (int)(reputation * 0.05); break;
            case 5:
                subscriberIncreasePercentage = 0.5;
                reputation += (int)(reputation * 0.05); break;
            default:
                subscriberIncreasePercentage = 0.6;
                reputation += (int)(reputation * 0.05); break;
        }
        Debug.Log("Gained Subscribers: " + (int)(subscribers * subscriberIncreasePercentage));
        subscribers += (int)(subscribers * subscriberIncreasePercentage);
        Debug.Log("Current Subscribers: " + subscribers + "\nNew Reuptation: " + getReputation());
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
