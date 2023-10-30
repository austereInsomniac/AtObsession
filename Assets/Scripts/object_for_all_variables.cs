using System.Collections.Generic;
using UnityEngine;
using System.Collections;
//using System.Random;
using UnityEngine.XR;
using JetBrains.Annotations;
using TMPro;

class ActionVariables
{
    int wellness = 0;
    int time = 0;
    double money = 0.00;
    bool oncePerDay = true;

    public ActionVariables(int changeWellness, int changeTime, double changeMoney, bool oncePerDay_)
    {
        wellness = changeWellness;
        time = changeTime;
        money = changeMoney;
        oncePerDay = oncePerDay_;

    }


    public bool getOncePerDay()
    {
        return oncePerDay;
    }

    public void setOncePerDay(bool setTrue)
    {
         oncePerDay = setTrue;
    }
    public int getWellness()
    {
        return wellness;
    }

    public int getTime() { return time; }

    public double getMoney()
    {
        return money;
    }

    public void Hunger()
    {

    }

    public void Time(int oldTime, int newTime)
    {
        time = newTime + oldTime;
    }
}

public class object_for_all_variables : MonoBehaviour
{

    [SerializeField]
    private string key;

    List<ActionVariables> listVariables;
    Dictionary<string, ActionVariables> activities;

    ActionVariables action;

    GameObject player;

    System.Random rand = new System.Random();

    //int SkipToMorning(int currentTime)
    //{
    //    currentTime = action.getTime();

    //}
    int RandomTimeBig()
    {
        int randomNumber;

        randomNumber = rand.Next(60 / 5, 120 / 5);
        randomNumber *= 5;
        return randomNumber;
    }

    int RandomTimeSmall()
    {
        int randomNumber;

        randomNumber = rand.Next(30 / 5, 60 / 5);
        randomNumber *= 5;
        return randomNumber;
    }

    int RandonmWellness()
    {
        int randomNumber;
        randomNumber = rand.Next(1, 2);
        if (randomNumber == 1)
        {
            return -15;
        }
        if (randomNumber == 2) {
            return 15;
        }
        else {
            return 0;
        }
    }
    void Awake()
    {
        // "Player" is the name of the Game Object with the game_state script
        player = GameObject.Find("Player");
    }

    public void doAction()
    {
        // update each statistic
        // game_state method = player.GetComponent<game_state>();
            ActionVariables activity = activities[key];
        if (activity.getOncePerDay() == false)
        {
            player.GetComponent<game_state>().updateWellness(activity.getWellness());
            player.GetComponent<game_state>().updateTime(activity.getTime());
            player.GetComponent<game_state>().updateMoney(activity.getMoney());
            if (key == "Go to sleep" || key == "Forced sleep" || key == "Freshen up" || key == "Take a shower" || key == "Bubble bath")
            {
                activity.setOncePerDay(true);
                if (key == "Freshen up" || key == "Take a shower" || key == "Bubble bath")
                {
                   
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        activities = new Dictionary<string, ActionVariables>();
        activities.Add("Cook food", new ActionVariables(10, 30, 5.00, false));//hunger
        activities.Add("Eat at a restaurant", new ActionVariables(10, 60, 25.00, false));//hunger
        activities.Add("Eat a snack", new ActionVariables(10, 5, 0.00, false));//hunger
       // activities.Add("Do household chores", new ActionVariables(8, 15, 0.00));
        activities.Add("Go to sleep", new ActionVariables(30, 480 /*- action.getTime()*/, 0.00, false));//use the equation to adjust the wellness 
        activities.Add("Take a nap", new ActionVariables(20, 120, 0.00, false));
        activities.Add("Forced sleep", new ActionVariables(-5, 480 - 120, 0.00, false));
        activities.Add("Freshen up", new ActionVariables(3, 5, 0.00, false));
        activities.Add("Take a shower", new ActionVariables(8, 20, 0.00, false));
        activities.Add("Bubble bath", new ActionVariables(12, 45, 0.00, false));
        activities.Add("Do household chores", new ActionVariables(8, 15, 0.00, false));
        activities.Add("Exercise at the gym", new ActionVariables(8, RandomTimeBig(), 15.00, false));
        activities.Add("Hang out with friends", new ActionVariables(RandonmWellness(), RandomTimeBig(), 0.00, false));
        activities.Add("Go for a walk", new ActionVariables(10, 25, 0, false));
        activities.Add("Watch TV", new ActionVariables(8, RandomTimeSmall(), 0.00, false));
        activities.Add("Exercise at home", new ActionVariables(8, 20, 0.00, false));
    }
}
