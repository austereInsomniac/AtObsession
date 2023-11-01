using System.Collections.Generic;
using UnityEngine;
using System.Collections;
//using System.Random;
using UnityEngine.XR;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using System.Text.RegularExpressions;

class ActionVariables
{
    int wellness = 0;
    int time = 0;
    double money = 0.00;
    string combinedActivities = "";
    int maxTimesPerDay = 0;

    public ActionVariables(int changeWellness, int changeTime, double changeMoney, string combinedActivities_, int maxTimesPerDay_)
    {
        wellness = changeWellness;
        time = changeTime;
        money = changeMoney;
        combinedActivities = combinedActivities_;
        maxTimesPerDay = maxTimesPerDay_;
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

    Dictionary<string, int> timesPerDay;
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
        // game_state method = player.GetComponent<game_state>();\
        ActionVariables activity = activities[key];
        //if (key == "Cook food" || key == "Eat at restaurant || key == "Eat a snack")
        //{
        //  call method to reset hunger
        //}
            
            player.GetComponent<game_state>().updateWellness(activity.getWellness());
            player.GetComponent<game_state>().updateTime(activity.getTime());
            player.GetComponent<game_state>().updateMoney(activity.getMoney());
        
    }
    // Start is called before the first frame update
    void Start()
    {
        activities = new Dictionary<string, ActionVariables>();
        activities.Add("Cook food", new ActionVariables(10, 30, 5.00, "food", 3));//hunger, do 3 times combined with eat at a restaurant
        activities.Add("Eat at a restaurant", new ActionVariables(10, 60, 25.00, "food", 3));//hunger, do 3 times combined with cook food
        activities.Add("Eat a snack", new ActionVariables(10, 5, 0.00, "snack", 3));//hunger, do 3 times a day
        activities.Add("Go to sleep", new ActionVariables(30, 480 /*- action.getTime()*/, 0.00, "sleep", 1));//use the equation to adjust the wellness, 1 per day 
        activities.Add("Take a nap", new ActionVariables(20, 120, 0.00, "nap", 2));// 2 times per day
        activities.Add("Forced sleep", new ActionVariables(-5, 480 - 120, 0.00, "sleep", 1));//1 time per day
        activities.Add("Freshen up", new ActionVariables(3, 5, 0.00, "freshen", 2)); // 2 times a day
        activities.Add("Take a shower", new ActionVariables(8, 20, 0.00, "shower", 1));// 1 per day combined with bubble bath
        activities.Add("Bubble bath", new ActionVariables(12, 45, 0.00, "shower", 1));// 1 per day combined with take a shower
        activities.Add("Do household chores", new ActionVariables(8, 15, 0.00, "chorse", 3)); // do 3 times a day
        //activities.Add("Do disnes", new ActionVariables(8, 15, 0.00, false)); may add this
        activities.Add("Exercise at the gym", new ActionVariables(8, RandomTimeBig(), 15.00, "exercise", 1)); // 1 time per day
        activities.Add("Hang out with friends", new ActionVariables(RandonmWellness(), RandomTimeBig(), 0.00, "friends", 999)); //unlimited but costs money
        activities.Add("Go for a walk", new ActionVariables(10, 25, 0, "walk", 2)); // 2 times per day
        activities.Add("Watch TV", new ActionVariables(8, RandomTimeSmall(), 0.00, "entertainment", 999)); // unlimited but costs a lot of time
        activities.Add("Exercise at home", new ActionVariables(8, 20, 0.00, "exercise at home", 2)); // 2 times per day

        timesPerDay = new Dictionary<string, int>();
   
    }
}
