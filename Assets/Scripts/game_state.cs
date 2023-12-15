using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

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
    private Image hungerHUD;
    

    // level of cleanliness
    private float shower;
    private float savedShower;
    private Image showerHUD;

    // level of tiredness
    private float sleep;
    private float savedSleep;
    private Image sleepHUD;

    // current room and its canvas
    private GameObject location;
    private GameObject locationCanvas;

    // other scripts
    private notification_manager notificationManager;
    private move_location locationManager;
    private splash_screen_manager splashScreenManager;

    // filter
    private SpriteRenderer deathFilter;

    // colors
    UnityEngine.Color color1;
    UnityEngine.Color color2;
    UnityEngine.Color colorB1;
    UnityEngine.Color colorB2;

    // testing variables
    public bool testingVideoWellness = false;

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
        location = GameObject.FindWithTag("location").gameObject;
        locationCanvas = GameObject.Find("Main Menu Canvas");
        hungerHUD = GameObject.Find("HungerHUD").GetComponent<Image>();
        sleepHUD = GameObject.Find("SleepHUD").GetComponent<Image>();
        showerHUD = GameObject.FindWithTag("showerHUD").GetComponent<Image>();

        notificationManager = GameObject.FindGameObjectWithTag("notifications").GetComponent<notification_manager>();
        locationManager = GetComponent<move_location>();
        splashScreenManager = GetComponent<splash_screen_manager>();
        deathFilter =GameObject.Find("Color Filter - low wellness (1)").GetComponent<SpriteRenderer>();

        wellness = 70;
        savedWellness = 70;

        day = 1;
        savedDay = 1;

        time = 8*60;

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

        color1 = new Color(1, 1, 1, .3f);
        color2 = new Color(1, 1, 1, 1f);
        colorB1 = new Color(0, 0, 0, 0);
        colorB2 = new Color(0, 0, 0, 1f);

        sleepHUD.color = color1;
        hungerHUD.color = color1;
        showerHUD.color = color1;
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

    public Image getHungerHUD() { return hungerHUD; }

    public Image getShowerHUD() {  return showerHUD; }

    public Image getSleepHUD() {  return sleepHUD; }

    // setters 
    public void updateWellness(int w)
    {
        wellness += w;

        if (wellness > 100)
        {
            notifyOnWellnessChanged(wellness - w, 100);
            wellness = 100;
        }
        else if(wellness <= 20)
        {
            if(wellness <= 0)
            {
                notifyOnWellnessChanged(wellness - w, 0);
                wellness = 0;

                if (hasDied)
                {
                    StartCoroutine(killPlayerWellness());
                }
                else
                {
                    notifyOnWellnessChanged(wellness - w, wellness);
                    StartCoroutine(playHospitalScene());
                }
            }
            else if (!hasDied)
            {
                notifyOnWellnessChanged(wellness - w, wellness);
                StartCoroutine(playHospitalScene());
            }
            else
            {
                notifyOnWellnessChanged(wellness - w, wellness);
            }
        }
        else
        { 
            notifyOnWellnessChanged(wellness - w, wellness);
        }
    }

    public void updateTime(int t)
    {
        // update time
        time += t;

        // lower reputation
        updateReputation((int)(-t/30.0));

        // update day if we hit midnight
        if (time >= 1440)
        {
            day++;
            time -= 1440;

            // update reputaiton if needed
            if(videosMadeToday == 0)
            {
                updateReputation(-50);
            }
            else if(videosMadeToday != 0)
            {
                //updateReputation(-30);
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

        if(time != 480)
        {
            if (!testingVideoWellness)
            {
                // stat updates for not sleeping
                updateHunger(t);
                updateShower(t);
                updateSleep(t);
            }
        }
        else
        {
            if (!testingVideoWellness)
            {
                // stat updates for sleeping
                updateHunger(t / 3);
            }

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
       // if (getTime() == 8 * 60)
        //{
            if (reputation > 100)
            {
                reputation = 100;
                notifyOnReputationChange(reputation - r, reputation);
            }
            else if (reputation <= 20)
            {
                if (!hasDied)
                {
                    notifyOnReputationChange(reputation - r, reputation);
                    playInfamyScene();
                }
                else if (reputation <= 0)
                {
                    reputation = 0;
                    notifyOnReputationChange(reputation - r, reputation);

                    if (hasDied)
                    {
                        StartCoroutine(killPlayerReputation());
                    }
                }
                else
                {
                    notifyOnReputationChange(reputation - r, reputation);
                }
            }
            else
            {
                notifyOnReputationChange(reputation - r, reputation);
            }
        //}
    }

    public void makeVideo()
    {
        videosMadeToday++;
    }

    public void updateSubscribers(int s)
    {    
        subscribers = subscribers + s;

        if(subscribers < 0)
        {
            subscribers = 0;
        }
    }

    public void updateEnding(int e)
    {
        ending = ending + e;
    }

    public void updateMoney(double m)
    {
        money = money + m;

        if(money >= 9999)
        {
            money = 9999;
        }
        else if(money <= 0)
        {
            money = 0;
        }
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

        if (hunger >= 8*60)
        {
            hunger = 8*60;
        }

        // player is hungry
        if (hungry())
        {
            // display icon
            hungerHUD.color = color2;
        }
        else
        {
            // turn off icon
            hungerHUD.color = color1;
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

        if (shower >= 20*60)
        {
            shower = 20 * 60;
        }
        // player is dirty
        if (needsShower())
        {
            // display icon
            showerHUD.color = color2;
        }
        else
        {
            // turn off icon
            showerHUD.color = color1;
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
        if(sleep >= 24 * 60)
        {
            sleep = 24 * 60;
        }

        // player is tired
        if (tired())
        {
            // display icon
            sleepHUD.color = color2;
        }
        else
        {
            sleepHUD.color = color1;
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
    private IEnumerator playHospitalScene()
    {
        // call hospital scene to ovveride current splash screen
        StartCoroutine(notificationManager.holdNotification("\"Ugh... I feel really dizzy...\"", 3));
        StartCoroutine(locationManager.OnButtonClicked());

        float elapsed = Time.deltaTime;
        float duration = Time.deltaTime + 2f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            deathFilter.color = (elapsed / duration) * colorB2;
        }

        yield return new WaitForSeconds(3);

        deathFilter.color = colorB1;

        StartCoroutine(notificationManager.holdNotification("Sigh \"You really need to take better care of yourself. \nNext time we won't be able to help you\"", 3));
        StartCoroutine(splashScreenManager.holdSplashScreen("Hospital", 3));
        locationManager.goToBedroom();

        // set stats
        wellness = 50;
        updateWellness(0);

        // half money
        money = (int)money / 2;
        notifyOnMoneyChange(money * 2, money);

        hasDied = true;
    }

    private IEnumerator killPlayerWellness()
    {
        // game over
        StartCoroutine(notificationManager.holdNotification("\"Ugh... I feel really... really dizzy...\"", 3));
        StartCoroutine(locationManager.OnButtonClicked());
        
        float elapsed = Time.deltaTime;
        float duration = Time.deltaTime + 2f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            deathFilter.color = (elapsed / duration) * colorB2;
        }

        yield return new WaitForSeconds(3);

        deathFilter.color = colorB1;
        StartCoroutine(notificationManager.holdNotification("You didn't take proper care of yourself... \nThis is the end...", 3));
        StartCoroutine(splashScreenManager.holdSplashScreen("Game over", 3));
        locationManager.goToGameOver();

        hasDied = false;

        // reset stats
        money = 100;
        updateMoney(0);
        reputation = 50;
        updateReputation(0);
        subscribers = 1000;
        updateSubscribers(0);
        wellness = 70;
        updateWellness(0);
        ending = 0;
        day = 1;
        time = 8 * 60;
        hunger = 0;
        updateHunger(0);
        shower = 0;
        updateShower(0);
        sleep = 0;
        updateSleep(0);
    }

    private void playInfamyScene()
    {
        // call notif
        notificationManager.showNotification("\"My reputation is terrible. I need to raise it, or the next time it drops so low I'll give up!\"");
        locationManager.goToBedroom();

        // set stats
        hasDied = true;
        reputation = 50;
        updateReputation(0);

        // half money
        money = (int)money / 2;
        notifyOnMoneyChange(money * 2, money);
    }

    private IEnumerator killPlayerReputation()
    {
        // game over
        StartCoroutine(notificationManager.holdNotification("\"Ugh... I can't take it anymore...\"", 3));
        StartCoroutine(locationManager.OnButtonClicked());

        float elapsed = Time.deltaTime;
        float duration = Time.deltaTime + 2f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            deathFilter.color = (elapsed / duration) * colorB2;
        }

        yield return new WaitForSeconds(3);

        deathFilter.color = colorB1;
        StartCoroutine(notificationManager.holdNotification("\"My reputation is destroyed! I can't do this anymore!\"", 3));
        StartCoroutine(splashScreenManager.holdSplashScreen("Game over", 3));
        locationManager.goToGameOver();

        hasDied = false;

        // reset stats
        money = 100;
        updateMoney(0);
        reputation = 50;
        updateReputation(0);
        subscribers = 1000;
        updateSubscribers(0);
        wellness = 70;
        updateWellness(0);
        ending = 0;
        day = 1;
        time = 8 * 60;
        hunger = 0;
        updateHunger(0);
        shower = 0;
        updateShower(0);
        sleep = 0;
        updateSleep(0);
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
        notificationManager.menuBlocker.enabled = false;
        notificationManager.menuCollider.enabled = false;
        notificationManager.hudCanvas.blocksRaycasts = false;

        // call delegates
        notifyOnWellnessChanged(wellness, wellness);
        notifyOnTimeChanged(time, time);
        notifyOnMoneyChange(money, money);
        notifyOnReputationChange(reputation, reputation);
        notifyOnLocationChange(location, location);
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