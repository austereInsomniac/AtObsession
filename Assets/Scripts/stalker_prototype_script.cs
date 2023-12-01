using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StalkerChoice
{
    public string choiceText;
    public string choiceNotification;
    public int wellnessChange;
    public int endingChange;
    public int reputationChange;

    public StalkerChoice(string text, string notification, int wellness, int ending, int reputation)
    {
        choiceText = text;
        choiceNotification = notification;
        wellnessChange = wellness;
        endingChange = ending;
        reputationChange = reputation;
    }
}

public class StalkerEvents
{
    int wellness;
    int eventNumber;
    int ending;
    int reputation;
    string eventMessage;
    string eventLocation;
    List<StalkerChoice> choices;

    public StalkerEvents(string changeMessage, string location, int changeWellness, int newEventNumber, int changeEnding, int changeRep)
    {
        wellness = changeWellness;
        eventNumber = newEventNumber;
        ending = changeEnding;
        reputation = changeRep;
        eventMessage = changeMessage;
        eventLocation = location;
        choices = new List<StalkerChoice>();
    }

    public int getWellness() { return wellness; }
    public int getEventNumber() { return eventNumber; }
    public int getEnding() { return ending; }
    public int getReputation() { return reputation; }
    public string getEventMessage() { return eventMessage; }
    public string getEventLocation() {  return eventLocation; }

    public void AddChoice(string text, string notification, int wellness, int ending, int reputation)
    {
        choices.Add(new StalkerChoice(text, notification, wellness, ending, reputation));
    }

    public List<StalkerChoice> GetChoices()
    {
        return choices;
    }

    // New methods for testing
    public StalkerChoice getChoice1()
    {
        if (choices.Count > 0)
        {
            return choices[0];
        }
        return null;
    }

    public StalkerChoice getChoice2()
    {
        if (choices.Count > 1)
        {
            return choices[1];
        }
        return null;
    }

    public StalkerChoice getChoice3()
    {
        if (choices.Count > 2)
        {
            return choices[2];
        }
        return null;
    }
}

public class stalker_prototype_script : MonoBehaviour
{
    private bool isOn = true;
    private bool isEndingEvent = false;
    List<string> eventKeys;
    Dictionary<string, StalkerEvents> stalkerEvents;
    private StalkerEvents eventHappening;

    private int eventNum;
    private int pendingEvent = -1; // Set to -1 to indicate no pending event
    private int eventCount = 0;

    private game_state player;
    GameObject stalkerEventHandler;
    TMP_Text choiceText;
    private GameObject choice1;
    private GameObject choice2;
    private GameObject choice3;
    TMP_Text choice1Text;
    TMP_Text choice2Text;
    TMP_Text choice3Text;

    private move_location move;
    public notification_manager notification;
    private swap_assets swap;
    private splash_screen_manager splashScreenManager;

