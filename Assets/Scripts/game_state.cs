using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_state : MonoBehaviour
{
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

    // delegates 
    public delegate void changeWellness(int oldWellness, int newWellness);
    private changeWellness onWellnessChanged;

    public delegate void changeTime(int oldTime, int newTime);
    private changeTime onTimeChanged;

    public delegate void changeSubscribers(int oldSubscribers, int newSubscribers);
    private changeSubscribers onSubscribersChanged;
   
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
    public void setWellness(int w)
    {
        notifyOnWellnessChanged(wellness, w);
        wellness = w;
        Debug.Log(wellness);
    }

    public void setDay(int d)
    {
        day = d;
    }

    public void setTime(int t)
    {
        notifyOnTimeChanged(time, t);
        time = t;
    }

    public void setReputation(int r)
    {
        reputation = r;
    }

    public void setSubscribers(int s)
    {
        notifyOnSubscribersChange(subscribers, s);
        subscribers = s;
    }

    public void setEnding(int e)
    {
        ending = e;
    }

    public void setMoney(double m)
    {
        money = m;
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


    // remove later
    private void Start()
    {
        addOnTimeChange(doNothing);
        addOnWellnessChange(doNothing);
        addOnSubscribersChange(doNothing);
    }

    private void doNothing(int t, int t2)
    {
        
    }

}


