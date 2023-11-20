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
    private int videosMadeToday;

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
    private float hunger;
    private float savedHunger;
    private SpriteRenderer hungerHUD;

    // level of cleanliness
    private float shower;
    private float savedShower;
    private SpriteRenderer showerHUD;

    // level of tiredness
    private float sleep;
    private float savedSleep;
    private SpriteRenderer sleepHUD;

    // current room and its canvas
    private GameObject location;
    private GameObject locationCanvas;

    // other scripts
    private notification_manager notificationManager;
    private move_location locationManager;
    private splash_screen_manager splashScreenManager;

    // testing variables
    public bool testingVideoWellness;

    // delegates 
    public delegate void changeWellness(int oldWellness, int newWellness);
    private changeWellness onWellnessChanged;

    public delegate void changeTime(int oldTime, int newTime);
    private changeTime onTimeChanged;

    public delegate void changeMoney(double oldMoney, double newMoney);
    private changeMoney onMoneyChanged;

    public delegate void changeReputation(int oldReputation, int newReputation);
    private changeReputation onReputationChanged;

    public delegate void changeLocation(GameObject oldLocation, GameObject newLocation);
    private changeLocation onLocationChanged;

    // Set Up
    private void Awake()
    {
        location = GameObject.Find("Main Menu");
        locationCanvas = GameObject.Find("Main Menu Canvas");
        hungerHUD = GameObject.Find("Hunger HUD").GetComponent<SpriteRenderer>();
        sleepHUD = GameObject.Find("Sleep HUD").GetComponent<SpriteRenderer>();
        showerHUD = GameObject.Find("Shower HUD").GetComponent<SpriteRenderer>();

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

        reputation = 50;
        savedReputation = 50;

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

    public GameObject getLocationCanvas() { return locationCanvas; }

    public float getHunger() { return hunger; }

    public bool hungry() { return hunger > 4 *60; }

    public bool needsShower() { return shower > 12 * 60; }

    public bool tired() { return sleep > 14 * 60; }

    public bool getHasDied() {  return hasDied; }

    public SpriteRenderer getHungerHUD() { return hungerHUD; }

    public SpriteRenderer getShowerHUD() {  return showerHUD; }

    public SpriteRenderer getSleepHUD() {  return sleepHUD; }

    // setters 
    public void updateWellness(int w)
    {
        wellness += w;

        if (wellness > 100)
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

    public void updateTime(int t)
    {
        // update time
        time += t;

        // update day if we hit midnight
        if (time >= 1440)
        {
            day++;
            time -= 1440;

            // update reputaiton if needed
            if(videosMadeToday == 0)
            {
                updateReputation(-20);
            }

            videosMadeToday = 0;
        }

        // force sleep 
        if ((time > 240 && time < 480))
        {
            // If the time when the activity is run is between 4 and 8 am then advance the day to make the sleep
            // bug if an action is longer than 6 hours...

            time = 480; // set time to 8am

            if (!testingVideoWellness)
            {
                updateWellness(-20); // Lowers your wellness
            }
            locationManager.goToBedroom();  // Move to the bedroom
            // run sleep method
        }

        // update later when we lock sleep to late at night
        if(time != 480)
        {
            if (!testingVideoWellness)
            {
                updateHunger(t);
                updateSleep(t);
                updateShower(t);
            }
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
            savedShower = shower;
            savedSleep = sleep;
        }

        // call delegate
        notifyOnTimeChanged(time - t, time);
    }

    public void updateReputation(int r)
    {
        reputation += r;

        if (reputation > 100)
        {
            reputation = 100;
        }
        else if (reputation <= 0)
        {
            reputation = 0;

            if (hasDied)
            {
                killPlayer();
            }
            else
            {
                playInfamyScene();
            }
        }

        notifyOnReputationChange(reputation - r, reputation);
    }

    public void makeVideo()
    {
        videosMadeToday++;
    }

    public void updateSubscribers(int s)
    {    
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
 
    public void moveLocation(GameObject newLocation, GameObject newCanvas)
    {
        notifyOnLocationChange(location, newLocation);

        // the players current room has changed
        location = newLocation;
        locationCanvas = newCanvas;
    }

    public void updateHunger(float t)
    {
        hunger += t;

        // player is hungry
        if (hungry())
        {
            // display icon
            hungerHUD.enabled = true;

            // enable buttons
        }
        else
        {
            // turn off icon
            hungerHUD.enabled = false;

            // disable buttons
        }

        // for each time jump, lower wellness
        if (hunger > 6 * 60)
        {
            // don't subtract when the player is sleeping

            // catches when you do half of an action before being hungry and half after so you dont loos extra/no wellness
            float over = 6 * 60 - hunger;
            float loss = Mathf.Max(t, over);

            // for every hour you are hungry after the original notification, lower wellness by 5
            updateWellness((int)(-loss * .0833 - .5));
        }
    }

    public void updateShower(float t)
    {
        shower += t;

        // player is dirty
        if (needsShower())
        {
            // display icon
            showerHUD.enabled = true;
        }
        else
        {
            // turn off icon
            showerHUD.enabled = false;
        }

        // for each time jump, lower wellness
        if (shower > 14 * 60)
        {
            // don't subtract when the player is sleeping

            // catches when you do half of an action before being hungry and half after so you dont loos extra/no wellness
            float over = 14 * 60 - shower;
            float loss = Mathf.Max(t, over);

            // for every hour you are dirty after the original notification, lower wellness by 5
            updateWellness((int)(-loss * .0833 - .5));
        }
    }

    public void updateSleep(float t)
    {
        sleep += t;

        // player is tired
        if (tired())
        {
            // display icon
            sleepHUD.enabled = true;
        }
        else
        {
            sleepHUD.enabled = false;
        }

        // for each time jump, lower wellness
        if (sleep > 16 * 60)
        {
            // don't subtract when the player is sleeping

            // catches when you do half of an action before being hungry and half after so you dont loos extra/no wellness
            float over = 16 * 60 - sleep;
            float loss = Mathf.Max(t, over);

            // for every hour you are hungry after the original notification, lower wellness by 5
            updateWellness((int)(-loss * .0833 - .5));
        }
    }

    // methods
    private void killPlayer()
    {
        // reset stats
        money = 100;
        reputation = 50;
        subscribers = 1000;
        wellness = 70;
        ending = 0;
        day = 1;
        hunger = 0;

        // game over
        notificationManager.showNotification("You made some mistakes...");
        splashScreenManager.openSplashScreen("Game over");
        locationManager.goToGameOver();
    }

    private void playHospitalScene()
    {
        // set stats
        hasDied = true;
        updateWellness(50);

        // half money
        money /= 2;
        notifyOnMoneyChange(money * 2, money);

        // call hospital scene to ovveride current splash screen
        notificationManager.showNotification("You haven't been taking care of yourself...");
        splashScreenManager.openSplashScreen("Hospital");
        locationManager.goToBedroom();
    }

    private void playInfamyScene()
    {
        // set stats
        hasDied = true;
        updateReputation(50);

        // call hospital scene to ovveride current splash screen
        notificationManager.showNotification("You haven't been keeping up with your work...");
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
        sleep = savedSleep;
        shower = savedShower;
        videosMadeToday = 0;

        // move location
        locationManager.goToBedroom();

        // reset HUD + splash screen
        splashScreenManager.openSplashScreen("reset");

        // call delegates
        notifyOnWellnessChanged(wellness, wellness);
        notifyOnTimeChanged(time, time);
        notifyOnMoneyChange(money, money);
        notifyOnReputationChange(reputation, reputation);
        notifyOnLocationChange(location.gameObject, location);
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
    public void addOnLocationChange(changeLocation changeLocation)
    {
        onLocationChanged += changeLocation;
    }
    private void notifyOnLocationChange(GameObject oldLocation, GameObject newLocation)
    {
        onLocationChanged(oldLocation, newLocation);
    }
}