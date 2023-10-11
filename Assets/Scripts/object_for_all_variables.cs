using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class ActivityVariables
{
    int wellness;
    int time;
    int reputation;
    int subscribers;
    int end;
    double money;
    public ActivityVariables(int changeWellness, int changeTime, int changeRep, int changeSubs, int changeEnd, double changeMoney)
    {
        wellness = changeWellness;
        time = changeTime;
        reputation = changeRep;
        subscribers = changeSubs;
        end = changeEnd;
        money = changeMoney;
    }
}
public class object_for_all_variables : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
