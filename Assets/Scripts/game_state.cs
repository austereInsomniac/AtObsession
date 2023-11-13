using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_state : MonoBehaviour
{
    // Instance Variables
    private int wellness;
    private int savedWellness;

    private int reputation;
    private int savedReputation;

    private int subscribers;
    private int savedSubscribers;

    private int ending;
    private int savedEnding;

    private double money;
    private double savedMoney;

    private bool hasDied;
    private bool savedHasDied;

    // time is in terms of minutes since midnight - 480 is 8am
    private int time;
    private int day;
    private int savedDay;

    // hours since you last ate. this will update the UI. if it equal to or greater than 6, you are hungry
    private int hunger;
    private int savedHunger;
    private SpriteRenderer hungerHUD;

    // current room and its canvas
    private GameObject location;
    private GameObject locationCanvas;

    // other scripts
    private notification_manager notificationManager;
    private move_location locationManager;
    private splash_screen_manager splashScreenManager;

    // delegates 
    public delegate void changeWellness(int oldWellness, int newWellness);
    private changeWellness onWellnessChanged;

    public delegate void changeTime(int oldTime, int newTime);
    private changeTime onTimeChanged;

    public delegate void changeSubscribers(int oldSubscribers, int newSubscribers);
    private changeSubscribers onSubscribersChanged;

    public delegate void changeMoney(double oldMoney, double newMoney);
    private changeMoney onMoneyChanged;

    public delegate void changeReputation(int oldReputation, int newReputation);
    private changeReputation onReputationChanged;

    // Set Up
    private void Awake()
    {
        location = GameObject.Find("Main Menu");
        locationCanvas = GameObject.Find("Main Menu Canvas");
        hungerHUD = GameObject.Find("Hunger").GetComponent<SpriteRenderer>();

        notificationManager = GameObject.FindGameObjectWithTag("notifications").GetComponent<notification_manager>();
        locationManager = GetComponent<move_location>();
        splashScreenManager = GetComponent<splash_screen_manager>();

        wellness = 70;
        savedWellness = 70;

        day = 1;
        savedDay = 1;

        time = 480;

        hunger = 0;
        savedHunger = 0;

        reputation = 20;
        savedReputation = 20;

        subscribers = 1000;
        savedSubscribers = 1000;

        ending = 0;
        savedEnding = 0;

        money = 100.00;
        savedMoney = 100.00;

        hasDied = false;
        savedHasDied = false;
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
        wellness += w;

        if (wellness >= 100)
        { 
            wellness = 100;
        }
        else if (wellness <= 0)
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

        notifyOnWellnessChanged(wellness - w, wellness);
    }

    private void killPlayer()
    {
        // reset stats
        money = 100;
        reputation = 20;
        subscribers = 1000;
        wellness = 70;
        ending = 0;
        day = 1;
        hunger = 0;

        // game over
        splashScreenManager.openSplashScreen("Game over");
        locationManager.goToGameOver();
    }

    private void playHospitalScene()
    {
        // set stats
        hasDied = true;
        updateWellness(50);

        // call hospital scene 
        splashScreenManager.openSplashScreen("Hospital");
        locationManager.goToBedroom();
    }

    public void resetDay()
    {
        // reset stats
        wellness = savedWellness;
        hasDied = savedHasDied;
        money = savedMoney;
        day = savedDay;
        reputation = savedReputation;
        subscribers = savedSubscribers;
        ending = savedEnding;
        time = 480;
        hunger = savedHunger;

        // move location
        locationManager.goToBedroom();

        // reset HUD + splash screen
        splashScreenManager.openSplashScreen("reset");

        // call delegates
        notifyOnWellnessChanged(wellness, wellness);
        notifyOnTimeChanged(time, time);
        notifyOnMoneyChange(money, money);
        notifyOnSubscribersChange(subscribers, subscribers);
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
        if ((time > 240 && time < 480))
        {
            // If the time when the activity is run is between 4 and 8 am then advance the day to make the sleep
            // bug if an action is longer than 6 hours...

            time = 480; // set time to 8am

            updateWellness(-20); // Lowers your wellness
            locationManager.goToBedroom();  // Move to the bedroom
            // run sleep method
        }

        // update later when we lock sleep to late at night
        if(time != 480)
        {
            updateHunger(t);
        }
        else
        {
            // save stats to reset the day
            savedMoney = money;
            savedReputation = reputation;
            savedSubscribers = subscribers;
            savedWellness = wellness;
            savedDay = day;
            savedHasDied = hasDied;
            savedHunger = hunger;
        }

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
            notificationManager.showNotification("You are hungry.");
        }

        // for each time jump, lower wellness
        if(hunger > 6*60)
        {
            // don't subtract when the player is sleeping

            // catches when you do half of an action before being hungry and half after so you dont loos extra/no wellness
            int over = 6*60 - hunger;
            int loss = Mathf.Max(t, over);

            // for every hour you are hungry after the original notification, lower wellness by 5
            updateWellness((int)(-loss * .0833 -.5));
        }
    }

    public void resetHunger() {
        // run after the time change
        hunger = 0;

        // turn off icon
        hungerHUD.enabled = false;
    }

    public void updateReputation(int r)
    {
        reputation = reputation + r;
    }

    public void updateSubscribers(int s)
    {    
        subscribers = subscribers + s;
        notifyOnSubscribersChange(subscribers - s, subscribers);
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
 
    public void moveLocation(GameObject newLocation, GameObject newCanvas)
    {
        // the players current room has changed
        location = newLocation;
        locationCanvas = newCanvas;
    }

    private void Update()
    {
        // move to main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            locationManager.goToMainMenu();
        }
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

    public void addOnReputationChange(changeReputation changeReputation)
    {
        onReputationChanged += changeReputation;
    }

    private void notifyOnReputationChange(int oldReputation, int newReputation)
    {
        onReputationChanged(oldReputation, newReputation);
    }


    // remove later
    private void Start()
    {
        addOnTimeChange(doNothing);
        addOnWellnessChange(doNothing);
        addOnSubscribersChange(doNothing);
        addOnMoneyChange(doNothing2);
        addOnReputationChange(doNothing);
    }

    private void doNothing(int t, int t2) { }

    private void doNothing2(double t, double t2) { }
}


