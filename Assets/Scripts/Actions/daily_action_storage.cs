using System.Collections.Generic;
using UnityEngine;
using System.Collections;
//using System.Random;
using UnityEngine.XR;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using System.Text.RegularExpressions;

using UnityEditor;
using UnityEngine.UI;

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

    public void Time(int oldTime, int newTime)
    {
        time = newTime + oldTime;
    }
}

public class daily_action_storage : MonoBehaviour
{

    [SerializeField]
    private string key;

    Dictionary<string, int> timesPerDay;

    List<ActionVariables> listVariables;

    Dictionary<string, ActionVariables> activities;
    Dictionary<string, UnityEngine.UI.Image> splashScreens;

    ActionVariables action;

    game_state player;
    UnityEngine.UI.Image splashScreen;

    System.Random rand = new System.Random();


    //int SkipToMorning(int currentTime)
    //{
    //    currentTime = action.getTime();

    //}


    // splash screen timers
    private bool isSplashShowing;
    private float displayTime = 3.0f;
    private float displayStartTime;


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

    int RandomWellness()
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
        return 0;
    }

    void Awake()
    {
        // "Player" is the name of the Game Object with the game_state script
        player = GameObject.Find("Player").GetComponent<game_state>();

        splashScreen = GameObject.Find("Splash Screen").GetComponent<UnityEngine.UI.Image>();
    }

    public void doAction(string key)
    {
        // update each statistic
        // game_state method = player.GetComponent<game_state>();\
        ActionVariables activity = activities[key];
        if (key == "Cook food" || key == "Eat at restaurant" || key == "Eat a snack")
        {
            player.GetComponent<game_state>().updateHunger(activity.getTime());
        }

            
            player.GetComponent<game_state>().updateWellness(activity.getWellness());
            player.GetComponent<game_state>().updateTime(activity.getTime());
            player.GetComponent<game_state>().updateMoney(activity.getMoney());
        

        // display approprriate splash screen for a set time, then wait for input
        splashScreen = splashScreens[key];
        splashScreen.enabled = true;

        // Record start time
        displayStartTime = Time.timeSinceLevelLoad;
        isSplashShowing = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        activities = new Dictionary<string, ActionVariables>();
        
        //Kitchen activities
        activities.Add("Cook food", new ActionVariables(10, 30, 5.00, "food", 3));//hunger, do 3 times combined with eat at a restaurant
        activities.Add("Eat a snack", new ActionVariables(10, 5, 0.00, "snack", 3));//hunger, do 3 times a day
        //activities.Add("Do disnes", new ActionVariables(8, 15, 0.00, false)); may add this

        //Living room activities
        activities.Add("Do household chores", new ActionVariables(8, 15, 0.00, "chores", 3)); // do 3 times a day
        activities.Add("Watch TV", new ActionVariables(8, RandomTimeSmall(), 0.00, "entertainment", 999)); // unlimited but costs a lot of time
        activities.Add("Exercise at home", new ActionVariables(8, 20, 0.00, "exercise at home", 2)); // 2 times per day

        //Bedroom activities
        activities.Add("Take a nap", new ActionVariables(20, 120, 0.00, "nap", 2));// 2 times per day
        activities.Add("Forced sleep", new ActionVariables(-5, 480 - 120, 0.00, "sleep", 1));//1 time per day
        activities.Add("Go to sleep", new ActionVariables(30, 480 /*- action.getTime()*/, 0.00, "sleep", 1));//use the equation to adjust the wellness, 1 per day 

        //Bathroom activities
        activities.Add("Freshen up", new ActionVariables(3, 5, 0.00, "freshen", 2)); // 2 times a day
        activities.Add("Take a shower", new ActionVariables(8, 20, 0.00, "shower", 1));// 1 per day combined with bubble bath
        activities.Add("Bubble bath", new ActionVariables(12, 45, 0.00, "shower", 1));// 1 per day combined with take a shower

        //Front door activities
        activities.Add("Eat at a restaurant", new ActionVariables(10, 60, 25.00, "food", 3));//hunger, do 3 times combined with cook food
        activities.Add("Exercise at the gym", new ActionVariables(8, RandomTimeBig(), 15.00, "exercise", 1)); // 1 time per day
        activities.Add("Hang out with friends", new ActionVariables(RandomWellness(), RandomTimeBig(), 0.00, "friends", 999)); //unlimited but costs money
        activities.Add("Go for a walk", new ActionVariables(10, 25, 0, "walk", 2)); // 2 times per day

        timesPerDay = new Dictionary<string, int>();

        timesPerDay.Add("food", 0);
        timesPerDay.Add("snack", 0);
        timesPerDay.Add("chores", 0);
        timesPerDay.Add("entertainment", 0);
        timesPerDay.Add("exercise at home", 0);
        timesPerDay.Add("nap", 0);
        timesPerDay.Add("sleep", 0);
        timesPerDay.Add("freshen", 0);
        timesPerDay.Add("shower", 0);
        timesPerDay.Add("exercise", 0);
        timesPerDay.Add("friends", 0);
        timesPerDay.Add("walk", 0);

        splashScreens = new Dictionary<string, UnityEngine.UI.Image>();

        // living room
        splashScreens.Add("Do chores", splashScreen);

        // kitchen


        // bedroom


        // bathroom


        
    }

    void Update()
    {
        if (isSplashShowing && Time.timeSinceLevelLoad >= displayTime + displayStartTime)
        {
            // give option to slose splash

            splashScreen.enabled = false;

            isSplashShowing = false;
        }

    }
}
