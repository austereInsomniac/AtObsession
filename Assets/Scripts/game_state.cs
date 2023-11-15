using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_state : MonoBehaviour
{
<<<<<<< HEAD
    private int wellness = 80;
    private int day = 1;
    // time is in terms of minutes since midnight
    // 480 is 8am
    private int time = 480;
    private int reputation = 20;
    private int subscribers = 1000;
    private int ending = 0;

    private double money = 100.00;

    private static int randomizerSeed;

    // hours since you last ate. this will update the UI if it equal to or greater than 4, you are hungry
    private int hunger;
=======
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

>>>>>>> main

    // delegates 
    public delegate void changeWellness(int oldWellness, int newWellness);
    private changeWellness onWellnessChanged;

    public delegate void changeTime(int oldTime, int newTime);
    private changeTime onTimeChanged;

    public delegate void changeSubscribers(int oldSubscribers, int newSubscribers);
    private changeSubscribers onSubscribersChanged;
<<<<<<< HEAD
   
    // getters
    public int getWellness()
    {
        return wellness;
    }

    public int getDay()
    { 
        return day; 
    }

    public int getTime()
    {
        return time;
    }

    public int getReputation()
    {
        return reputation;
    }

    public int getSubscribers()
    {
        return subscribers;
    }

    public int getEnding()
    {
        return ending;
    }

    public double getMoney()
    {
        return money;
    }

    public int getSeed()
    {
        return randomizerSeed;
    }

    // setters
    public void updateWellness(int w)
    {
        notifyOnWellnessChanged(wellness, wellness + w);
        wellness = wellness + w;
    }

    public void updateDay(int d)
    {
        day = day + d;
=======

    public delegate void changeMoney(double oldMoney, double newMoney);
    private changeMoney onMoneyChanged;

    public delegate void changeReputation(int oldReputation, int newReputation);
    private changeReputation onReputationChanged;

    public bool testingVideoWellness;

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

        reputation = 25;
        savedReputation = 25;

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

    private void killPlayer()
    {
        // reset stats
        money = 100;
        reputation = 25;
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

        // half money
        money /= 2;
        notifyOnMoneyChange(money * 2, money);

        // call hospital scene to ovveride current splash screen
        splashScreenManager.openSplashScreen("Hospital");
        locationManager.goToBedroom();
        notificationManager.showNotification("You haven't been taking care of yourself...");
    }

    private void playInfamyScene()
    {
        // set stats
        hasDied = true;
        updateReputation(20);

        // call hospital scene to ovveride current splash screen
        splashScreenManager.openSplashScreen("Hospital");
        locationManager.goToBedroom();
        notificationManager.showNotification("You haven't been keeping up with your work...");
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
        videosMadeToday = 0;

        // move location
        locationManager.goToBedroom();

        // reset HUD + splash screen
        splashScreenManager.openSplashScreen("reset");

        // call delegates
        notifyOnWellnessChanged(wellness, wellness);
        notifyOnTimeChanged(time, time);
        notifyOnMoneyChange(money, money);
        notifyOnSubscribersChange(subscribers, subscribers);
        notifyOnReputationChange(reputation, reputation);
>>>>>>> main
    }

    public void updateTime(int t)
    {
<<<<<<< HEAD
        notifyOnTimeChanged(time, time + t);
        time = time + t;
=======
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
        }

        // call delegate
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
>>>>>>> main
    }

    public void updateReputation(int r)
    {
<<<<<<< HEAD
        reputation = reputation + r;
    }

    public void updateSubscribers(int s)
    {
        notifyOnSubscribersChange(subscribers, subscribers + s);
        subscribers = subscribers + s;
=======
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
        notifyOnSubscribersChange(subscribers - s, subscribers);
>>>>>>> main
    }

    public void updateEnding(int e)
    {
        ending = ending + e;
    }

    public void updateMoney(double m)
    {
        money = money + m;
<<<<<<< HEAD
    }

    public void advanceDay()
    {
        day = day + 1;
        time = 480;
=======
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
>>>>>>> main
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

<<<<<<< HEAD
=======
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
>>>>>>> main

    // remove later
    private void Start()
    {
        addOnTimeChange(doNothing);
        addOnWellnessChange(doNothing);
        addOnSubscribersChange(doNothing);
<<<<<<< HEAD
    }

    private void doNothing(int t, int t2)
    {
        
    }

=======
        addOnMoneyChange(doNothing2);
        addOnReputationChange(doNothing);
    }

    private void doNothing(int t, int t2) { }

    private void doNothing2(double t, double t2) { }
>>>>>>> main
}


