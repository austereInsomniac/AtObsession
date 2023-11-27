using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Connor + Mackenzie

class ActionVariable
{
    int wellness = 0;
    int time = 0;
    double money = 0.00;
    string group;
    string actionText;

    public ActionVariable(int changeWellness, int changeTime, double changeMoney, string group_, string actionText_)
    {
        wellness = changeWellness;
        time = changeTime;
        money = changeMoney;
        group = group_;
        actionText = actionText_;
    }

    public int getWellness() { return wellness; }

    public int getTime() { return time; }

    public double getMoney() { return money; }

    public string getGroup() { return group; }

    public string getText() { return actionText; }

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
    Dictionary<string, Button> buttons;

    // outside objects
    private game_state state;
    private notification_manager notificationManager;
    private int day;

    // RNG
    System.Random rand = new System.Random();

    // buttons
    Button cook;
    Button restaurant;
    Button sleep;
    Button nap;
    Button shower;
    Button bath;

    Button warmup;
    Button light;
    Button intense;

    void Awake()
    {
        // assign outside objects
        state = GetComponent<game_state>();
        state.addOnTimeChange(toggleButtons);
        notificationManager = GameObject.Find("Notification Panel").GetComponent<notification_manager>();
        day = 1;

        // create
        buttons = new Dictionary<string, Button>();

        // find buttons
        cook = GameObject.Find("Cook food").GetComponent<Button>();
        restaurant = GameObject.Find("Eat at a restaurant").GetComponent<Button>();
        sleep = GameObject.Find("Go to sleep").GetComponent<Button>();
        nap = GameObject.Find("Take a nap").GetComponent<Button>();
        shower = GameObject.Find("Shower").GetComponent<Button>();
        bath = GameObject.Find("Bubble Bath").GetComponent<Button>();

        warmup = GameObject.Find("Warm up").GetComponent<Button>();
        light = GameObject.Find("Light workout").GetComponent<Button>();
        intense = GameObject.Find("Intense workout").GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        activities = new Dictionary<string, ActionVariable>
        {
            // wellness, time, money

            // living room
            { "Do chores", new ActionVariable(8, 15, 0.00, "chores", "chores") },
            { "Go to the gym", new ActionVariable(8, RandomTimeBig(), -15.00, "exercise", "") },
            { "Visit friends", new ActionVariable(RandomWellness(), RandomTimeBig(), 0.00, "friends", "") },
            { "Go for a walk", new ActionVariable(10, 25, 0, "walk", "") },
            { "Watch TV", new ActionVariable(8, RandomTimeSmall(), 0.00, "entertainment", "") },
            { "Warm up", new ActionVariable(8, 30, 0.00, "exercise", "") },
            { "Light workout", new ActionVariable(14, 75, 0.00, "exercise", "") },
            { "Intense workout", new ActionVariable(25, 120, 0.00, "exercise", "") },
            { "Eat at a restaurant", new ActionVariable(10, 60, -25.00, "food", "") },//hunger

            // kitchen
            { "Cook food", new ActionVariable(10, 30, -5.00, "food", "") },//hunger
            { "Eat a snack", new ActionVariable(10, 5, 0.00, "snack", "") },//hunger

            // bedroom
            { "Go to sleep", new ActionVariable(30, 32*60 - state.getTime(), 0.00, "sleep", "") },
            { "Take a nap", new ActionVariable(20, 120, 0.00, "nap", "") },

            // bathroom
            { "Freshen up", new ActionVariable(3, 5, 0.00, "freshen", "") },
            { "Shower", new ActionVariable(8, 20, 0.00, "shower", "") },
            { "Bubble bath", new ActionVariable(12, 45, 0.00, "bath", "") }
        };

        // set up time limits
        timesPerDay = new Dictionary<string, int>();

        maxTimesPerDay = new Dictionary<string, int>()
        {
            // other limits
            { "food", 3 },
            { "snack", 3},
            { "nap", 1},

            // normal
            { "chores", 1 },
            { "walk", 2},
            { "freshen", 2},
            { "shower", 1},
            { "bath", 1 },
            { "exercise", 1},

            // no limits
            { "sleep", 999},
            { "entertainment", 999 },
            { "friends", 999}
        };

        // toggle any buttons at the start
        toggleButtons(0, 0);
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
        return 999;
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
        if (day != GetComponent<game_state>().getDay())
        {
            timesPerDay.Clear();
            foreach (KeyValuePair<string, Button> button in buttons)
            {
                button.Value.interactable = true;
            }
            buttons.Clear();
        }
        day = GetComponent<game_state>().getDay();
    }

