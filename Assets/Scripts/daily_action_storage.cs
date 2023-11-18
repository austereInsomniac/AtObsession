using System.Collections.Generic;
using UnityEngine;

// Connor + Mackenzie

class ActionVariable
{
    int wellness = 0;
    int time = 0;
    double money = 0.00;
    string group;

    public ActionVariable(int changeWellness, int changeTime, double changeMoney, string group_)
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
    Dictionary<string, ActionVariable> activities;
    Dictionary<string, int> timesPerDay;
    Dictionary<string, int> maxTimesPerDay;

    // outside objects
    private game_state state;
    private int day;

    // RNG
    System.Random rand = new System.Random();

    void Awake()
    {
        // assign outside objects
        state = GetComponent<game_state>();
        day = 1;
    }

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

    private void resetTimesPerDay()
    {
        if(day != GetComponent<game_state>().getDay())
        {
            foreach (KeyValuePair<string, int> action in maxTimesPerDay)
            {
                timesPerDay[action.Key] = 0;
            }
        }
        day = GetComponent<game_state>().getDay();
    }

    public void doAction(string key)
    {
        // re roll random stats
        randomizeStats();

        // reset times if needed
        //resetTimesPerDay();

        // update each statistic
        ActionVariable activity = activities[key];

        //if (getCurrentTimesPerDay(activity.getGroup()) < getMaxTimesPerDay(activity.getGroup()))
        {
            // must be before splash screen so notifications work, and before time jump
            if (activity.getGroup() == "food")
            {
                state.updateHunger(-4*60);  
            }
            else if(activity.getGroup() == "snack")
            {
                state.updateHunger(-1.5f * 60);
            }
            else if(activity.getGroup() == "freshen")
            {
                state.updateShower(-4 * 60);
            }
            else if (activity.getGroup() == "shower")
            {
                state.updateShower(-12 * 60);
            }
            else if (activity.getGroup() == "sleep")
            {
                state.updateSleep(-14 * 60);
            }
            else if (activity.getGroup() == "nap")
            {
                state.updateSleep(-6 * 60);
            }

            // update the splash screen before updating stats so that death scenes work
            GetComponent<splash_screen_manager>().openSplashScreen(key);

            updateTimesPerDay(activity.getGroup());

            state.updateWellness(activity.getWellness());
            state.updateTime(activity.getTime());
            state.updateMoney(activity.getMoney());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        activities = new Dictionary<string, ActionVariable>
        {
            // wellness, time, money

            // living room
            { "Do chores", new ActionVariable(8, 15, 0.00, "chores") },
            { "Go to the gym", new ActionVariable(8, RandomTimeBig(), -15.00, "exercise") },
            { "Visit friends", new ActionVariable(RandomWellness(), RandomTimeBig(), 0.00, "friends") },
            { "Go for a walk", new ActionVariable(10, 25, 0, "walk") },
            { "Watch TV", new ActionVariable(8, RandomTimeSmall(), 0.00, "entertainment") },
            { "Warm up", new ActionVariable(8, 30, 0.00, "exercise at home") },
            { "Light workout", new ActionVariable(14, 75, 0.00, "exercise at home") },
            { "Intense workout", new ActionVariable(25, 120, 0.00, "exercise at home") },
            { "Eat at a restaurant", new ActionVariable(10, 60, -25.00, "food") },//hunger

            // kitchen
            { "Cook food", new ActionVariable(10, 30, -5.00, "food") },//hunger
            { "Eat a snack", new ActionVariable(10, 5, 0.00, "snack") },//hunger

            // bedroom
            { "Go to sleep", new ActionVariable(30, 32*60 - state.getTime(), 0.00, "sleep") },
            { "Take a nap", new ActionVariable(20, 120, 0.00, "nap") },

            // bathroom
            { "Freshen up", new ActionVariable(3, 5, 0.00, "freshen") },
            { "Shower", new ActionVariable(8, 20, 0.00, "shower") },
            { "Bubble bath", new ActionVariable(12, 45, 0.00, "shower") }

        };

        // set up time limits
        timesPerDay = new Dictionary<string, int>();
        maxTimesPerDay = new Dictionary<string, int>();

        maxTimesPerDay.Add("food", 3);
        maxTimesPerDay.Add("snack", 3);
        maxTimesPerDay.Add("chores", 1);
        maxTimesPerDay.Add("entertainment", 999);
        maxTimesPerDay.Add("exercise at home", 1);
        maxTimesPerDay.Add("nap", 1);
        maxTimesPerDay.Add("sleep", 999);
        maxTimesPerDay.Add("freshen", 2);
        maxTimesPerDay.Add("shower", 1);
        maxTimesPerDay.Add("exercise", 1);
        maxTimesPerDay.Add("friends", 999);
        maxTimesPerDay.Add("walk", 2);
    }

    private void randomizeStats()
    {
        // time based
        activities["Go to sleep"] = new ActionVariable(30, 32*60 - state.getTime(), 0.00, "sleep");

        // random
        activities["Go to the gyms"] = new ActionVariable(8, RandomTimeBig(), 15.00, "exercise");
        activities["Visit friends"] = new ActionVariable(RandomWellness(), RandomTimeBig(), 0.00, "friends");
        activities["Watch TVs"] = new ActionVariable(8, RandomTimeSmall(), 0.00, "entertainment");
    }
}
