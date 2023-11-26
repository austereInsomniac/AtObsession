using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StalkerEvents
{
    int wellness;
    int eventNumber;
    int ending;
    int reputation;
    string eventMessage;
    string choice1;
    string choice2;
    string choice3;
    string eventLocation;

    public StalkerEvents(string changeMessage, string option1, string option2, string option3, string location, int changeWellness, int newEventNumber, int changeEnding, int changeRep)
    {
        wellness = changeWellness;
        eventNumber = newEventNumber;
        ending = changeEnding;
        reputation = changeRep;
        eventMessage = changeMessage;
        choice1 = option1;
        choice2 = option2;
        choice3 = option3;
        eventLocation = location;
    }

    public int getWellness() { return wellness; }
    public int getEventNumber() { return eventNumber; }
    public int getEnding() { return ending; }
    public int getReputation() { return reputation; }
    public string getEventMessage() { return eventMessage; }
    public string getChoice1() { return choice1; }
    public string getChoice2() { return choice2; }
    public string getChoice3() { return choice3; }
    public string getEventLocation() {  return eventLocation; }
}

public class stalker_prototype_script : MonoBehaviour
{
    private bool isOn = true;
    List<string> eventKeys;
    Dictionary<string, StalkerEvents> stalkerEvents;
    private StalkerEvents eventHappening;
    private bool isStalkerEvent = false; // Tracks if event is happening
    private float eventDuration = 10; // How long it should last
    private float eventEndTime = 0; // Timestamp when event should end

    private float minTimeBetweenEvents = 20; // Minimum time between events
    private float maxTimeBetweenEvents = 40; // Maximum time between events
    private float nextEventTime = 0; // Timestamp when next event should start

    private int eventNum;
    private int pendingEvent = -1; // Set to -1 to indicate no pending event

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

    private int randomEvent;
    public notification_manager notification;
    private swap_assets swap;

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

        stalkerEvents = new Dictionary<string, StalkerEvents>();
        stalkerEvents.Add("Email", new StalkerEvents("I got a weird email...", "Interact", "Ignore", "Report", "Bedroom", 0, 1, 1, 5));
        stalkerEvents.Add("Knocking on window", new StalkerEvents("There's something at the window!", "Look", "Ignore", "Call 911", "Bedroom", 10, 2, 1, 0));
        stalkerEvents.Add("Suspicious gift", new StalkerEvents("I got a weird gift in the mail...", "Open it", "Leave it", "Send back", "Living room", 10, 3, 1, 0));
        stalkerEvents.Add("Window figure", new StalkerEvents("Something's outside the window!", "Look out the window", "Ignore it", "Call 911", "Kitchen", 10, 4, 1, 0));
        stalkerEvents.Add("Fan game", new StalkerEvents("A fan asked me to play a game they sent!", "Play the game", "Ignore them", "Decline", "Bedroom", 10, 5, 1, 5));
        stalkerEvents.Add("Unknown call", new StalkerEvents("The phone is ringing! it's an unknown number...", "Answer the call", "Ignore", "Decline", "Any", 10, 6, 1, 0));
        stalkerEvents.Add("Uncomfortable comment", new StalkerEvents("Someone left a comment that mentioned a private conversation I had.", "Delete the comment", "Ignore", "Install cameras", "Any", 10, 7, 1, 0));
        stalkerEvents.Add("Banging on door", new StalkerEvents("There's banging on the door!", "Call the cops", "Try to ignore", "Check outside", "Living room", 10, 8, 5, 0));
        stalkerEvents.Add("Trapped in bathroom", new StalkerEvents("The Bathroom door is locked!", "", "", "", "Any", 0, 9, 0, 0));

        eventHappening = stalkerEvents["Email"];
        eventKeys = new List<string>();
        eventKeys.Add("Email");
        eventKeys.Add("Knocking on window");
        eventKeys.Add("Suspicious gift");
        eventKeys.Add("Window figure");
        eventKeys.Add("Fan game");
        eventKeys.Add("Unknown call");
        eventKeys.Add("Uncomfortable comment");
        eventKeys.Add("Banging on door");
        eventKeys.Add("Trapped in bathroom");

        // Subscribe to the location change event from game_state
        player.addOnLocationChange(OnLocationChanged);
        //randomEvent = Random.Range(0, eventKeys.Count - 2);
        // TriggerStalkerEvent(randomEvent); // Start with initial event
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
                isStalkerEvent = true;
                pendingEvent = -1; // Clear the pending event