    // Start is called before the first frame update
    void Start()
    {
        // "Player" is the name of the Game Object with the game_state script
        player = GameObject.Find("Player").GetComponent<game_state>();
        move = GetComponent<move_location>();
        stalkerEventHandler = GameObject.Find("Stalker Event Handler");
        stalkerEventHandler.SetActive(true);
        choiceText = GameObject.Find("ChoiceText").GetComponent<TMP_Text>();
        choice1 = GameObject.Find("Choice1");
        choice2 = GameObject.Find("Choice2");
        choice3 = GameObject.Find("Choice3");
        stalkerEventHandler.SetActive(false);
        splashScreenManager = GetComponent<splash_screen_manager>();

        stalkerEvents = new Dictionary<string, StalkerEvents>();

        StalkerEvents emailEvent = new StalkerEvents("I got a weird email...", "Bedroom", 0, 1, 1, -1);
        emailEvent.AddChoice("Interact", "I clicked on the email and clicked a weird link", -3, -1, 0);
        emailEvent.AddChoice("Ignore", "I ignored the email", -1, 0, 0);
        emailEvent.AddChoice("Report", "I reported the email as it seemed suspicious", -1, 1, 5);
        stalkerEvents.Add("Email", emailEvent);

        // Knocking on window event
        StalkerEvents knockingEvent = new StalkerEvents("There's something at the window!", "Bedroom", 10, 2, 1, 0);
        knockingEvent.AddChoice("Look", "I didn't see anything but some rustling bushes", -3, -1, 0);
        knockingEvent.AddChoice("Scream", "Whatever it was it's gone", -1, 0, 0);
        knockingEvent.AddChoice("Ignore", "It's probably nothing", -1, 1, 0);
        stalkerEvents.Add("Knocking on window", knockingEvent);

        // Suspicious gift event
        StalkerEvents giftEvent = new StalkerEvents("I got a weird gift in the mail...", "Living room", 10, 3, 1, 0);
        giftEvent.AddChoice("Open it", "I opened the gift and found something weird", -3, -1, 0);
        giftEvent.AddChoice("Leave it", "I left the gift untouched", -1, 0, 0);
        giftEvent.AddChoice("Send back", "I sent the gift back", -1, 1, 0);
        stalkerEvents.Add("Suspicious gift", giftEvent);

        // Window figure event
        StalkerEvents windowEvent = new StalkerEvents("Something's outside the window!", "Kitchen", 10, 4, 1, 0);
        windowEvent.AddChoice("Look out the window", "I looked outside", -3, -1, 0);
        windowEvent.AddChoice("Scream", "I screamed and they ran away", -1, 0, 0);
        windowEvent.AddChoice("Avoid", "I looked away", -1, 1, 0);
        stalkerEvents.Add("Window figure", windowEvent);

        // Fan game event
        StalkerEvents fanGameEvent = new StalkerEvents("A fan asked me to play a game they sent!", "Bedroom", 10, 5, 1, -1);
        fanGameEvent.AddChoice("Play the game", "I played the fan's game and ended up getting a virus on my computer", -3, -1, 0);
        fanGameEvent.AddChoice("Ignore them", "I ignored the fan's request and kept with my regular schedule", -1, 0, 0);
        fanGameEvent.AddChoice("Decline", "I declined to play", -1, 1, 5);
        stalkerEvents.Add("Fan game", fanGameEvent);

        // Unknown call event
        StalkerEvents unknownCallEvent = new StalkerEvents("The phone is ringing! It's an unknown number...", "Any", 10, 6, 1, 0);
        unknownCallEvent.AddChoice("Answer the call", "I answered the unknown call and heard someone breathing heavily", -3, -1, 0);
        unknownCallEvent.AddChoice("Ignore", "I ignored the call and nothing happened", -1, 0, 0);
        unknownCallEvent.AddChoice("Decline", "I declined the call", -1, 1, 0);
        stalkerEvents.Add("Unknown call", unknownCallEvent);

        // Uncomfortable comment event
        StalkerEvents commentEvent = new StalkerEvents("Someone left a comment that mentioned a private conversation I had.", "Any", 10, 7, 1, 0);
        commentEvent.AddChoice("Delete the comment", "I deleted the uncomfortable comment", -3, -1, 0);
        commentEvent.AddChoice("Ignore", "I ignored the comment", -1, 0, 0);
        commentEvent.AddChoice("Install cameras", "I installed security cameras to make sure I'm not being watched", -1, 1, 0);
        stalkerEvents.Add("Uncomfortable comment", commentEvent);

        // Banging on door event
        StalkerEvents bangingEvent = new StalkerEvents("There's banging on the door!", "Living room", 10, 8, 5, 0);
        bangingEvent.AddChoice("Call the cops", "I called the police", -3, -1, 0);
        bangingEvent.AddChoice("Try to ignore", "I tried to ignore the banging", -1, 0, 0);
        bangingEvent.AddChoice("Check outside", "I checked outside and I heard some noises coming from the bushes", -1, 1, 0);
        stalkerEvents.Add("Banging on door", bangingEvent);

        // Trapped in bathroom event
        StalkerEvents bathroomEvent = new StalkerEvents("The bathroom door is locked!", "Any", 0, 9, 0, 0);
        stalkerEvents.Add("Trapped in bathroom", bathroomEvent);

        eventKeys = new List<string>(stalkerEvents.Keys);
        eventHappening = stalkerEvents["Email"];

        // Subscribe to the location change event from game_state
        player.addOnLocationChange(OnLocationChanged);
    }

