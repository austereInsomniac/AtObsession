using System.Collections.Generic;
using UnityEngine;

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
    // store the activities
    Dictionary<string, ActionVariables> activities;

    // outside objects
    private game_state state;

    // RNG
    System.Random rand = new System.Random();

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
        // assign outside objects
        state = GetComponent<game_state>();
    }

    public void doAction(string key)
    {
        // update each statistic
        ActionVariables activity = activities[key];
        state.updateWellness(activity.getWellness());
        state.updateTime(activity.getTime());
        state.updateMoney(activity.getMoney());

        // update the splash screen
        GetComponent<splash_screen_manager>().openSplashScreen(key);
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
    }
}
