using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class force_sleep : MonoBehaviour

{
    GameObject player;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    public void forceSleep(int oldTime, int newTime)
    {
        if ((oldTime >= 240 && oldTime<=480) ||(oldTime >=1680 && oldTime <=1920))
        {
            player.GetComponent<game_state>().advanceDay();
            player.GetComponent<game_state>().updateWellness(-5);
            Debug.Log("New Day!");
            //Move to the bedroom
        }
        //else
        //{
        //    player.GetComponent<game_state>().setTime(newTime);
        //}


        // player.GetComponent<game_state>().notifyOnTimeChange(oldTime, newTime);
    }

    //for testing only
    //public void increaseTime_TESTING()
    //{
    //    if(time == 23)
    //    {
    //        time = 1;
    //    }
    //    else 
    //    { 
    //        time++; 
    //    }

    //}
}