    private void OnLocationChanged(GameObject oldLocation, GameObject newLocation)
    {
        // Check if there is a pending event
        if (pendingEvent != -1)
        {
            string location = stalkerEvents[eventKeys[pendingEvent]].getEventLocation();
            if (newLocation.name == location)
            {
                // Player reached the correct location, proceed with displaying choices
                DisplayChoices(stalkerEvents[eventKeys[pendingEvent]]);
                pendingEvent = -1; // Clear the pending event
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int wellness = player.getWellness();
        int day = player.getDay();
        int time = player.getTime();

        if (isOn)
        {
            if (day == 3 && time >= 17 * 60 && eventCount == 0)
            {
                eventCount++;
                isOn = false;
                TriggerStalkerEvent(3);
            }
            else if (day == 5 && time >= 15 * 60 && eventCount == 1)
            {
                eventCount++;
                isOn = false;
                TriggerStalkerEvent(1);
            }
            else if (day == 7 && time >= 13 * 60 && eventCount == 2)
            {
                eventCount++;
                isOn = false;
                TriggerStalkerEvent(2);
            }
            else if (day == 9 && time >= 16 * 60 && eventCount == 3)
            {
                eventCount++;
                isOn = false;
                TriggerStalkerEvent(0);
            }
            else if (day == 10 && time >= 18 * 60 && eventCount == 4)
            {
                eventCount++;
                isOn = false;
                TriggerStalkerEvent(4);
            }
            else if (day == 11 && time >= 12 * 60 && eventCount == 5)
            {
                eventCount++;
                isOn = false;
                TriggerStalkerEvent(5);
            }
            else if (day == 12 && time >= 17 * 60 && eventCount == 6)
            {
                eventCount++;
                isOn = false;
                TriggerStalkerEvent(6);
            }
            else if (day == 13 && time >= 17.5 * 60 && eventCount == 7)
            {
                eventCount++;
                isOn = false;
                TriggerStalkerEvent(7);
            }
            else if (day == 14 && time >= 14 * 60)
            {
                isOn = false;
                TriggerStalkerEvent(8);
            }
        }
        if (isEndingEvent && Input.anyKeyDown)
        {
            EndGameEvent(8);
        }
    }

    public void setEventNum(int eventNum)
    {
        this.eventNum = eventNum;
    }

    public void TurnOff()
    {
        Debug.Log(isOn);
        if (isOn)
        {
            isOn = false;
            Debug.Log(isOn);
        }
        Debug.Log(isOn);
    }
    public void TurnOn()
    {
        if (!isOn)
        {
            isOn = true;
        }
    }

    public void TriggerStalkerEvent(int numEvent)
    {
        if (numEvent != eventKeys.Count - 1)
        {
            // Handle stalker event logic here.
            string eventKey = eventKeys[numEvent];
            StalkerEvents stalkerEvent = stalkerEvents[eventKey];
            eventHappening = stalkerEvent;
            string eventMessage = stalkerEvent.getEventMessage();
            Debug.Log(eventMessage);

            string location = stalkerEvent.getEventLocation();
            string playerLocation = player.getLocation().name;

            // Check if the player is in the required location
            if (IsPlayerInRequiredLocation(location) || location == "Any")
            {
                DisplayChoices(stalkerEvent);
            }
            else
            {
                eventMessage = "You hear something happening in the " + location + "...";
                if (notification != null)
                {
                    notification.showNotification(eventMessage);
                }
                pendingEvent = numEvent;
            }
        }
        else
        {
            string eventKey = eventKeys[numEvent];
            StalkerEvents stalkerEvent = stalkerEvents[eventKey];
            string eventMessage = stalkerEvent.getEventMessage();
            eventNum = numEvent;

            EndingEvent(stalkerEvent);
        }
    }

    public StalkerEvents getStalkerEvent()
    {
        return eventHappening;
    }

    private bool IsPlayerInRequiredLocation(string requiredLocation)
    {
        string playerLocation = player.getLocation().name;
        return playerLocation == requiredLocation;
    }

    private void DisplayChoices(StalkerEvents stalkerEvent)
    {
        choiceText.text = stalkerEvent.getEventMessage();
        choice1Text = choice1.GetComponentInChildren<TMP_Text>();
        choice2Text = choice2.GetComponentInChildren<TMP_Text>();
        choice3Text = choice3.GetComponentInChildren<TMP_Text>();

        // Get choices from the event
        List<StalkerChoice> choices = stalkerEvent.GetChoices();

        // Option 1
        choice1Text.text = choices.Count > 0 ? choices[0].choiceText : "";
        choice1.GetComponent<Button>().onClick.AddListener(() => HandlePlayerChoice(choices.Count > 0 ? choices[0] : null, stalkerEvent));

        // Option 2
        choice2Text.text = choices.Count > 1 ? choices[1].choiceText : "";
        choice2.GetComponent<Button>().onClick.AddListener(() => HandlePlayerChoice(choices.Count > 1 ? choices[1] : null, stalkerEvent));

        // Option 3
        choice3Text.text = choices.Count > 2 ? choices[2].choiceText : "";
        choice3.GetComponent<Button>().onClick.AddListener(() => HandlePlayerChoice(choices.Count > 2 ? choices[2] : null, stalkerEvent));

        stalkerEventHandler.SetActive(true);
    }

    public void HandlePlayerChoice(StalkerChoice choice, StalkerEvents stalkerEvent)
    {
        if (choice != null)
        {
            choice.wellnessChange *= stalkerEvent.getWellness();
            choice.endingChange *= stalkerEvent.getEnding();
            choice.reputationChange *= stalkerEvent.getReputation();
            // Update player stats
            player.updateWellness(choice.wellnessChange);
            player.updateEnding(choice.endingChange);
            player.updateReputation(choice.reputationChange);

            // Hide the stalker event handler
            stalkerEventHandler.SetActive(false);

            // Show notification
            if (notification != null)
            {
                notification.showNotification(choice.choiceNotification);
            }
        }
        else
        {
            Debug.Log("Invalid Choice");
        }
    }

    private void EndGameEvent(int eventNum)
    {
        if (eventNum == 8)
        {
            splashScreenManager.openSplashScreen("Game over");
            move.goToGameOver();
            stalkerEventHandler.SetActive(false);
        }
    }

    private void EndingEvent(StalkerEvents stalkerEvent)
    {
        move.moveLocation(GameObject.Find("Bathroom"), GameObject.Find("Bathroom Canvas"), player.getLocation(), player.getLocationCanvas());
        stalkerEventHandler.SetActive(true);

        Destroy(choice1);
        Destroy(choice2);
        Destroy(choice3);

        if (player.getEnding() < 0)
        {
            choiceText.text = stalkerEvent.getEventMessage() + "\nBad Ending- The Stalker broke in and found you";
            isEndingEvent = true;
        }
        else if (player.getEnding() >= 0)
        {
            choiceText.text = stalkerEvent.getEventMessage() + "\nGood Ending- You called 911 and the stalker was arrested";
            isEndingEvent = true;
        }
    }
}