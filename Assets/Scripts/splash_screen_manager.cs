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
    private UnityEngine.UI.Image splashScreenAnimated;
    private UnityEngine.Animator splashScreenAnimator;
    private UnityEngine.UI.Image menuBlocker;
    private BoxCollider2D menuCollider;
    private notification_manager notificationManager;

    // splash screen timers
    private bool isSplashShowing;
    private float displayTimeAnimated = .9f;
    private float displayTimeStatic = .01f;
    private float displayStartTime;

    // HUD
    CanvasGroup HUD;
    private bool shouldHUDShow;

    // Start is called before the first frame update
    void Start()
    {
        // grab outside objects
        splashScreen = GameObject.Find("Splash Screen").GetComponent<UnityEngine.UI.Image>();
        splashScreenAnimated = GameObject.Find("Splash Screen Animated").GetComponent<UnityEngine.UI.Image>();
        splashScreenAnimator = GameObject.Find("Splash Screen Animated").GetComponent<UnityEngine.Animator>();
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
            { "Visit friends", Resources.Load<Sprite>("Park_02") },
            { "Go for a walk", Resources.Load<Sprite>("Park_02") },
            { "Eat at a restaurant", Resources.Load<Sprite>("Restaurant") },

            // non actions
            { "reset", Resources.Load<Sprite>("Hospital_Concepts_V001 1") },
            { "Hospital", Resources.Load<Sprite>("Hospital_Concepts_V001 1") },
            { "Game over", Resources.Load<Sprite>("Hospital_Concepts_V001 1") },

            // ending
            { "Ending", Resources.Load<Sprite>("Computer_Breaking_News") }
        };
    }

    public void openSplashScreen(string key)
    {
        // display appropriate splash screen for a set time
        if (splashScreens.ContainsKey(key))
        {
            splashScreen.sprite = splashScreens[key];
            splashScreen.enabled = true;
        }
        else
        {
            // play animation
            splashScreenAnimated.enabled = true;
            splashScreenAnimator.ResetTrigger("End Animation");
            splashScreenAnimator.SetTrigger("Start Animation");  
        }

        // Record start time
        displayStartTime = Time.timeSinceLevelLoad;
        isSplashShowing = true;

        // hide hud
        HUD.alpha = 0;
        menuBlocker.enabled = true;
        menuCollider.enabled = true;

        // destroy HUD if we die
        if (key.Equals("Game over") || key.Equals("Ending"))
        {
            shouldHUDShow = false;
        }
        else 
        {
            shouldHUDShow = true;    
        }
    }

    void disableSplash()
    {
        splashScreen.enabled = false;
        splashScreenAnimated.enabled = false;
        isSplashShowing = false;

        // enable HUD
        if (shouldHUDShow)
        {
            HUD.alpha = 1;
        }
    }

    public IEnumerator<UnityEngine.WaitForSeconds> holdSplashScreen(string key, float time)
    {
        // Set the notification message
        openSplashScreen(key);
        isSplashShowing = false;

        // wait and disable
        yield return new WaitForSeconds(time);
        disableSplash();
    }

    // Update turns off the splash after a set time
    void Update()
    {
        // static splash
        if (splashScreen.enabled == true && isSplashShowing && Time.timeSinceLevelLoad >= displayTimeStatic + displayStartTime && Input.anyKeyDown)
        {
            disableSplash();
        }

        if (splashScreenAnimated.enabled == true && isSplashShowing && Time.timeSinceLevelLoad >= displayTimeAnimated + displayStartTime)
        {
            // close splash
            splashScreenAnimator.ResetTrigger("Start Animation");
            splashScreenAnimator.SetTrigger("End Animation");

            disableSplash();
        }
    }
}
