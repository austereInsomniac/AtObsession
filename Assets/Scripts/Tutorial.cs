using PlasticGui.WorkspaceWindow;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
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
    private game_state player;
    bool buttonClickedOn;
    public notification_manager notification;
    GameObject startGame;
    GameObject videoCreation;

    //Gets current day
    public int getCurrentDay()
    {
        return GetComponent<game_state>().getDay();
    }

    //turns the box collider on the computer off
    public void computerNotInteractable()
    {
        GameObject computer = GameObject.Find("Computer_Asset");//rename computer object
        computer.GetComponent<BoxCollider>().enabled = false;
    }

    //turns the box collider on the computer on
    public void computerInteractable()
    {
        GameObject computer = GameObject.Find("Computer_Asset");
        computer.GetComponent<BoxCollider>().enabled = true;
    }

    //Gets the showNotification method so the notifaction will pop up
    public void notificationPopUp(string message)
    {
        notification.GetComponent<notification_manager>().showNotification(message);
    }


    public void buttonClicked()
    {
         buttonClickedOn = true;
    }

    public void setButtonClickedToFalse()
    {
        buttonClickedOn = false;
    }

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player").GetComponent<game_state>();

        startGame = GameObject.Find("Start Game");
        startGame.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => buttonClicked());

        videoCreation = GameObject.Find("Make_Video");
        videoCreation.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => buttonClicked());

    }

    // Update is called once per frame
    void Update()
    {

        if (getCurrentDay() == 1)
        {
            computerNotInteractable();
            if (buttonClickedOn == true)
            {
                Debug.Log("In the first if");
                //introduce player to wellness, add the arrow asset
                notificationPopUp("This is your wellness which will rasie and lower based your time and the activities you do");
                setButtonClickedToFalse();
                count++;
            }

            //need pop up telling the player to go to the living room and introducing them to the clickable objects
            else if (count == 1 && Input.GetMouseButtonDown(0))
            {
                notificationPopUp("If you hover over the a door or an object and it has a yellow highlight around it, it is clickable." +
                        "These highlights tell you what can and can't be clicked on." +
                        "Hover over and click on the broom. Doing so will pop up a menu where you can do a task, do that task. Notice you feel better afterwards which means your wellness goes up.\n"
                        + "Now explore your apartment and find the activities you can do and which ones will raise or lower your wellness." +
                        "Once you're done exploring go to the bed room and click on the bed and choose the go to sleep task.");
                count++;
            }

            else if (count == 2 && Input.GetMouseButtonUp(0)) // fix this if statement so it doesn't go instantly into this
            {
                notificationPopUp("Now explore your apartment and find the activities you can do and which ones will raise or lower your wellness. " +
                    "Once you're done exploring go to the bed room and click on the bed and choose the go to sleep task.");
                count++;
            }
           
         }
 

        else if (getCurrentDay() == 2)
        {
            count = 0;
            computerInteractable();
            GameObject computerRoom = GameObject.Find("Computer");

            if (count == 0)
            {

                notificationPopUp("For this day you'll be introduced to content creation\n."
                    + "Hover over and click on the computer");
                count++;
            }

            else if (player.transform.position == computerRoom.transform.position && count == 1)
            {
                notificationPopUp("The video creation is where you'll make your money so you can upgrade your set up which you'll get introduced to in the next day.\n" +
                    "Hover over and click on the play button in the top left, this is your content creation.");
                count++;
            }

            else if (buttonClickedOn == true)
            {
                notificationPopUp("This is the video creation app, where you can create videos varying in quality." +
                    "The higher the stars the more money and subscribers you'll get but it'll cost more time and wellness. " +
                    "This is vise versa for lower stars.");
                setButtonClickedToFalse();
                count++;
            }
            //else if (count == 2 && Input.GetMouseButtonDown(0))
            //{
            //    notificationPopUp()
            //}
        }

        else if (getCurrentDay() == 3)
        {
            count = 0;
        }

        else if (getCurrentDay() == 4)
        {
            count = 0;
        }
    }
}
