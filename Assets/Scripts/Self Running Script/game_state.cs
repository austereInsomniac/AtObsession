using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_state : MonoBehaviour
{
    private int wellness;
    private int reputation;
    private int subscribers;
    private int ending;

    // time is in terms of minutes since midnight - 480 is 8am
    private int time;
    private int day;

    private double money;

    // hours since you last ate. this will update the UI if it equal to or greater than 4, you are hungry
    private int hunger;

    // current room and its canvas
    private GameObject location;
    private GameObject locationCanvas;

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

        wellness = 80;
        day = 1;
        time = 480;
        reputation = 20;
        subscribers = 1000;
        ending = 0;
        money = 100.00;
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

    // setters
    public void updateWellness(int w)
    {
        if (wellness + w >= 100)
        { 
            wellness = 100;
        }
        else if (wellness + w <= 0)
        {
            // die
            wellness = 0;
        }
        else
        {
            wellness += w;
        }
        notifyOnWellnessChanged(wellness - w, wellness);
    }

    public void updateTime(int t)
    {
        time += t;

        if (time >= 1440)
        {
            // update day if we hit midnight
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

        notifyOnTimeChanged(time - t, time);
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