    //Sets the buttons to not interactable when the user has used them up for the current day
    public void notInteractable(string key, string group)
    {
        if (group.Equals("food"))
        {
            cook.interactable = false;
            restaurant.interactable = false;

            buttons.Add("Cook food", cook);
            buttons.Add("Eat at a restaurant", restaurant);
        }
        else if (group.Equals("exercise"))
        {
            warmup.interactable = false;
            light.interactable = false;
            intense.interactable = false;

            buttons.Add("Warm up", warmup);
            buttons.Add("Light workout", light);
            buttons.Add("Intense workout", intense);
        }
        else
        {
            Button button = GameObject.Find(key).GetComponent<Button>();
            button.interactable = false;
            buttons.Add(key, button);
        }
    }

    private void toggleButtons(int oldT, int newT) 
    {
        if (state.hungry())
        {
            cook.interactable = true;
            restaurant.interactable = true;
            
        }
        else
        {
            cook.interactable = false;
            restaurant.interactable = false;
        }

        if (state.needsShower())
        {
            shower.interactable = true;
            bath.interactable = true;
        }
        else
        {
            shower.interactable = false;
            bath.interactable = false;
        }

        if (state.tired() && state.getTime() > 12*60)
        {
            nap.interactable = true;
        }
        else
        {    
            nap.interactable = false;
        }

        if(state.tired() && state.getTime() > 22 * 60)
        {
            sleep.interactable = true;
        }
        else
        {
            sleep.interactable = false;
        }

    }

    public void doAction(string key)
    {
        // re roll random stats
        randomizeStats();

        ActionVariable activity = activities[key];
        string group = activity.getGroup();

        if (getCurrentTimesPerDay(group) < getMaxTimesPerDay(group))
        {
            updateTimesPerDay(group);

            // must be before splash screen so notifications work, and before time jump
            // updating alternate stats
            if (activity.getGroup() == "food")
            {
                state.updateHunger(-4 * 60);
            }
            else if (activity.getGroup() == "snack")
            {
                state.updateHunger(-1.5f * 60);
            }
            else if (activity.getGroup() == "freshen")
            {
                state.updateShower(-4 * 60);
            }
            else if (activity.getGroup() == "shower")
            {
                state.updateShower(-12 * 60);
            }
            else if (activity.getGroup() == "bath")
            {
                state.updateShower(-16 * 60);
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

            // set the rest of the buttons in the group as off
            if (getCurrentTimesPerDay(group) == getMaxTimesPerDay(group))
            {
                notInteractable(key, group);
            }

            // update stats
            state.updateWellness(activity.getWellness());
            state.updateTime(activity.getTime());
            state.updateMoney(activity.getMoney());

            // notifications
            notificationManager.showWellnessNotification(activity.getText(), state.getWellness());

            // reset times if needed
            resetTimesPerDay();
        }
    }

    private void randomizeStats()
    {
        // time based
        activities["Go to sleep"] = new ActionVariable(30, 32*60 - state.getTime(), 0.00, "sleep", "");

        // random
        activities["Go to the gym"] = new ActionVariable(8, RandomTimeBig(), 15.00, "exercise", "");
        activities["Visit friends"] = new ActionVariable(RandomWellness(), RandomTimeBig(), 0.00, "friends", "");
        activities["Watch TVs"] = new ActionVariable(8, RandomTimeSmall(), 0.00, "entertainment", "");
    }
}