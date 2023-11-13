using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_scares : MonoBehaviour
{
    private bool isScare = false; // Tracks if scare is happening
    private float scareDuration = 10; // How long it should last
    private float scareEndTime = 0; // Timestamp when event should end

    private float minTimeBetweenScares = 10; // Minimum time between scares
    private float maxTimeBetweenScares = 30; // Maximum time between scares
    private float nextScareTime = 0; // Timestamp when next event should start

    private GameObject[] swappableAssets;
    private GameObject player;

    private swap_background_assets swap;
    public notification_manager notification;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        swappableAssets = GameObject.FindGameObjectsWithTag("swappable");
    }

    // Update is called once per frame
    void Update()
    {
        int wellness = player.GetComponent<game_state>().getWellness();
        int day = player.GetComponent<game_state>().getDay();
        if (isScare && Time.time >= scareEndTime)
        {
            EndScare();
        }
        if (!isScare && Time.time > nextScareTime && wellness <= 60 && day >= 3)
        {
            if (swappableAssets.Length > 0)
            {
                int randomScare = Random.Range(0, swappableAssets.Length);
                TriggerScare(randomScare);
            }
            else
            {
                Debug.Log("No swappable assets");
            }
        }
    }

    private void TriggerScare(int scareNum)
    {
        scareDuration = Random.Range(5, 20);
        scareEndTime = Time.time + scareDuration;
        string scareMessage = "...Something feels off";
        Debug.Log(scareMessage);

        swap = swappableAssets[scareNum].GetComponent<swap_background_assets>();

        if (swap != null)
        {
            swap.swapObjects();
            if (notification != null)
            {
                notification.showNotification(scareMessage);
            }
        }

        float randomTimeBetweenScares = Random.Range(minTimeBetweenScares, maxTimeBetweenScares);
        nextScareTime = Time.time + randomTimeBetweenScares;
        isScare = true;
    }

    private void EndScare()
    {
        isScare = false;
        Debug.Log("Scare ended");
    }
}