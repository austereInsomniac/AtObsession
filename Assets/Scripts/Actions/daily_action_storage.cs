using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.XR;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UI;

class ActionVariables
{
    int wellness = 0;
    int time = 0;
    double money = 0.00;

    public ActionVariables(int changeWellness, int changeTime, double changeMoney)
    {
        wellness = changeWellness;
        time = changeTime;
        money = changeMoney;

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

    }
}

public class daily_action_storage : MonoBehaviour
{
    List<ActionVariables> listVariables;
    Dictionary<string, ActionVariables> activities;
    Dictionary<string, UnityEngine.UI.Image> splashScreens;

    ActionVariables action;

    game_state player;
    UnityEngine.UI.Image splashScreen;

    System.Random rand = new System.Random();

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
        ActionVariables activity = activities[key];
        player.updateWellness(activity.getWellness());
        player.updateTime(activity.getTime());
        player.updateMoney(activity.getMoney());

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
        activities = new Dictionary<string, ActionVariables>
        {
            // living room
            { "Do chores", new ActionVariables(8, 15, 0.00) },
            { "Go to the gym", new ActionVariables(8, RandomTimeBig(), 15.00) },
            { "Visit friends", new ActionVariables(RandomWellness(), RandomTimeBig(), 0.00) },
            { "Go for a walk", new ActionVariables(10, 25, 0) },
            { "Watch TV", new ActionVariables(8, RandomTimeSmall(), 0.00) },
            { "Lift weights", new ActionVariables(8, 20, 0.00) },
            { "Eat at a restaurant", new ActionVariables(10, 60, 25.00) },//hunger

            // kitchen
            { "Cook food", new ActionVariables(10, 30, 5.00) },//hunger
            { "Eat a snack", new ActionVariables(10, 5, 0.00) },//hunger

            // bedroom
            { "Go to sleep", new ActionVariables(30, 480 /*- action.getTime()*/, 0.00) },//use the equation to adjust the wellness 
            { "Take a nap", new ActionVariables(20, 120, 0.00) },

            // bathroom
            { "Freshen up", new ActionVariables(3, 5, 0.00) },
            { "Shower", new ActionVariables(8, 20, 0.00) },
            { "Bubble bath", new ActionVariables(12, 45, 0.00) }
        };

        Debug.Log("test");

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
