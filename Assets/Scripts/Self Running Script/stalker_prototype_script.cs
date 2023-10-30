using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class StalkerEvents
{
    int wellness;
    int eventNumber;
    int ending;

    public StalkerEvents(int changeWellness,  int newEventNumber, int changeEnding)
    {
        wellness = changeWellness;
        eventNumber = newEventNumber;
        ending = changeEnding;
    }
}
public class stalker_prototype_script : MonoBehaviour
{
    private bool isOn = true;
    List<string> eventKeys;
    Dictionary<string, StalkerEvents> stalkerEvents;
    private bool isStalkerEvent = false; // Tracks if event is happening
    private float eventDuration = 10; // How long it should last
    private float eventEndTime = 0; // Timestamp when event should end

    private float minTimeBetweenEvents = 10; // Minimum time between events
    private float maxTimeBetweenEvents = 30; // Maximum time between events
    private float nextEventTime = 0; // Timestamp when next event should start

    private int eventNum;

    GameObject player;
    
    private int randomEvent;
    public notification_script notification;
    // Start is called before the first frame update
    void Start()
    {
        // "Player" is the name of the Game Object with the game_state script
        player = GameObject.Find("Player");
        stalkerEvents = new Dictionary<string, StalkerEvents>();
        
        stalkerEvents.Add("Email", new StalkerEvents(0, 1, 1));
        stalkerEvents.Add("Knock on door", new StalkerEvents(1, 2, 1));
        stalkerEvents.Add("Gift in the mail", new StalkerEvents(1, 3, 1));
        stalkerEvents.Add("Suspicious friend", new StalkerEvents(1, 4, 1));
        stalkerEvents.Add("Window figure", new StalkerEvents(1, 5, 1));

        eventKeys = new List<string>();
        eventKeys.Add("Email");
        eventKeys.Add("Knock on door");
        eventKeys.Add("Gift in the mail");
        eventKeys.Add("Suspicious friend");
        eventKeys.Add("Window Figure");
        randomEvent = Random.Range(1, eventKeys.Count);
       // TriggerStalkerEvent(randomEvent); // Start with initial event
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        Debug.Log(eventNum);
        int wellness = player.GetComponent<game_state>().getWellness();
        int day = player.GetComponent<game_state>().getDay();
        // Check if it's time to end the event.
        if (isOn)
        {
            if (isStalkerEvent && Time.time >= eventEndTime)
            {
                EndStalkerEvent();
            }
            // Check if it's time to trigger a new event.
            else if (!isStalkerEvent && Time.time >= nextEventTime && wellness <= 60 && day >= 5)
            {
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
    private void TriggerStalkerEvent(int EventNumber)
    {
        // Handle stalker event logic here.
        eventDuration = Random.Range(5, 20); // Set random event duration between 5 and 20 seconds
        eventEndTime = Time.time + eventDuration; // Calculate when the event should end.
        string eventMessage = "Stalker event " + EventNumber + " is active!";
        if (notification != null)
        {
            notification.ShowNotifications(eventMessage);
        }
        Debug.Log(eventMessage);

        // Calculate the time for the next stalker event.
        float randomTimeBetweenEvents = Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
        nextEventTime = Time.time + randomTimeBetweenEvents;
        isStalkerEvent = true;
    }

    private void EndStalkerEvent()
    {
        // Clean up and end the stalker event.
        isStalkerEvent = false;
        Debug.Log("Stalker event ended.");
    }
}
