
using UnityEngine;


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
    int count2 = 0;
    private game_state player;
    bool buttonClickedOn;
    public notification_manager notification;
    GameObject startGame;
    GameObject videoCreation;
    GameObject streaming;
    GameObject tv;
    GameObject sleep;
    GameObject wellnessAndRep;
    GameObject computerScreen;
    GameObject email;
    GameObject socialMedia;
    GameObject shopping;
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

    public GameObject OnMouseDownFindGameObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider == this.GetComponent<BoxCollider2D>())
            {
                return hit.collider.GetComponent<GameObject>();
            }
        }
        return null;
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
        
        email = GameObject.Find("Check_Email");
        email.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => buttonClicked());

        shopping = GameObject.Find("Go_Shopping");
        shopping.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => buttonClicked());

        socialMedia = GameObject.Find("Check_Chitter");
        socialMedia.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => buttonClicked());

        computerScreen = GameObject.Find("Monitor");
        //wellnessAndRep = GameObject.Find("");

    }

    // Update is called once per frame
    void Update()
    {
        //fix the doorsInteractable where the player can click on other buttons while the tutorial is up
        //Fix the hunry, tired, and need to shower if statements as the notification doesn't pop up at the right time
        //Add asset highlights for the UI
        if (getCurrentDay() == 1 || (getCurrentDay() == 2 && player.getTime() < 8 * 60))
        {
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

            else if (count == 2)
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

                notificationPopUp("The blue bar is your reputation which whill raise or lower based on the content you create on the computer.");

                notificationPopUp("Go into the bedroom and click on the computer. The bedroom door is the second door on your right.");

                //doorsInteractable(true);

                setButtonClickedToFalse();

                count++;

            }

            else if (computerScreen.gameObject.name == OnMouseDownFindGameObject().name && count == 4)
            {
                notificationShow("This isthe computer where all of your apps are located.");

                notificationPopUp("Click on the app in the top right.");
                count++;
            }

            else if (buttonClickedOn == true && count == 5)
            {
                notificationPopUp("This is the video creation app, where you can create videos varying in quality." +
                    "The higher the quality, the more money and subscribers you'll get but it'll cost more time and wellness.");
                setButtonClickedToFalse();
                count++;
            }

            else if (buttonClickedOn == true && count == 6) //fix this 
            {
                notificationPopUp("This is the streaming service.");
                setButtonClickedToFalse();
                count++;
            }

            else if (buttonClickedOn == true && count == 7) // fix this
            {
                notificationPopUp("This is the shopping app where you can buy things.");
                count++;
            }

            else if (buttonClickedOn == true && count == 8)
            {
                notificationPopUp("This is the email app where you can see the emails you have.");
                setButtonClickedToFalse();
                count++;
            }

            else if (buttonClickedOn == true && count == 9)
            {
                notificationPopUp("This is chitter which you can check social media.");
                setButtonClickedToFalse();
                count++;
            }

            else if (count == 10)
            {
                notificationPopUp("Explore these new feartures and get use to them");
            }

            else if (player.hungry() && count2 == 0)
            {
                //doorsInteractable(false);

                notificationShow("This symbol in the bottom right means you are hungry.");

                notificationPopUp("You can find things to eat in the kitchen.");

                notificationPopUp("You can also find a place to eat at the front door by going out to eat.");

                //doorsInteractable(true);

                count2++;
            }

            else if (player.needsShower() && count2 == 1)
            {
                //doorsInteractable(false);

                notificationShow("This symbol in the bottom right means you need to shower. Go to the bathroom to clean up.");

                notificationPopUp("You can freshen up, take a bubble bath, or take a shower. These will all raise your cleanliness");

                //doorsInteractable (true);

                count2++;

            }

            else if (player.tired() && count2 == 2)
            {
                //doorsInteractable(false);

                notificationShow("This symbol in the bottom right means you're tired and need to go to sleep.");

                notificationPopUp("You can go to bed by going into the bedroom and clicking on the bed.");

                //doorsInteractable(true);
                count2++;
            }
        }

       
    }
}
