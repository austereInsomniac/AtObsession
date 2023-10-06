using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class force_sleep : MonoBehaviour

{
    [SerializeField]
    private int time;
    [SerializeField]
    private int wellness;
    // Start is called before the first frame update
    public void forceSleep()
    {
        if(time == 4)
        {
            time = 8;
            wellness -= 5;
            //Move to the bedroom

        }

    }

    //for testing only
    public void increaseTime_TESTING()
    {
        if(time == 24)
        {
            time = 1;
        }
        else 
        { 
            time++; 
        }
       
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
