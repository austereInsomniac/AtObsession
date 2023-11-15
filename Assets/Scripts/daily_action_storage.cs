using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

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

    //this updates the time as it adds the time to do something and adds it to the current time
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
    Dictionary<string ,Button> buttons;
    

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

    //This gets the time between an hour and two hours as an int
    int RandomTimeBig()
    {
        int randomNumber;

        randomNumber = rand.Next(60 / 5, 120 / 5);
        randomNumber *= 5;
        return randomNumber;
    }

    //this gets the time between half an hour and an hour as an int
    int RandomTimeSmall()
    {
        int randomNumber;

        randomNumber = rand.Next(30 / 5, 60 / 5);
        randomNumber *= 5;
        return randomNumber;
    }

    //this gets a random wellness of either 15 or -15 as an int
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

    //this gets the max times an activity can be done which is an int
    int getMaxTimesPerDay(string key)
    {
        if (maxTimesPerDay.ContainsKey(key))
        {
            return maxTimesPerDay[key];
        }
        return 999999;
    }

    //this gets the amount of times the user has done the activity
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

    //clears the current timesPerDay dictionary and makes the buttons in the in the buttons
    //dictionary interactable, if the day advances
    private void resetTimesPerDay()
    {
        Debug.Log(day);
        Debug.Log(GetComponent<game_state>().getDay());
        if(day != GetComponent<game_state>().getDay())
        {
            Debug.Log("In the if");
            timesPerDay.Clear();
            foreach (KeyValuePair<string, Button> button in buttons)
            {
                Debug.Log("In foreach loop");
                button.Value.interactable = true;
            }
            buttons.Clear();
        }
        day = GetComponent<game_state>().getDay();
    }

    //Sets the buttons to not interactable when the user has used them up for the current day
    public void notInteractable(string key, string group)
    {
        if (getCurrentTimesPerDay(group) == getMaxTimesPerDay(group))
        {
            if(group == "food" && key == "Cook food")
            {
                GameObject findSecondButton = GameObject.Find("Eat at a restaurant");
                GameObject findButton = GameObject.Find(key);
                Button button1 = findButton.GetComponent<Button>();
                Button button2 = findSecondButton.GetComponent<Button>();
                button1.interactable = false;
                button2.interactable = false;
                buttons.Add(group, button1);
                buttons.Add(key, button2);
            }
            else if (group == "food" && key == "Eat at a restaurant")
            {
                GameObject findSecondButton = GameObject.Find("Cook food");
                GameObject findButton = GameObject.Find(key);
                Button button1 = findButton.GetComponent<Button>();
                Button button2 = findSecondButton.GetComponent<Button>();
                button1.interactable = false;
                button2.interactable = false;
                buttons.Add(group, button1);
                buttons.Add(key, button2);
            }
            else
            {
                GameObject findButton = GameObject.Find(key);
                Button button1 = findButton.GetComponent<Button>();
                button1.interactable = false;
                buttons.Add(group, button1);
            }
        }
    }
    public void doAction(string key)
    {
        // re roll random stats
        randomizeStats();

        // update each statistic
        ActionVariable activity = activities[key];
       
       if (getCurrentTimesPerDay(activity.getGroup()) < getMaxTimesPerDay(activity.getGroup()))
        {
            updateTimesPerDay(activity.getGroup());

            state.updateWellness(activity.getWellness());
            state.updateTime(activity.getTime());
            state.updateMoney(activity.getMoney());

            // reset times if needed
            resetTimesPerDay();

            if (activity.getGroup() == "food" || activity.getGroup() == "snack")
            {
                state.resetHunger();
            }

            // update the splash screen
            GetComponent<splash_screen_manager>().openSplashScreen(key);
            if (getCurrentTimesPerDay(activity.getGroup()) == getMaxTimesPerDay(activity.getGroup()))
            {
                notInteractable(key, activity.getGroup());
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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

        buttons = new Dictionary<string, Button>();
        this.GetComponent<game_state>();
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
