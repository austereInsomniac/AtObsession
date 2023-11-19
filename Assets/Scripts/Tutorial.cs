using PlasticGui.WorkspaceWindow;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;
class TutorialPopUps
{
    string text;
    bool OffOrOn;

    public TutorialPopUps(bool OffOrOn_, string text_)
    {
        text = text_;
        OffOrOn = OffOrOn_;
    }

    public string getText()
    {
        return text;
    }

    public bool getOffOrOn()
    {
        return OffOrOn;
    }
}
public class Tutorial : MonoBehaviour
{
    int count = 0;

    GameObject player = GameObject.Find("Player");
    GameObject livingRoom = GameObject.Find("Living Room");
    Vector3 livingRoomLocation = new Vector3(-1000, 0, 0);


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

    //Gets the showNotification method so the notifaction will pop up
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
            if (GetComponent<game_state>().getTime() == 480 && count == 0)
            {
                //introduce player to wellness, add the arrow asset
                notificationPopUp("This is your wellness which will rasie and lower based your time and the activities you do");
                count++;
            }

            //need pop up telling the player to go to the living room and introducing them to the clickable objects
            else if (count == 1)
            {
                notificationPopUp("If you hover over the arrow it will have a yellow highlight.\n" +
                        "These highlights tell you what can and can't be clicked on.\n" +
                        "Hover over and click on the arrow which will take you to the living room.");
                count++;
            }

            else if (livingRoom.transform.position == livingRoomLocation)
            {
                notificationPopUp("This is the living room where the great majority of your activities are.\n " +
                    "Hover over the broom and click on it.");
               
                if (count == 2)
                {
                    notificationPopUp("If you hover over and click on the broom you will notice a menu that pops up.\n" +
                        "You can either click on do chores, to do that task or exit the menu.\n" +
                        "Click on the broom and do the task.");
                    count++;
                }
                else if (count == 3)
                {
                    notificationPopUp("Notice that your wellness went up from doing that task in the top right\n" +
                        "There are other actions around the house that will rasie or lower your wellness.\n" +
                        "Anything that is highlighted yellow when you hover over it can be clicked on.\n" +
                        "Explore for the rest of day 1 and see what different activities you can do.");
                }
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
