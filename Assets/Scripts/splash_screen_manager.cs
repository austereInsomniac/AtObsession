using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class splash_screen_manager : MonoBehaviour
{
    // store the splash screens
    Dictionary<string, Sprite> splashScreens;

    // outside objects
    private UnityEngine.UI.Image splashScreen;
    private UnityEngine.UI.Image menuBlocker;
    private BoxCollider2D menuCollider;
    private notification_manager notificationManager;


    // splash screen timers
    private bool isSplashShowing;
    private float displayTime = 1.5f;
    private float displayStartTime;

    // HUD
    GameObject HUD;

    // Start is called before the first frame update
    void Start()
    {
        // grab outside objects
        splashScreen = GameObject.Find("Splash Screen").GetComponent<UnityEngine.UI.Image>();
        menuBlocker = GameObject.Find("Menu Click Blocker").GetComponent<UnityEngine.UI.Image>();
        menuCollider = GameObject.Find("Menu Click Blocker").GetComponent<BoxCollider2D>();
        HUD = GameObject.Find("HUD");
        notificationManager = GameObject.Find("Notification Panel").GetComponent<notification_manager>();

        // grab sprites
        Sprite hospital = Resources.Load<Sprite>("Hospital_Concepts_V001");

        // splash screen code
        splashScreens = new Dictionary<string, Sprite>
        {
           // living room
            { "Do chores", splashScreen.sprite },
            { "Go to the gym", splashScreen.sprite },
            { "Visit friends", splashScreen.sprite },
            { "Go for a walk", splashScreen.sprite },
            { "Watch TV", splashScreen.sprite },
            { "Lift weights", splashScreen.sprite },
            { "Eat at a restaurant", splashScreen.sprite },
            
            // kitchen
            { "Cook food", Resources.Load<Sprite>("Oven_Zoom_In") },
            { "Eat a snack", Resources.Load<Sprite>("Fridge_Zoom_In")},

            // bedroom
            { "Go to sleep", splashScreen.sprite },
            { "Take a nap", splashScreen.sprite },

            // bathroom
            { "Freshen up", splashScreen.sprite },
            { "Shower", splashScreen.sprite },
            { "Bubble bath", splashScreen.sprite },

            // non actions
            { "Black", splashScreen.sprite},
            { "Hospital", hospital },
            { "Game over", hospital }
        };
    }

    public void openSplashScreen(string key)
    {
        // display appropriate splash screen for a set time
        splashScreen.sprite = splashScreens[key];
        splashScreen.enabled = true;

        // Record start time
        displayStartTime = Time.timeSinceLevelLoad;
        isSplashShowing = true;

        // hide hud
        HUD.SetActive(false);
        menuBlocker.enabled = true;
        menuCollider.enabled = true;

        // destroy HUD if we die
        if (key.Equals("Game over"))
        {
            Destroy(HUD);
        }

        // diable any current notifications
        notificationManager.disableNotification();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSplashShowing && Time.timeSinceLevelLoad >= displayTime + displayStartTime)
        {
            // close the splash screeen after 1.5 seconds if any key or mouse button is held down
            if (Input.anyKey)
            {
                // close splash
                splashScreen.enabled = false;
                menuBlocker.enabled = false;
                menuCollider.enabled = false;
                isSplashShowing = false;

                // enable HUD
                if (HUD != null)
                {
                    HUD.SetActive(true);
                }
            }
        }
    }
}
