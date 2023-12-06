using PlasticGui.WorkspaceWindow;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Codice.Client.BaseCommands;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.Graphs;

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
    GameObject streaming;
    GameObject tv;
    GameObject sleep;
    GameObject wellnessAndRep;

    //Gets current day
    public int getCurrentDay()
    {
        return GetComponent<game_state>().getDay();
    }

    //turns the box collider on the computer off
    public void computerNotInteractable()
    {
        GameObject computer = GameObject.Find("Monitor");//rename computer object
        computer.GetComponent<BoxCollider>().enabled = false;
    }

    //turns the box collider on the computer on
    public void computerInteractable()
    {
        GameObject computer = GameObject.Find("Monitor");
        computer.GetComponent<BoxCollider>().enabled = true;
    }

    //Gets the showNotification method so the notifaction will pop up
    public void notificationPopUp(string message)
    {
        notification.GetComponent<notification_manager>().queNotification(message);
    }

    public void notificationShow(string message)
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

    public void shoppingNotInteractable()
    {
        GameObject shoppingButton = GameObject.Find("Go_Shopping");
        shoppingButton.GetComponent<UnityEngine.UI.Button>().enabled = false;
    }

    public void shoppingInteractable()
    {
        GameObject shoppingButton = GameObject.Find("Go_Shopping");
        shoppingButton.GetComponent<UnityEngine.UI.Button>().enabled = true;
    }

    public void emailNotInteractable()
    {
        GameObject emailButton = GameObject.Find("Check_Email");
        emailButton.GetComponent<UnityEngine.UI.Button>().enabled = false;
    }

    public void emailInteractable()
    {
        GameObject emailButton = GameObject.Find("Check_Email");
        emailButton.GetComponent<UnityEngine.UI.Button>().enabled = true;
    }

    public void chitterNotInteractable()
    {
        GameObject chitterButton = GameObject.Find("Check_Chitter");
        chitterButton.GetComponent<UnityEngine.UI.Button>().enabled = false;
    }

    public void chitterInteractable()
    {
        GameObject chitterButton = GameObject.Find("Check_Email");
        chitterButton.GetComponent<UnityEngine.UI.Button>().enabled = true;
    }

    public void videoInteractable()
    {
        GameObject videoButton = GameObject.Find("Make_Video");
        videoButton.GetComponent<UnityEngine.UI.Button>().enabled = true; 
    }

    public void videoNotInteractable()
    {
        GameObject emailButton = GameObject.Find("Check_Email");
        emailButton.GetComponent<UnityEngine.UI.Button>().enabled = false;
    }

    public void toggleBoxCollider(string item, bool key)
    {

        GameObject gameObject = GameObject.Find(item);
        if (gameObject != null)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = key;
        }

    }

    public void toggleButton(string item, bool key)
    {
        GameObject gameObject = GameObject.Find(item);
        if (gameObject != null)
        {
            gameObject.GetComponent<UnityEngine.UI.Button>().enabled = key;
        }
    }

    public void doorsInteractable(bool key)
    {
        toggleBoxCollider("Kitchen Door", key);
        toggleBoxCollider("Kitchen Door (1)", key);
        toggleBoxCollider("Bedroom Door", key);
        toggleBoxCollider("Bathroom Door", key);
        toggleBoxCollider("Exit Bedroom", key);
        toggleBoxCollider("Kitchen Arrow", key);
        toggleBoxCollider("Front Door", key);
        toggleBoxCollider("Exit Arrow", key);
        toggleBoxCollider("TV", key);
        toggleBoxCollider("Front Door", key);
        toggleBoxCollider("Broom", key);
        toggleBoxCollider("Workout", key);
        toggleBoxCollider("Bed", key);
        toggleBoxCollider("Lamp", key);
        toggleBoxCollider("Fridge", key);
        toggleBoxCollider("Oven", key);



        //kitchenDoorDay.GetComponent<BoxCollider2D>().enabled = false;
        ////kitchenDoorNight.GetComponent<BoxCollider2D>().enabled = false;
        //bedroomDoor.GetComponent<BoxCollider2D>().enabled = false;
        //bathroomDoor.GetComponent<BoxCollider2D>().enabled = false;
        //bedroomArrow.GetComponent<BoxCollider2D>().enabled = false;
        //kitchenArrow.GetComponent<BoxCollider2D>().enabled = false;
        //frontDoor.GetComponent<BoxCollider2D>().enabled = false;
        //bathroomArrow.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void wellnessAndRepHighlightEnabled(GameObject key)
    {
        key.SetActive(true);
    }

    public void wellnessAndRepHighlightDisabled(GameObject key)
    {
        key.SetActive(false);
    }



    //public void doorsInteractable()
    //{
    //toggleBoxCollider("Kitchen Door", true);
    //toggleBoxCollider("Kitchen Door (1)", true);
    //toggleBoxCollider("Bedroom Door", true);
    //toggleBoxCollider("Bathroom Door", true);
    //toggleBoxCollider("Exit Bedroom", true);
    //toggleBoxCollider("Kitchen Arrow", true);
    //toggleBoxCollider("Front Door", true);
    //toggleBoxCollider("Exit Arrow", true);

    //// kitchenDoorDay.GetComponent<BoxCollider2D>().enabled = true;
    //    //kitchenDoorNight.GetComponent<BoxCollider2D>().enabled = true;
    //    bedroomDoor.GetComponent<BoxCollider2D>().enabled = true;
    //    bathroomDoor.GetComponent<BoxCollider2D>().enabled = true;
    //    bedroomArrow.GetComponent<BoxCollider2D>().enabled = true;
    //    kitchenArrow.GetComponent<BoxCollider2D>().enabled = true;
    //    frontDoor.GetComponent<BoxCollider2D>().enabled = true;
    //    bathroomArrow.GetComponent<BoxCollider2D>().enabled = true;
    //}


    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player").GetComponent<game_state>();

        startGame = GameObject.Find("Start Game");
        startGame.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => buttonClicked());

        videoCreation = GameObject.Find("Make_Video");
        videoCreation.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => buttonClicked());

        streaming = GameObject.Find("Check_Tritch");
        streaming.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => buttonClicked());

        tv = GameObject.Find("Watch TV");
        tv.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => buttonClicked());

        sleep = GameObject.Find("Go to sleep");
        sleep.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => buttonClicked());

        //wellnessAndRep = GameObject.Find("");

    }

    // Update is called once per frame
    void Update()
    {
        //fix the doorsInteractable where the player can click on other buttons while the tutorial is up
        //Fix the hunry, tired, and need to shower if statements as the notification doesn't pop up at the right time
        //Add asset highlights for the UI
        if (getCurrentDay() == 1 || (getCurrentDay() ==2 && player.getTime() < 8 * 60) )
        {
            computerNotInteractable();
            if (buttonClickedOn == true && count == 0)
            {
                //introduce player to wellness, add the arrow asset
                //doorsInteractable(false);
                notificationShow("Welcome streamer.");
                setButtonClickedToFalse();
                //wellnessAndRepHighlightDisabled(wellnessAndRep);
                count++;

            }

            else if (count == 1)
            {
                
                notificationPopUp("If you hover over an object and it has a yellow highlight around it, it is clickable.");

                count++;

            }

            else if (count == 2 )
            {
                notificationPopUp("Hover over and click on something. This will open a menu where you can do something. Try it out.");
                
                //doorsInteractable(true);
                count++;
                
            }

            else if (buttonClickedOn == true && count == 3)
            {
                //doorsInteractable(false);

                notificationPopUp("Notice you feel better afterwards. That means your wellness went up.");

                //wellnessAndRepHighlightEnabled(wellnessAndRep);

                notificationPopUp("The pink bar is your wellness which will raise and lower based on the activities you do.");

                notificationPopUp("Explore your apartment and find the activities you can do to raise and lower your wellness.");

                //doorsInteractable(true);

                setButtonClickedToFalse();
                
                count++;
       
            }

            else if (player.hungry() && count == 4)
            {
                //doorsInteractable(false);

                notificationShow("This symbol in the bottom right means you are hungry.");

                notificationPopUp("You can find things to eat in the kitchen.");

                notificationPopUp("You can also find a place to eat at the front door by going out to eat.");

                //doorsInteractable(true);

                count++;
            }

            else if (player.needsShower() && count == 5)
            {
                //doorsInteractable(false);
                
                notificationShow("This symbol in the bottom right means you need to shower. Go to the bathroom to clean up.");

                notificationPopUp("You can freshen up, take a bubble bath, or take a shower. These will all raise your cleanliness");

                //doorsInteractable (true);

                count++;

            }

            else if (player.tired() && count == 6)
            {
                //doorsInteractable(false);

                notificationShow("This symbol in the bottom right means you're tired and need to go to sleep.");

                notificationPopUp("You can go to bed by going into the bedroom and clicking on the bed.");

                //doorsInteractable(true);
                count++;
            }
         }
 
        //test and fix this day
        else if (getCurrentDay() == 2 || (getCurrentDay() == 3 && player.getTime() < 8 * 60))
        {
            count = 0;
            computerInteractable();
            shoppingNotInteractable();
            emailNotInteractable();
            chitterNotInteractable();
            GameObject computerRoom = GameObject.Find("Computer");
            Vector3 computerLocation = new Vector3(-1000, 0, 0);

            if (buttonClickedOn == true)
            {

                notificationPopUp("For this day you'll be introduced to content creation\n."
                    + "Hover over and click on the computer");
                setButtonClickedToFalse();
                count++;
            }

            else if (computerRoom.transform.position == computerLocation && count == 1)
            {
                notificationPopUp("The video creation is where you'll make your money so you can upgrade your set up.\n" +
                    "Hover over and click on the play button in the top left, this is your content creation.");
                count++;
            }

            else if (buttonClickedOn == true && count == 2)
            {
                notificationPopUp("This is the video creation app, where you can create videos varying in quality." +
                    "The higher the quality, the more money and subscribers you'll get but it'll cost more time and wellness.");
                setButtonClickedToFalse();
                count++;
            }

            else if (computerRoom.transform.position == computerLocation && count == 3)
            {
                notificationPopUp("The streaming app allows you to stream content.");
                count++;
            }

            else if (buttonClickedOn == true && count == 4) //fix this 
            {
                notificationPopUp("This is the streaming service.");
                setButtonClickedToFalse();
                count++;
            }

            else if (count == 5)
            {
                notificationPopUp("Explore making videos and streaming.");
            }
        }

        //test and fix this day
        else if (getCurrentDay() == 3 || (getCurrentDay() == 4 && player.getTime() < 8 * 60))
        {
            count = 0;
            shoppingInteractable();
            emailInteractable();
            chitterInteractable();
            GameObject computerRoom = GameObject.Find("Computer");
            Vector3 computerLocation = new Vector3(-1000, 0, 0);

            if (buttonClickedOn == true)
            {
                notificationPopUp("Today you'll learn about the social media and email within the computer.");
                count++;
            }

            else if ((computerRoom.transform.position == computerLocation && count == 1))
            {
                notificationPopUp("The email and social media apps is the way you can communicate and find out whats going on.");
                count++;
            }

            else if (buttonClickedOn == true && count == 2) // fix this
            {
                notificationPopUp("This is the shopping app where you can buy things.");
                count++;
            }

            else if (buttonClickedOn == true && count == 3)
            {
                notificationPopUp("This is the email app where you can see the emails you have.");
                setButtonClickedToFalse();
            }

            else if (buttonClickedOn == true && count == 4)
            {
                notificationPopUp("This is chitter which you can check social media.");
                setButtonClickedToFalse();
            }

            else if (count == 5)
            {
                notificationPopUp("Explore these new feartures and get use to them");
            }
   

        }
    }
}
