using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_state : MonoBehaviour
{
    private int wellness;
    private int reputation;
    private int subscribers;
    private int ending;
    private double money;
    private bool hasDied;

    // time is in terms of minutes since midnight - 480 is 8am
    private int time;
    private int day;

    // hours since you last ate. this will update the UI if it equal to or greater than 4, you are hungry
    private int hunger;
    private SpriteRenderer hungerHUD;

    // current room and its canvas
    private GameObject location;
    private GameObject locationCanvas;

    // other scripts
    private notification_manager notificationManager;
    private move_location locationManager;

    // delegates 
    public delegate void changeWellness(int oldWellness, int newWellness);
    private changeWellness onWellnessChanged;

    public delegate void changeTime(int oldTime, int newTime);
    private changeTime onTimeChanged;

    public delegate void changeSubscribers(int oldSubscribers, int newSubscribers);
    private changeSubscribers onSubscribersChanged;

    public delegate void changeMoney(double oldMoney, double newMoney);
    private changeMoney onMoneyChanged;

    private void Awake()
    {
        location = GameObject.Find("Living Room");
        locationCanvas = GameObject.Find("Living Room Canvas");
        hungerHUD = GameObject.Find("Hunger").GetComponent<SpriteRenderer>();

        notificationManager = GameObject.FindGameObjectWithTag("notifications").GetComponent<notification_manager>();
        locationManager = GetComponent<move_location>();

        wellness = 70;
        day = 1;
        time = 480;
        hunger = 0;
        reputation = 20;
        subscribers = 1000;
        ending = 0;
        money = 100.00;
        hasDied = false;
    }

    // getters
    public int getWellness() { return wellness; }

    public int getDay() { return day; }

    public int getTime() { return time; }

    public int getReputation() { return reputation; }

    public int getSubscribers() { return subscribers; }

    public int getEnding() { return ending; }

    public double getMoney() { return money; }

    public GameObject getLocation() { return location; }

    public GameObject getLocationCanvas() { return locationCanvas;}

    // setters + methods
    public void updateWellness(int w)
    {
        if (wellness + w >= 100)
        { 
            wellness = 100;
        }
        else if (wellness + w <= 0)
        {
            wellness = 0;

            if (hasDied)
            {
                killPlayer();
            }
            else
            {
                playHospitalScene();
            }
        }
        else
        {
            wellness += w;
        }

        notifyOnWellnessChanged(wellness - w, wellness);
    }

    private void killPlayer()
    {
        GetComponent<splash_screen_manager>().openSplashScreen("Game over");
        locationManager.goToGameOver();
    }

    private void playHospitalScene()
    {
        // call hospital scene 
        GetComponent<splash_screen_manager>().openSplashScreen("Hospital");
        locationManager.goToBedroom();
        hasDied = true;

        // give the hospital text
    }

    public void updateTime(int t)
    {
        // update time
        time += t;

        // update day if we hit midnight
        if (time >= 1440)
        {
            day++;
            time -= 1440;
        }

        // force sleep 
        // If the time when the activity is run is between 4 and 8 am then advance the day to make the sleep
        // bug if an action is longer than 4 hours...
        if ((time > 240 && time < 480))
        {
            time = 480; // set time to 8am
            updateWellness(-20); // Lowers your wellness
            GetComponent<move_location>().goToBedroom();  // Move to the bedroom
            // run sleep method
        }

        // update hunger
        updateHunger(t);

        // call all delegates
        notifyOnTimeChanged(time - t, time);
    }

    private void updateHunger(int t)
    {
        hunger += t;

        // player is hungry
        if(hunger >= 4*60)
        {
            // display icon
            hungerHUD.enabled = true;

            // display notification
            notificationManager.ShowNotifications("You are hungy.");
        }
        else
        {
            // turn off icon
            hungerHUD.enabled = false;
        }

        // for each time jump, lower wellness
        if(hunger > 4*60)
        {
            // catches when you do half of an action before being hungry and half after so you dont loos extra/no wellness
            int over = 4*60 - hunger;
            int loss = Mathf.Max(t, over);

            // for every hour you are hungry after the original notification, lower wellness by 10
            updateWellness((int)(-loss * .1666 -.5));
        }
    }

    public void resetHunger() {
        // run after the time change
        hunger = 0;
    }

    public void updateReputation(int r)
    {
        reputation = reputation + r;
    }

    public void updateSubscribers(int s)
    {
        //notifyOnSubscribersChange(subscribers, subscribers + s);
        subscribers = subscribers + s;
    }

    public void updateEnding(int e)
    {
        ending = ending + e;
    }

    public void updateMoney(double m)
    {
        money = money + m;
        notifyOnMoneyChange(money - m, money);
    }

    // the players current room has changed
    public void moveLocation(GameObject newLocation, GameObject newCanvas)
    {
        location = newLocation;
        locationCanvas = newCanvas;
    }

    // delegate methods

    // call this method with a parameter method to make the parameter method be called when wellness changed
    // in your code, call something like player.addOnWellnessChange(methodThatYouWantToRun)
    // your method must have the same parameters and return type
    public void addOnWellnessChange(changeWellness newWellnessChanged)
    {
        onWellnessChanged += newWellnessChanged;
    }

    // calls all the methods attatched to onChangeWellness when wellness is updated
    private void notifyOnWellnessChanged(int oldWellness, int newWellness)
    {
        onWellnessChanged(oldWellness, newWellness);
    }

    public void addOnTimeChange(changeTime newTimeChanged) 
    {
        onTimeChanged += newTimeChanged;
    }

    private void notifyOnTimeChanged(int oldTime, int newTime)
    {
        onTimeChanged(oldTime, newTime);
    }

    public void addOnSubscribersChange(changeSubscribers changeSubscribers)
    {
        onSubscribersChanged += changeSubscribers;
    }

    private void notifyOnSubscribersChange(int oldSubscribers, int newSubscribers)
    {
        onSubscribersChanged(oldSubscribers, newSubscribers);
    }

    public void addOnMoneyChange(changeMoney changeMoney)
    {
        onMoneyChanged += changeMoney;
    }

    private void notifyOnMoneyChange(double oldMoney, double newMoney)
    {
        onMoneyChanged(oldMoney, newMoney);
    }


    // remove later
    private void Start()
    {
        addOnTimeChange(doNothing);
        addOnWellnessChange(doNothing);
        addOnSubscribersChange(doNothing);
        addOnMoneyChange(doNothing2);
    }

    private void doNothing(int t, int t2) { }

    private void doNothing2(double t, double t2) { }
}


