using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using System.Collections;
//using System.Random;
using UnityEngine.XR;
using JetBrains.Annotations;

class ActionVariables
=======

// Connor + Mackenzie

class ActionVariable
>>>>>>> main
{
    int wellness = 0;
    int time = 0;
    double money = 0.00;
<<<<<<< HEAD

    public ActionVariables(int changeWellness, int changeTime, double changeMoney)
=======
    string group;

    public ActionVariable(int changeWellness, int changeTime, double changeMoney, string group_)
>>>>>>> main
    {
        wellness = changeWellness;
        time = changeTime;
        money = changeMoney;
<<<<<<< HEAD
=======
        group = group_;
>>>>>>> main

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

<<<<<<< HEAD
    public void Hunger()
    {

    }

    public void Time(int oldTime, int newTime)
    {

=======
    public string getGroup() { return group; }
    public void Time(int oldTime, int newTime)
    {
        time = oldTime + newTime;
>>>>>>> main
    }
}

public class daily_action_storage : MonoBehaviour
{
<<<<<<< HEAD

    [SerializeField]
    private string key;

    List<ActionVariables> listVariables;
    Dictionary<string, ActionVariables> activities;

    ActionVariables action;

    GameObject player;

    System.Random rand = new System.Random();

=======
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

>>>>>>> main
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
<<<<<<< HEAD
=======

>>>>>>> main
        randomNumber = rand.Next(1, 2);
        if (randomNumber == 1)
        {
            return -15;
        }
        if (randomNumber == 2) {
            return 15;
        }
<<<<<<< HEAD
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
        ActionVariables activity = activities[key];
        player.GetComponent<game_state>().updateWellness(activity.getWellness());
        player.GetComponent<game_state>().updateTime(activity.getTime());
        player.GetComponent<game_state>().updateMoney(activity.getMoney());
=======
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
            // update the splash screen FIRST so that death scenes work
            GetComponent<splash_screen_manager>().openSplashScreen(key);

            updateTimesPerDay(activity.getGroup());

            state.updateWellness(activity.getWellness());
            state.updateTime(activity.getTime());
            state.updateMoney(activity.getMoney());

            if (activity.getGroup() == "food" || activity.getGroup() == "snack")
            {
                state.resetHunger();
            }
        }
>>>>>>> main
    }

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        activities = new Dictionary<string, ActionVariables>();
        activities.Add("Cook food", new ActionVariables(10, 30, 5.00));//hunger
        activities.Add("Eat at a restaurant", new ActionVariables(10, 60, 25.00));//hunger
        activities.Add("Eat a snack", new ActionVariables(10, 5, 0.00));//hunger
        activities.Add("Go to sleep", new ActionVariables(30, 480 /*- action.getTime()*/, 0.00));//use the equation to adjust the wellness 
        activities.Add("Take a nap", new ActionVariables(20, 120, 0.00));
        activities.Add("Forced Sleep", new ActionVariables(-5, 480 - 120, 0.00));
        activities.Add("Freshen up", new ActionVariables(3, 5, 0.00));
        activities.Add("Take a shower", new ActionVariables(8, 20, 0.00));
        activities.Add("Bubble bath", new ActionVariables(12, 45, 0.00));
        activities.Add("Do chores", new ActionVariables(8, 15, 0.00));
        activities.Add("Go to the gym", new ActionVariables(8, RandomTimeBig(), 15.00));
        activities.Add("Visit friends", new ActionVariables(RandomWellness(), RandomTimeBig(), 0.00));
        activities.Add("Go for a walk", new ActionVariables(10, 25, 0));
        activities.Add("Watch TV", new ActionVariables(8, RandomTimeSmall(), 0.00));
        activities.Add("Lift weights", new ActionVariables(8, 20, 0.00));
=======
        activities = new Dictionary<string, ActionVariable>
        {
            // living room
            { "Do chores", new ActionVariable(8, 15, 0.00, "chores") },
            { "Go to the gym", new ActionVariable(8, RandomTimeBig(), 15.00, "exercise") },
            { "Visit friends", new ActionVariable(RandomWellness(), RandomTimeBig(), 0.00, "friends") },
            { "Go for a walk", new ActionVariable(10, 25, 0, "walk") },
            { "Watch TV", new ActionVariable(8, RandomTimeSmall(), 0.00, "entertainment") },
            { "Lift weights", new ActionVariable(8, 20, 0.00, "exercise at home") },
            { "Eat at a restaurant", new ActionVariable(10, 60, 25.00, "food") },//hunger

            // kitchen
            { "Cook food", new ActionVariable(10, 30, 5.00, "food") },//hunger
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
        maxTimesPerDay.Add("snack", 5);
        maxTimesPerDay.Add("chores", 3);
        maxTimesPerDay.Add("entertainment", 9999999);
        maxTimesPerDay.Add("exercise at home", 2);
        maxTimesPerDay.Add("nap", 1);
        maxTimesPerDay.Add("sleep", 999);
        maxTimesPerDay.Add("freshen", 2);
        maxTimesPerDay.Add("shower", 1);
        maxTimesPerDay.Add("exercise", 1);
        maxTimesPerDay.Add("friends", 99999999);
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
>>>>>>> main
    }
}