                // Calculate the time for the next stalker event.
                float randomTimeBetweenEvents = Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
                nextEventTime = Time.time + randomTimeBetweenEvents;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int wellness = player.getWellness();
        int day = player.getDay();
        int time = player.getTime();
        // Check if it's time to end the event.
        if (isStalkerEvent && Time.time >= eventEndTime)
        {
            isOn = true;
            EndStalkerEvent(eventNum);
        }
        if (isOn)
        {

            if (day == 14 && time >= 17 * 60)
            {
                isOn = false;
                TriggerStalkerEvent(8);
            }
            else if (day == 4 && time >= 17 * 60)
            {
                isOn = false;
                TriggerStalkerEvent(1);
            }
            else if (day == 3 && time >= 17 * 60)
            {
                isOn = false;
                TriggerStalkerEvent(3);
            }
            // Check if it's time to trigger a new event.
            else if (!isStalkerEvent && Time.time >= nextEventTime && wellness <= 60 && day >= 2 && time >= 17*60)
            {
                isOn = false;
                if (eventNum == 0)
                {
                    TriggerStalkerEvent(randomEvent);
                }
                else
                {
                    TriggerStalkerEvent(eventNum);
                }
            }
        }
    }
    //public void SpecificStalkerEvent(int EventNum)
    //{
    //    int wellness = player.GetComponent<game_state>().getWellness();
    //    int day = player.GetComponent<game_state>().getDay();
    //    // Check if it's time to end the event.
    //        if (isStalkerEvent && Time.time >= eventEndTime)
    //        {
    //            EndStalkerEvent();
    //        }
    //        // Check if it's time to trigger a new event.
    //        else if (!isStalkerEvent && Time.time >= nextEventTime && wellness <= 60 && day >= 5)
    //        {
    //            TriggerStalkerEvent(EventNum);
    //        }
    //}

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
            eventDuration = Random.Range(5, 20); // Set random event duration between 5 and 20 seconds
            eventEndTime = Time.time + eventDuration; // Calculate when the event should end.

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
                isStalkerEvent = true;

                // Calculate the time for the next stalker event.
                float randomTimeBetweenEvents = Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
                nextEventTime = Time.time + randomTimeBetweenEvents;
            }
            else
            {
                eventMessage = "There's something happening in the " + location + "...";
                if (notification != null)
                {
                    notification.showNotification(eventMessage);
                }
                pendingEvent = numEvent;
            }
        }
        else
        {
            eventDuration = Random.Range(5, 20); // Set random event duration between 5 and 20 seconds
            eventEndTime = Time.time + eventDuration; // Calculate when the event should end.
            string eventKey = eventKeys[eventNum];
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

        // Option 1
        choice1Text.text = stalkerEvent.getChoice1();
        choice1.GetComponent<Button>().onClick.AddListener(() => HandlePlayerChoice(1));

        // Option 2
        choice2Text.text = stalkerEvent.getChoice2();
        choice2.GetComponent<Button>().onClick.AddListener(() => HandlePlayerChoice(2));

        // Option 3
        choice3Text.text = stalkerEvent.getChoice3();
        choice3.GetComponent<Button>().onClick.AddListener(() => HandlePlayerChoice(3));

        stalkerEventHandler.SetActive(true);
    }

    private void HandlePlayerChoice(int choiceNum)
    {
        StalkerEvents stalkerEvent = stalkerEvents[eventKeys[randomEvent]];

        int wellnessChange = stalkerEvent.getWellness();
        int endingChange = stalkerEvent.getEnding();
        int reputationChange = stalkerEvent.getReputation();

        if (choiceNum == 1)
        {
            wellnessChange *= -3;
            endingChange *= -1;
        }
        else if (choiceNum == 2)
        {
            wellnessChange *= -1;
            endingChange *= 0;
        }
        else if (choiceNum == 3)
        {
            wellnessChange *= -1;
            endingChange *= 1;
        }
        else
        {
            Debug.Log("Invalid Choice");
        }

        // Update player stats
        player.updateWellness(wellnessChange);
        player.updateEnding(endingChange);
        player.updateReputation(reputationChange);

        // Hide the stalker event handler
        stalkerEventHandler.SetActive(false);
    }

    private void EndStalkerEvent(int eventNum)
    {
        // Clean up and end the stalker event.
        isStalkerEvent = false;
        Debug.Log("Stalker event ended.");
        if (eventNum == 8 || randomEvent == 8)
        {
            //move.moveLocation(GameObject.Find("Game Over"), GameObject.Find("Game Over Canvas"), player.getLocation(), player.getLocationCanvas());
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
            choiceText.text = stalkerEvent.getEventMessage() + "Bad Ending- The Stalker broke in and found you";
        }
        else if (player.getEnding() >= 0)
        {
            choiceText.text = stalkerEvent.getEventMessage() + "Good Ending- You called 911 and the stalker was arrested";
        }
        isStalkerEvent = true;
    }
}