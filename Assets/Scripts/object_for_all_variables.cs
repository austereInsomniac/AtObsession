using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEditor.VersionControl;
//using System.Random;
using UnityEngine.XR;
using JetBrains.Annotations;

class ActionVariables
{
    int wellness = 0;
    int time = 0;
    double money = 0.00;

    public ActionVariables(int changeWellness, int changeTime, double changeMoney)
    {
        wellness = changeWellness;
        time = changeTime;
        money = changeMoney;

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

    public void Hunger()
    {

    }

    public void Time(int oldTime, int newTime)
    {

    }
}




public class object_for_all_variables : MonoBehaviour
{


    List<ActionVariables> listVariables;
    Dictionary<string, ActionVariables> activities;

    ActionVariables action;

    GameObject player;

    public int wellness = 0;

    System.Random rand = new System.Random();

    

    //int SkipToMorning(int currentTime)
    //{
    //    currentTime = action.getTime();

    //}
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

    int RandonmWellness()
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
        else {
            return 0;
        }
   
    }
    //void Awake()
    //{
    //    // "Player" is the name of the Game Object with the game_state script
       
    //    player = GameObject.Find("Player");
    //    player.GetComponent<game_state>().updateTime(activities[]); 
    //}

    public void doAnAction(int index)
    {
        // update each statistic
        game_state method = player.GetComponent<game_state>();
        method.updateWellness(action.getWellness());
        method.updateTime(action.getTime());
        method.updateMoney(action.getMoney());
    }

    public void Action(string activityName)
    {
        // update each statistic
        game_state method = player.GetComponent<game_state>();
        ActionVariables activity = activities[activityName];
        method.updateWellness(activity.getWellness());
        method.updateTime(activity.getTime());
        method.updateMoney(activity.getMoney());
    }
    // Start is called before the first frame update
    void Start()
    {

        activities = new Dictionary<string, ActionVariables>();
        activities.Add("Cook food", new ActionVariables(10, 30, 5.00));//hunger
        activities.Add("Eat at a restaurant", new ActionVariables(10, 60, 25.00));//hunger
        activities.Add("Eat a snack", new ActionVariables(10, 5, 0.00));//hunger
        activities.Add("Do household chores", new ActionVariables(8, 15, 0.00));
        activities.Add("Go to sleep", new ActionVariables(30, 480 - action.getTime(), 0.00));//use the equation to adjust the wellness 
        activities.Add("Take a nap", new ActionVariables(20, 120, 0.00));
        activities.Add("Forced Sleep", new ActionVariables(-5, 480 - 120, 0.00));
        activities.Add("Freshen up", new ActionVariables(3, 5, 0.00));
        activities.Add("Take a shower", new ActionVariables(8, 20, 0.00));
        activities.Add("Bubble bath", new ActionVariables(12, 45, 0.00));
        activities.Add("Do household chores", new ActionVariables(8, 15, 0.00));
        activities.Add("Exercise at the gym", new ActionVariables(8, RandomTimeBig(), 15.00));
        activities.Add("Hang out with friends", new ActionVariables(RandonmWellness(), RandomTimeBig(), 0.00));
        activities.Add("Go for a walk", new ActionVariables(10, 25, 0));
        activities.Add("Watch TV", new ActionVariables(8, RandomTimeSmall(), 0.00));
        activities.Add("Exercise at home", new ActionVariables(8, 20, 0.00));
    }
}
