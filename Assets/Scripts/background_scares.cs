using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_scares : MonoBehaviour
{
    private GameObject[] swappableAssets;
    private game_state player;

    private swap_assets swap;
    public notification_manager notification;
    int scare = 0;

    public AudioClip clickSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<game_state>();
        swappableAssets = GameObject.FindGameObjectsWithTag("swappable");

        player.addOnTimeChange(OnTimeChanged);
    }

    private void OnTimeChanged(int oldTime, int newTime)
    {
        int day = player.getDay();
        int time = player.getTime();
        if (day >= 3 && time >= 8 * 60)
        {
            int timeChange = newTime - oldTime;
            scare = timeChange + scare;
            int wellness = player.getWellness();

            if (scare >= 4 * 60 && wellness <= 60)
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
                scare -= scare;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        PlayClickSound();
    }

    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            AudioSource.PlayClipAtPoint(clickSound, transform.position);
        }
    }
    private void TriggerScare(int scareNum)
    {
        string scareMessage = "I hear something...";
        Transform greatGrandParentObj = swappableAssets[scareNum].transform.parent.parent.parent;
        if (greatGrandParentObj.name == player.getLocation().name)
        {
            int originalScareNum = scareNum;
            // Randomly select a new scareNum within the valid range
            scareNum = Random.Range(0, swappableAssets.Length);

            // Make sure the new scareNum is not equal to the original scareNum
            while (scareNum == originalScareNum)
            {
                scareNum = Random.Range(0, swappableAssets.Length);
            }
        }

        swap = swappableAssets[scareNum].GetComponent<swap_assets>();
        if (swappableAssets[scareNum].name == "Sink - off")
        {
            PlayClickSound();
        }
        if (swap != null)
        {
            swap.swapObjects();
            if (notification != null)
            {
                notification.showNotification(scareMessage);
            }
        }
    }
}