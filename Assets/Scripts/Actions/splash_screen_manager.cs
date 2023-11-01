using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splash_screen_manager : MonoBehaviour
{
    // store the splash screens
    Dictionary<string, UnityEngine.UI.Image> splashScreens;

    // outside objects
    private UnityEngine.UI.Image splashScreen;
    private BoxCollider2D menuCollider;

    // splash screen timers
    private bool isSplashShowing;
    private float displayTime = 1.5f;
    private float displayStartTime;

    // Start is called before the first frame update
    void Start()
    {
        splashScreen = GameObject.Find("Splash Screen").GetComponent<UnityEngine.UI.Image>();
        menuCollider = GameObject.Find("Menu Click Blocker").GetComponent<BoxCollider2D>();

        // splash screen code
        splashScreens = new Dictionary<string, UnityEngine.UI.Image>()
        {
            // living room
            { "Do chores", splashScreen },
            { "Go to the gym", splashScreen },
            { "Visit friends", splashScreen },
            { "Go for a walk", splashScreen },
            { "Watch TV", splashScreen },
            { "Lift weights", splashScreen },
            { "Eat at a restaurant", splashScreen },

            // kitchen
            { "Cook food", splashScreen },
            { "Eat a snack", splashScreen },

            // bedroom
            { "Go to sleep", splashScreen },
            { "Take a nap", splashScreen },

            // bathroom
            { "Freshen up", splashScreen },
            { "Shower", splashScreen },
            { "Bubble bath", splashScreen }
        };
    }
    public void openSplashScreen(string key)
    {
        // display appropriate splash screen for a set time
        splashScreen = splashScreens[key];
        splashScreen.enabled = true;

        // Record start time
        displayStartTime = Time.timeSinceLevelLoad;
        isSplashShowing = true;
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
                menuCollider.enabled = false;
                isSplashShowing = false;
            }
        }
    }
}
