using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_music : MonoBehaviour
{
    AudioSource bgMusic1;
    AudioSource bgMusic2;
    AudioSource bgMusic3;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bgMusic1 = GameObject.Find("Background Music 01").GetComponent<AudioSource>();
        bgMusic2 = GameObject.Find("Background Music 02").GetComponent<AudioSource>();
        bgMusic3 = GameObject.Find("Background Music 03").GetComponent<AudioSource>();

        // Start playing default music.
        PlayDefaultMusic();
    }

    void PlayDefaultMusic()
    {
        // Play the default music here, for example, bgMusic1.
        bgMusic1.Play();
    }

    void PlayDay11Music()
    {
        // Stop the previous music and play bgMusic2.
        bgMusic1.Stop();
        bgMusic2.Play();
    }

    void PlayDay14Music()
    {
        // Stop the previous music and play bgMusic3.
        bgMusic1.Stop();
        bgMusic2.Stop();
        bgMusic3.Play();
    }


    // Update is called once per frame
    void Update()
    {
        // Access game state to determine day and time.
        int currentDay = player.GetComponent<game_state>().getDay();
        int currentTime = player.GetComponent<game_state>().getTime();

        // Check conditions for changing music.
        if (currentDay >= 11 && currentDay < 14)
        {
            PlayDay11Music();
        }
        else if (currentDay >= 14)
        {
            PlayDay14Music();
        }
    }
}
