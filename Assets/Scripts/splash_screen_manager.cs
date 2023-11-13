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
    private UnityEngine.UI.Image menuBlocker;
    private BoxCollider2D menuCollider;
    private notification_manager notificationManager;


    // splash screen timers
    private bool isSplashShowing;
    private float displayTime = 0.5f;
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
            { "Watch TV", Resources.Load<Sprite>("TV_Zoom_In") },
            { "Lift weights", Resources.Load<Sprite>("Workout_Zoom_In") },
            { "Do chores", Resources.Load<Sprite>("Clean_Zoom_In") },
            { "Go to the gym", Resources.Load<Sprite>("Clean_Zoom_In") },
            { "Visit friends", Resources.Load<Sprite>("Clean_Zoom_In") },
            { "Go for a walk", Resources.Load<Sprite>("Clean_Zoom_In") },
            { "Eat at a restaurant", Resources.Load<Sprite>("Clean_Zoom_In") },
            
            // kitchen
            { "Cook food", Resources.Load<Sprite>("Oven_Zoom_In") },
            { "Eat a snack", Resources.Load<Sprite>("Fridge_Zoom_In")},

            // bedroom
            { "Go to sleep", Resources.Load<Sprite>("Clean_Zoom_In")  },
            { "Take a nap", Resources.Load<Sprite>("Clean_Zoom_In")  },

            // bathroom
            { "Freshen up", Resources.Load < Sprite >("Clean_Zoom_In") },
            { "Shower", Resources.Load < Sprite >("Clean_Zoom_In") },
            { "Bubble bath", Resources.Load < Sprite >("Clean_Zoom_In") },

            // non actions
            { "reset", Resources.Load<Sprite>("Hospital_Concepts_V001") },
            { "Hospital", Resources.Load<Sprite>("Hospital_Concepts_V001") },
            { "Game over", Resources.Load<Sprite>("Hospital_Concepts_V001") }
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

        // diable any current notifications
        notificationManager.disableNotification();

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
            // close the splash screeen after 1.5 seconds if any key or mouse button is held down
            if (Input.anyKey)
            {
                // close splash
                splashScreen.enabled = false;
                menuBlocker.enabled = false;
                menuCollider.enabled = false;
                isSplashShowing = false;

                // enable HUD
                if (shouldHUDShow)
                {
                    HUD.alpha = 1;
                }

                // run notification
                notificationManager.repeatNotification();
            }
        }
    }
}
