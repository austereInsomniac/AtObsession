using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalker_prototype_script : MonoBehaviour
{
    private bool isStalkerEvent = false; // Tracks if event is happening
    private float eventDuration = 10; // How long it should last
    private float eventEndTime = 0; // Timestamp when event should end

    private float minTimeBetweenEvents = 10; // Minimum time between events
    private float maxTimeBetweenEvents = 30; // Maximum time between events
    private float nextEventTime = 0; // Timestamp when next event should start

    // Start is called before the first frame update
    void Start()
    {
        TriggerStalkerEvent(); // Start with initial event
    }

    // Update is called once per frame
    void Update()
    {
        // Check if it's time to end the event.
        if (isStalkerEvent && Time.time >= eventEndTime)
        {
            EndStalkerEvent();
        }
        // Check if it's time to trigger a new event.
        else if (!isStalkerEvent && Time.time >= nextEventTime)
        {
            TriggerStalkerEvent();
        }
    }

    private void TriggerStalkerEvent()
    {
        // Handle stalker event logic here.
        isStalkerEvent = true;
        eventDuration = Random.Range(5, 20); // Set random event duration between 5 and 20 seconds
        eventEndTime = Time.time + eventDuration; // Calculate when the event should end.
        Debug.Log("Stalker event is active!");

        // Calculate the time for the next stalker event.
        float randomTimeBetweenEvents = Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
        nextEventTime = Time.time + randomTimeBetweenEvents;
    }

    private void EndStalkerEvent()
    {
        // Clean up and end the stalker event.
        isStalkerEvent = false;
        Debug.Log("Stalker event ended.");
    }
}
