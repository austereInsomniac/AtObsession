using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics.CodeAnalysis;

public class video_making : MonoBehaviour
{
    private int subscribers; // Creates a local instance of the subscribers variable
    private int reputation; // Creates a local Instance of the reputation varaible

    game_state player;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<game_state>(); //Identifies the player object
    }

    //Does all the math for making a video using starCount as hours
    public void makeVideo(int starCount)
    {
        player.makeVideo();

        subscribers = player.getSubscribers(); //Sets the local subscribers to be the amount in the game state
        reputation = player.getReputation(); //Sets the local reputation to be the amount in the game state

        //increase subscribers by: (3*starCount + reputation)/5 % to (3*starCount + reputation+10)/5%
        int r = Random.Range(0, 10);
        int newSubscribers = (3*starCount) + (reputation +r);
        newSubscribers = (int)newSubscribers/5;

        subscribers += newSubscribers; //Update to the new number of subscribers

        player.GetComponent<game_state>().updateSubscribers(newSubscribers); //Changes the game state subscribers value
        player.GetComponent<game_state>().updateMoney((int)(subscribers * .02) + Random.Range(-5, 20)); //Assigns a money variable to be 10% of the new subscriber count
        player.GetComponent<game_state>().updateReputation(3*starCount); //Changes reputation by 3 each video
        player.GetComponent<game_state>().updateTime((starCount * 60)); //changes time by star count(amount of half hours) * 60 minutes
        player.GetComponent<game_state>().updateWellness(-(starCount * 3)); // -3 Wellness per hour



        //Debug.Log("Current Hour" + (player.GetComponent<game_state>().getTime()/60));
        //Debug.Log("Subscribers: " + player.GetComponent<game_state>().getSubscribers());
        //Debug.Log("Gained Subscribers: " + (newSubscribers));
        //Debug.Log("Money: " + player.GetComponent<game_state>().getMoney());
        //Debug.Log("Reputation: " + player.GetComponent<game_state>().getReputation());

    }
}
