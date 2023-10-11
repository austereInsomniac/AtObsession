using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class force_sleep : MonoBehaviour

{
    [SerializeField]
    private int time;
    [SerializeField]
    private int wellness;

    GameObject player;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    public void forceSleep(int oldTime, int newTime)
    {
        Debug.Log(newTime);
        if (oldTime == 240)
        {
            Debug.Log(oldTime);
            wellness -= 5;
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
