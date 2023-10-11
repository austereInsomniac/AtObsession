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
}




public class object_for_all_variables : MonoBehaviour
{
    List<ActionVariables> listVariables;
    Dictionary<string, ActionVariables> activities;

    ActionVariables action;

    GameObject player;

    System.Random rand = new System.Random();
    int RandonmInteger()
    {
        int randomNumber;

        randomNumber = rand.Next(60 / 5, 120 / 5);
        randomNumber *= 5;
        return randomNumber;
    }

    void Awake()
    {
        // "Player" is the name of the Game Object with the game_state script
        player = GameObject.Find("Player");
    }

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

        listVariables = new List<ActionVariables>
        {
            new ActionVariables(10, 30, 5.00),//cook food
        };
        
        activities = new Dictionary<string, ActionVariables>();
        activities.Add("Cook Food", new ActionVariables(10, 30, 5.00));

    }
}
