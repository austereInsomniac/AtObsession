using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Mackenzie

public class splash_screen_manager : MonoBehaviour
{
    // store the splash screens
    Dictionary<string, Sprite> splashScreens;

    // outside objects
    private UnityEngine.UI.Image splashScreen;
    private UnityEngine.Animation splashScreenAnimated;
    private UnityEngine.UI.Image menuBlocker;
    private BoxCollider2D menuCollider;
    private notification_manager notificationManager;

    // splash screen timers
    private bool isSplashShowing;
    private float displayTime = 0.4f;
    private float displayStartTime;

    // HUD
    CanvasGroup HUD;
    private bool shouldHUDShow;

    // Start is called before the first frame update
    void Start()
    {
        // grab outside objects
        splashScreen = GameObject.Find("Splash Screen").GetComponent<UnityEngine.UI.Image>();
        menuBlocker = GameObject.Find("Menu Click Blocker").GetComponent<UnityEngine.UI.Image>();
        menuCollider = GameObject.Find("Menu Click Blocker").GetComponent<BoxCollider2D>();
        HUD = GameObject.Find("HUD").GetComponent<CanvasGroup>();
        notificationManager = GameObject.Find("Notification Panel").GetComponent<notification_manager>();

        shouldHUDShow = true;

        // splash screen code
        splashScreens = new Dictionary<string, Sprite>
        {
            // living room
            { "Go to the gym", Resources.Load<Sprite>("Gym") },
            { "Visit friends", Resources.Load<Sprite>("Park-Recovered") },
            { "Go for a walk", Resources.Load<Sprite>("Park-Recovered") },

            // non actions
            { "reset", Resources.Load<Sprite>("Hospital_Concepts_V001") },
            { "Hospital", Resources.Load<Sprite>("Hospital_Concepts_V001") },
            { "Game over", Resources.Load<Sprite>("Hospital_Concepts_V001") }
        };
    }

    public void openSplashScreen(string key)
    {
        if (splashScreens[key] != null)
        {
            // display appropriate splash screen for a set time
            splashScreen.sprite = splashScreens[key];
            splashScreen.enabled = true;

            // Record start time
            displayStartTime = Time.timeSinceLevelLoad;
            isSplashShowing = true;
        }
        else
        {
            splashScreenAnimated.Play();
            splashScreen.enabled = true;
        }

        // hide hud
        HUD.alpha = 0;
        menuBlocker.enabled = true;
        menuCollider.enabled = true;

        // destroy HUD if we die
        if (key.Equals("Game over"))
        {
            shouldHUDShow = false;
        }
        else 
        {
            shouldHUDShow = true;    
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSplashShowing && Time.timeSinceLevelLoad >= displayTime + displayStartTime)
        {
            // close splash
            splashScreen.enabled = false;
            isSplashShowing = false;

            // show hud
            menuBlocker.enabled = false;
            menuCollider.enabled = false;                

            // enable HUD
            if (shouldHUDShow)
            {
                HUD.alpha = 1;
            }
        }
    }
}
