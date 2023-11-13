using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics.CodeAnalysis;

public class video_making : MonoBehaviour
{

    [SerializeField]
    private int subscribers; // Creates a local instance of the subscribers variable
    [SerializeField]
    private int reputation; // Creates a local Instance of the reputation varaible

    GameObject player;
    void Awake()
    {
        player = GameObject.Find("Player"); //Identifies the player object
    }

    //Does all the math for making a video using starCount as hours
    public void makeVideo(int starCount)
    {
        subscribers = player.GetComponent<game_state>().getSubscribers(); //Sets the local subscribers to be the amount in the game state
        reputation = player.GetComponent<game_state>().getReputation(); //Sets the local reputation to be the amount in the game state

        //increase subscribers by: (3*starCount + reputation)/5 % to (3*starCount + reputation+10)/5%
        int r = Random.Range(0, 10);
        int newSubscribers = (3*starCount) + (reputation +r);
        newSubscribers = (int)newSubscribers/5;

        subscribers += newSubscribers; //Update to the new number of subscribers

        int randomMoney = Random.Range(-5, 20);
        int money = (int)(subscribers * .02) + randomMoney; //Assigns a money variable to be 10% of the new subscriber count

        player.GetComponent<game_state>().updateSubscribers(newSubscribers); //Changes the game state subscribers value
        player.GetComponent<game_state>().updateMoney(money); //Changes the game state money value to be updated
        player.GetComponent<game_state>().updateReputation(3); //Changes reputation by 3 each video
        player.GetComponent<game_state>().updateTime((starCount * 60)); //changes time by star count(amount of hours) * 60 minutes
        player.GetComponent<game_state>().updateWellness(-(starCount * 3)); // -3 Wellness per hour



        /*
         * MinimumIncrease
         * 
         * 
         * 
         * 
         */

        //Debug.Log("Current Hour" + (player.GetComponent<game_state>().getTime()/60));
        //Debug.Log("Subscribers: " + player.GetComponent<game_state>().getSubscribers());
        //Debug.Log("Gained Subscribers: " + (newSubscribers));
        //Debug.Log("Money: " + player.GetComponent<game_state>().getMoney());
        //Debug.Log("Reputation: " + player.GetComponent<game_state>().getReputation());

    }
    public int getSubscribers()
    {
        return subscribers;
    }

    public int getReputation()
    {
        return reputation;
    }
}
