using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tutorial : MonoBehaviour
{
    //Gets current day
    public int getCurrentDay()
    {
        return GetComponent<game_state>().getDay();
    }

    //turns the box collider on the computer off
    public void computerNotInteractable()
    {
        GameObject computer = GameObject.Find("Computer");
        computer.GetComponent<BoxCollider2D>().enabled = false;
    }

    //turns the box collider on the computer on
    public void computerInteractable() 
    {
        GameObject computer = GameObject.Find("Computer");
        computer.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void notificationPopUp(string message)
    {
        GetComponent<notification_manager>().showNotification(message);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (getCurrentDay() == 1)
        {
            computerNotInteractable();
            if (GetComponent<game_state>().getTime() == 480)
            {
                //introduce player to wellness
                notificationPopUp("This is your wellness which will rasie and lower based your time and the activities you do");
            }
        }

        else if (getCurrentDay() == 2)
        {
            computerInteractable();
        }

        else if (getCurrentDay() == 3)
        {

        }

        else if (getCurrentDay() == 4)
        {

        }
    }
}
