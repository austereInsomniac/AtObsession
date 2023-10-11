using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
//using System.Random;
using UnityEngine;
using UnityEngine.XR;

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

    ActionVariables action;
    
    GameObject player;

    System.Random rand = new System.Random();
    int RandonmInteger()
    {
        int randomNumber;
        
        randomNumber = rand.Next(60/5, 120/5);
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
        player.GetComponent<game_state>().setWellness(player.GetComponent<game_state>().getWellness() + action.getWellness());
        player.GetComponent<game_state>().setTime(player.GetComponent<game_state>().getTime() + action.getTime());
        player.GetComponent<game_state>().setMoney(player.GetComponent<game_state>().getMoney() + action.getMoney());
    }
    // Start is called before the first frame update
    void Start()
    {
        listVariables = new List<ActionVariables>
        {
            new ActionVariables(10, 30, 5.00),
        };
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
