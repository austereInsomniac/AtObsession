using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

// Connor + Mackenzie

public class ActionVariable
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
    private trash_spawner trash;

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
    Button lightW;
    Button intense;
    Button gym;

    Button cleanL;
    Button cleanK;
    Button cleanBe;
    Button cleanBa;

    void Awake()
    {
        // assign outside objects
        state = GetComponent<game_state>();
        state.addOnTimeChange(toggleButtons);
        notificationManager = GameObject.Find("Notification Panel").GetComponent<notification_manager>();
        trash = GetComponent<trash_spawner>(); 
        day = 1;

        // create
        buttons = new Dictionary<string, Button>();

        // find buttons
        cook = GameObject.Find("Cook food").GetComponent<Button>();
        restaurant = GameObject.Find("Eat at a restaurant").GetComponent<Button>();
        sleep = GameObject.Find("Go to sleep").GetComponent<Button>();
        nap = GameObject.Find("Take a nap").GetComponent<Button>();
        shower = GameObject.Find("ShowerB").GetComponent<Button>();
        bath = GameObject.Find("Bubble Bath").GetComponent<Button>();

        warmup = GameObject.Find("Warm up").GetComponent<Button>();
        lightW = GameObject.Find("Light workout").GetComponent<Button>();
        intense = GameObject.Find("Intense workout").GetComponent<Button>();
        gym = GameObject.Find("Go to the gym").GetComponent<Button>();

        cleanL = GameObject.Find("Do chores").GetComponent<Button>();
        cleanK = GameObject.Find("Do chores (1)").GetComponent<Button>();
        cleanBe = GameObject.Find("Do chores (2)").GetComponent<Button>();
        cleanBa = GameObject.Find("Do chores (3)").GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        activities = new Dictionary<string, ActionVariable>
        {
            // wellness, time, money

            // living room
            { "Do chores living room", new ActionVariable(10, 60, 0.00, "chores", "You spent some time doing some chores around the living room.") },
            { "Do chores kitchen", new ActionVariable(10, 60, 0.00, "chores", "You spent some time doing some chores around the kitchen.") },
            { "Do chores bedroom", new ActionVariable(10, 60, 0.00, "chores", "You spent some time doing some chores around the living room.") },
            { "Do chores bathroom", new ActionVariable(10, 60, 0.00, "chores", "You spent some time doing some chores around the kitchen.") },

            { "Go to the gym", new ActionVariable(8, 60, -15.00, "exercise", "You spent $15 to work out at your local gym.") },
            { "Visit friends", new ActionVariable(15, 45, 0.00, "friends", " You went out and spent some time with your friend.") },
            { "Go for a walk", new ActionVariable(10, 25, 0, "walk", "You went for a short walk at your local park.") },
            { "Watch TV", new ActionVariable(8, 10, 0.00, "entertainment", "You’ve watched an episode of your favorite show.") },
            { "Warm up", new ActionVariable(8, 30, 0.00, "exercise", "You decided to do a light warm up.") },
            { "Light workout", new ActionVariable(14, 75, 0.00, "exercise", "You chose to do a light workout.") },
            { "Intense workout", new ActionVariable(25, 120, 0.00, "exercise", "You committed to an intense workout.") },
            { "Eat at a restaurant", new ActionVariable(10, 60, -25.00, "food", "You spent $25 to eat out.") },//hunger

            // kitchen
            { "Cook food", new ActionVariable(10, 30, -5.00, "food", "You spent $5 on groceries to cook food at home.") },//hunger
            { "Eat a snack", new ActionVariable(10, 5, 0.00, "snack", "You ate a small snack.") },//hunger

            // bedroom
            { "Go to sleep", new ActionVariable(30, 32*60 - state.getTime(), 0.00, "sleep", "You had a night of restful sleep.") },
            { "Take a nap", new ActionVariable(20, 120, 0.00, "nap", "You took a short power nap.") },

            // bathroom
            { "Freshen up", new ActionVariable(3, 5, 0.00, "Freshen up", "You quickly freshened up.") },
            { "Shower", new ActionVariable(8, 20, 0.00, "shower", "You took a quick shower.") },
            { "Bubble bath", new ActionVariable(12, 45, 0.00, "bath", "You took a long relaxing bubble bath.") }
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
            { "chores", 999 },
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

    public ActionVariable getActionVariable(string key)
    {
        return activities[key];
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
            lightW.interactable = false;
            intense.interactable = false;
            gym.interactable = false;

            buttons.Add("Warm up", warmup);
            buttons.Add("Light workout", lightW);
            buttons.Add("Intense workout", intense);
            buttons.Add("Go to the gym", gym);
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

        // cleaning
        trash.addTrash(oldT, newT);
        if (trash.isDirty("LivingRoom"))
        {
            cleanL.interactable = true;
        }
        else
        {
            cleanL.interactable = false;
        }

        if (trash.isDirty("KitchenT"))
        {
            cleanK.interactable = true;
        }
        else
        {
            cleanK.interactable = false;
        }

        if (trash.isDirty("BedroomT"))
        {
            cleanBe.interactable = true;
        }
        else
        {
            cleanBe.interactable = false;
        }

        if (trash.isDirty("BathroomT"))
        {
            cleanBa.interactable = true;
        }
        else
        {
            cleanBa.interactable = false;
        }

    }

    public void doAction(string key)
    {
        if (key != null)
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
                    state.updateHunger(-6 * 60);
                }
                else if (activity.getGroup() == "snack")
                {
                    state.updateHunger(-2f * 60);
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
                    state.updateSleep(-8 * 60);
                }

                if (activity.getGroup() == "chores")
                {
                    if (key.Equals("Do chores living room"))
                    {
                        trash.cleanTrash("LivingRoom");
                    }
                    else if (key.Equals("Do chores kitchen"))
                    {
                        trash.cleanTrash("KitchenT");
                    }
                    else if (key.Equals("Do chores bedroom"))
                    {
                        trash.cleanTrash("BedroomT");
                    }
                    else
                    {
                        trash.cleanTrash("BathroomT");
                    }
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
    }

    private void randomizeStats()
    {
        // time based
        activities["Go to sleep"] = new ActionVariable(30, 32*60 - state.getTime(), 0.00, "sleep", " You had a night of restful sleep.");
    }
}