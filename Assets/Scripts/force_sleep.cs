using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class force_sleep : MonoBehaviour

{
    GameObject player; //Creates an unassigned GameObject

    void Awake()
    {
        player = GameObject.Find("Player");//assigns game object to the player
    }

    //Forced sleep method takes in the original time and the new time of an action
    public void forceSleep(int oldTime, int newTime)
    {
        //If the time when the activity is run is between 4 and 8 am then advance the day to make the sleep
        if ((oldTime >= 240 && oldTime<=480) ||(oldTime >=1680 && oldTime <=1920)) 
        {
            player.GetComponent<game_state>().advanceDay(); //Runs the advance day method
            player.GetComponent<game_state>().updateWellness(-5); // Lowers your wellness
            //Move to the bedroom
        }
    }
}
