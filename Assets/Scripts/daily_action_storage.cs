using System.Collections.Generic;
using UnityEngine;

class ActionVariables
{
    int wellness = 0;
    int time = 0;
    double money = 0.00;
    string group;

    public ActionVariables(int changeWellness, int changeTime, double changeMoney, string group_)
    {
        wellness = changeWellness;
        time = changeTime;
        money = changeMoney;
        group = group_;

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

    public string getGroup() { return group; }
    public void Time(int oldTime, int newTime)
    {
        time = oldTime + newTime;
    }
}

public class daily_action_storage : MonoBehaviour
{
    // store the activities
    Dictionary<string, ActionVariables> activities;
    Dictionary<string, int> timesPerDay;
    Dictionary<string, int> maxTimesPerDay;

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

    int getMaxTimesPerDay(string key)
    {
        if (maxTimesPerDay.ContainsKey(key))
        {
            return maxTimesPerDay[key];
        }
        return 999999;
    }

    int getCurrentTimesPerDay(string key)
    {
        if (timesPerDay.ContainsKey(key))
        {
            return timesPerDay[key];
        }
        timesPerDay.Add(key, 0);
        return 0;
    }

   
    void updateTimesPerDay(string key)
    {
        if (getCurrentTimesPerDay(key) < getMaxTimesPerDay(key))
        {
            timesPerDay[key] += 1;
        }
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

        if (getCurrentTimesPerDay(activity.getGroup()) < getMaxTimesPerDay(activity.getGroup()))
        {
            GetComponent<splash_screen_manager>().openSplashScreen(key);
            state.updateWellness(activity.getWellness());
            state.updateTime(activity.getTime());
            state.updateMoney(activity.getMoney());
            updateTimesPerDay(activity.getGroup());
            if (activity.getGroup() == "food" || activity.getGroup() == "snack")
            {
                state.resetHunger();
            }
        }

        // update the splash screen
        /GetComponent<splash_screen_manager>().openSplashScreen(key);
    }

    // Start is called before the first frame update
    void Start()
    {
        activities = new Dictionary<string, ActionVariables>
        {
            // living room
            { "Do chores", new ActionVariables(8, 15, 0.00, "chores") },
            { "Go to the gym", new ActionVariables(8, RandomTimeBig(), 15.00, "exercise") },
            { "Visit friends", new ActionVariables(RandomWellness(), RandomTimeBig(), 0.00, "friends") },
            { "Go for a walk", new ActionVariables(10, 25, 0, "walk") },
            { "Watch TV", new ActionVariables(8, RandomTimeSmall(), 0.00, "entertainment") },
            { "Lift weights", new ActionVariables(8, 20, 0.00, "exercise at home") },
            { "Eat at a restaurant", new ActionVariables(10, 60, 25.00, "food") },//hunger

            // kitchen
            { "Cook food", new ActionVariables(10, 30, 5.00, "food") },//hunger
            { "Eat a snack", new ActionVariables(10, 5, 0.00, "snack") },//hunger

            // bedroom
            { "Go to sleep", new ActionVariables(30, 480 /*- action.getTime()*/, 0.00, "sleep") },//use the equation to adjust the wellness 
            { "Take a nap", new ActionVariables(20, 120, 0.00, "nap") },

            // bathroom
            { "Freshen up", new ActionVariables(3, 5, 0.00, "freshen") },
            { "Shower", new ActionVariables(8, 20, 0.00, "shower") },
            { "Bubble bath", new ActionVariables(12, 45, 0.00, "shower") }

        };

        // set up time limits
        timesPerDay = new Dictionary<string, int>();
        maxTimesPerDay = new Dictionary<string, int>();

        maxTimesPerDay.Add("food", 3);
        maxTimesPerDay.Add("snack", 5);
        maxTimesPerDay.Add("chores", 3);
        maxTimesPerDay.Add("entertainment", 9999999);
        maxTimesPerDay.Add("exercise at home", 2);
        maxTimesPerDay.Add("nap", 1);
        maxTimesPerDay.Add("sleep", 1);
        maxTimesPerDay.Add("freshen", 2);
        maxTimesPerDay.Add("shower", 1);
        maxTimesPerDay.Add("exercise", 1);
        maxTimesPerDay.Add("friends", 99999999);
        maxTimesPerDay.Add("walk", 2);
    
    }
}
