using System.Collections.Generic;
using UnityEngine;
using System.Collections;
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

public class daily_action_storage : MonoBehaviour
{

    [SerializeField]
    private string key;

    List<ActionVariables> listVariables;
    Dictionary<string, ActionVariables> activities;

    ActionVariables action;

    GameObject player;

    System.Random rand = new System.Random();

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
        else {
            return 0;
        }
    }
    void Awake()
    {
        // "Player" is the name of the Game Object with the game_state script
        player = GameObject.Find("Player");
    }

    public void doAction()
    {
        // update each statistic
        ActionVariables activity = activities[key];
        player.GetComponent<game_state>().updateWellness(activity.getWellness());
        player.GetComponent<game_state>().updateTime(activity.getTime());
        player.GetComponent<game_state>().updateMoney(activity.getMoney());
    }

    // Start is called before the first frame update
    void Start()
    {
        activities = new Dictionary<string, ActionVariables>();
        activities.Add("Cook food", new ActionVariables(10, 30, 5.00));//hunger
        activities.Add("Eat at a restaurant", new ActionVariables(10, 60, 25.00));//hunger
        activities.Add("Eat a snack", new ActionVariables(10, 5, 0.00));//hunger
        activities.Add("Go to sleep", new ActionVariables(30, 480 /*- action.getTime()*/, 0.00));//use the equation to adjust the wellness 
        activities.Add("Take a nap", new ActionVariables(20, 120, 0.00));
        activities.Add("Forced Sleep", new ActionVariables(-5, 480 - 120, 0.00));
        activities.Add("Freshen up", new ActionVariables(3, 5, 0.00));
        activities.Add("Take a shower", new ActionVariables(8, 20, 0.00));
        activities.Add("Bubble bath", new ActionVariables(12, 45, 0.00));
        activities.Add("Do chores", new ActionVariables(8, 15, 0.00));
        activities.Add("Go to the gym", new ActionVariables(8, RandomTimeBig(), 15.00));
        activities.Add("Visit friends", new ActionVariables(RandomWellness(), RandomTimeBig(), 0.00));
        activities.Add("Go for a walk", new ActionVariables(10, 25, 0));
        activities.Add("Watch TV", new ActionVariables(8, RandomTimeSmall(), 0.00));
        activities.Add("Lift weights", new ActionVariables(8, 20, 0.00));
    }
}
