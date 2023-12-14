using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StalkerChoice
{
    public string choiceText;
    public string choiceNotification;
    public int wellnessChange;
    public int endingChange;
    public int reputationChange;

    public StalkerChoice(string text, string notification, int wellness, int ending, int reputation)
    {
        choiceText = text;
        choiceNotification = notification;
        wellnessChange = wellness;
        endingChange = ending;
        reputationChange = reputation;
    } 

    public string getText() {  return choiceText; }
    public string getNotification() { return choiceNotification;
    } public int getWellnessChange() {  return wellnessChange; }
    public int getEndingChange() {  return endingChange; }
    public int getReputationChange() {  return reputationChange; }
}

public class StalkerEvents
{
    int wellness;
    int eventNumber;
    int ending;
    int reputation;
    string eventMessage;
    string eventLocation;
    public const int finalEventNumber = 9;
    List<StalkerChoice> choices;
    AudioClip eventSound;

    int maxOccurrences;
    int currentOccurrences;
    public bool hasDisplayed;

    public StalkerEvents(string changeMessage, string location, int changeWellness, int newEventNumber, int changeEnding, int changeRep, int maxTimes, int currentTimes, bool isDisplayed, AudioClip stalkerSound)
    {
        wellness = changeWellness;
        eventNumber = newEventNumber;
        ending = changeEnding;
        reputation = changeRep;
        eventMessage = changeMessage;
        eventLocation = location;
        choices = new List<StalkerChoice>();
        eventSound = stalkerSound;

        maxOccurrences = maxTimes;
        currentOccurrences = currentTimes;
        hasDisplayed = isDisplayed;
    }

    public int getWellness() { return wellness; }
    public int getEventNumber() { return eventNumber; }
    public int getEnding() { return ending; }
    public int getReputation() { return reputation; }
    public string getEventMessage() { return eventMessage; }
    public string getEventLocation() {  return eventLocation; }
    public int getCurrentOccurrences() { return currentOccurrences; }
    public int getMaxOccurrences() { return maxOccurrences;}
    public bool isDisplayed() { return hasDisplayed; }
    public AudioClip getEventSound() { return eventSound; }

    public void setEventMessage(string newEventMessage)
    {
        eventMessage = newEventMessage;
    }

    public void AddChoice(string text, string notification, int wellness, int ending, int reputation)
    {
        choices.Add(new StalkerChoice(text, notification, wellness, ending, reputation));
    }

    public List<StalkerChoice> GetChoices()
    {
        return choices;
    }

    public StalkerChoice getChoice1()
    {
        if (choices.Count > 0)
        {
            return choices[0];
        }
        return null;
    }

    public StalkerChoice getChoice2()
    {
        if (choices.Count > 1)
        {
            return choices[1];
        }
        return null;
    }

    public StalkerChoice getChoice3()
    {
        if (choices.Count > 2)
        {
            return choices[2];
        }
        return null;
    }

    public void setChoice1(string newText)
    {
        if (choices.Count > 0)
        {
            choices[0].choiceText = newText;
        }
    }

    public void setChoice2(string newText)
    {
        if (choices.Count > 1)
        {
            choices[1].choiceText = newText;
        }
    }

    public void setChoice3(string newText)
    {
        if (choices.Count > 2)
        {
            choices[2].choiceText = newText;
        }
    }

    // Method to check if the event can occur
    public bool CanOccur()
    {
        return currentOccurrences <= maxOccurrences;
    }

    // Method to increment the occurrence counter
    public void IncrementOccurrences()
    {
        currentOccurrences++;
    }

    public void changeChoiceNotification(StalkerChoice choice, string newNotification)
    {
        choice.choiceNotification = newNotification;
    }
}

public class stalker_prototype_script : MonoBehaviour
{
    private bool isOn = true;
    private bool isEnd = false;
    private bool isGameEnding = false;
    private bool isEndingEvent = false;
    List<string> eventKeys;
    Dictionary<string, StalkerEvents> stalkerEvents;
    List<StalkerChoice> playerChoices;
    private StalkerEvents eventHappening;

    private int initialEventCount = 0;
    private int eventNum;
    private int pendingEvent = -1; // Set to -1 to indicate no pending event
    private int eventCount;
    private float displayTime = 3.0f;
    private float displayStartTime;

    private game_state player;
    GameObject stalkerEventHandler;
    TMP_Text choiceText;
    private GameObject choice1;
    private GameObject choice2;
    private GameObject choice3;
    TMP_Text choice1Text;
    TMP_Text choice2Text;
    TMP_Text choice3Text;
    private GameObject checkChitter;
    public Button checkChitterRight;
    private GameObject checkMessages;
    public Button checkMessagesRight;
    public GameObject email1;
    public GameObject email1Preview;
    public GameObject email1Options;
    public GameObject email2;
    public GameObject email2Preview;
    public GameObject email2Options;
    public GameObject email3;
    public GameObject email3Preview;
    public GameObject email3Options;
    public GameObject dm1;
    public GameObject dm1Preview;
    public GameObject dm1Options;
    public GameObject dm2;
    public GameObject dm2Preview;
    public GameObject dm2Options;
    private GameObject currentStalkerEmail;
    private GameObject currentStalkerEmailPreview;
    private GameObject currentStalkerEmailOptions;
    private GameObject currentStalkerDM;
    private GameObject currentStalkerDMPreview;
    private GameObject currentStalkerDMOptions;

    private move_location move;
    public notification_manager notification;
    private swap_assets swap;
    private splash_screen_manager splashScreenManager;

    private void Awake()
    {
        // Subscribe to the location change event from game_state
        player = GameObject.Find("Player").GetComponent<game_state>();
        player.addOnLocationChange(OnLocationChanged);
    }
    // Start is called before the first frame update
    void Start()
    {
        // "Player" is the name of the Game Object with the game_state script

        move = GetComponent<move_location>();
        stalkerEventHandler = GameObject.Find("Stalker Event Handler");
        stalkerEventHandler.SetActive(true);
        choiceText = GameObject.Find("ChoiceText").GetComponent<TMP_Text>();
        choice1 = GameObject.Find("Choice1");
        choice2 = GameObject.Find("Choice2");
        choice3 = GameObject.Find("Choice3");
        stalkerEventHandler.SetActive(false);
        splashScreenManager = GetComponent<splash_screen_manager>();
        checkChitter = GameObject.Find("Check_Chitter");
        checkMessages = GameObject.Find("Check_Email");
        currentStalkerEmail = email1;
        currentStalkerEmailPreview = email1Preview;
        currentStalkerEmailOptions = email1Options;
        currentStalkerDM = dm1;
        currentStalkerDMPreview = dm1Preview;
        currentStalkerDMOptions = dm1Options;

        stalkerEvents = new Dictionary<string, StalkerEvents>();

        // Email event 0/1
        StalkerEvents emailEvent = new StalkerEvents("NoticeMeSenpaii has now sent me an email.", "Computer", 0, 1, 1, -1, 4, 0, false, Resources.Load<AudioClip>("Assets/Household Items SFX/Mobile/Mobile_Vibre (5).wav"));
        emailEvent.AddChoice("Reply to email", "I should reply to the email. They may be acting a little creepy, but they are one of my fans, they aren�t going to hurt me.", -3, -1, 0);
        emailEvent.AddChoice("Ignore email", "They keep sending me creepy DMs and now they sent me an email. I should just ignore them and not interact with them at all.", -1, 0, 0);
        emailEvent.AddChoice("Report email", "This person keeps on sending me really creepy DMs and now they are sending me a creepy email. I need to report them before this gets any worse.", -1, 1, 5);
        stalkerEvents.Add("Email", emailEvent);

        // Knocking on window event 1/2
        StalkerEvents knockingEvent = new StalkerEvents("As I�m near the window I hear a knocking on the window. Almost like someone is trying to get my attention.", "Bedroom", 10, 2, 1, 0, 1, 0, false, Resources.Load<AudioClip>("Assets/Household Items SFX/Breakable/Tap_Glass (1).wav"));
        knockingEvent.AddChoice("Investigate", "I grab a flashlight and go outside to see if there is anything there. No one is there but I can see where someone stood to look through the window.", -3, -1, 0);
        knockingEvent.AddChoice("Scream and call cops", "I screamed and grabbed my phone to call 911.The cops show up and tell me that no one was there but looks like something could have been there but that it wasn�t likely there had been.", -1, 0, 0);
        knockingEvent.AddChoice("Take photo with phone", "I pretend like I don�t hear anything as I�m on my phone. I act like I got a phone call by holding my phone up to my ear and taking a recording as I walk past the window. Once I walk look at the video and see that there is someone in my window. I call the cops and they take the video as evidence and tell me they will do what they can.", -1, 1, 0);
        stalkerEvents.Add("Knocking on window", knockingEvent);

        // Knocking on door event 2/3
        StalkerEvents doorEvent = new StalkerEvents("Someone is knocking on the front door", "Any", 10, 3, 1, 0, 1, 0, false, Resources.Load<AudioClip>("Assets/Household Items SFX/Doorbell/Doorbell (4).wav"));
        doorEvent.AddChoice("Open door", "I open the door but there is no one there. I shrug and close the door.", -3, -1, 0);
        doorEvent.AddChoice("Hesitate but open door", "I hesitate to open the door. Someone knocks again so I decide to open the door slightly and look outside. NO ONE is there! I slam the door closed and lock it!", -1, 0, 0);
        doorEvent.AddChoice("Look through peephole", "I decided to look through the peephole of the door. No one is there. It concerns me a little bit, so I make sure that my door is locked.", -1, 1, 0);
        stalkerEvents.Add("Knocking on door", doorEvent);

        // Suspicious gift event 3/4
        StalkerEvents giftEvent = new StalkerEvents("I got a package with no return address but on the package, it says \"From your biggest fan - NoticeMeSenpaii\"", "Living room", 10, 4, 1, 0, 2, 0, false, Resources.Load<AudioClip>("Assets/Household Items SFX/Doorbell/Doorbell (4).wav"));
        giftEvent.AddChoice("Open package and keep gift", "I got a package without an address. When I opened the package there was a note and a gift. The note was a little creepy, but they say they are one of my biggest fans.", -3, -1, 0);
        giftEvent.AddChoice("Open package but toss gift", "There is no return address but decided to open it to check the contents. When I open it there is a gift and a creepy note saying how much they love me and that we should be together. I decided to throw them away.", -1, 0, 0);
        giftEvent.AddChoice("Don�t open and throw away package", "There is no return address and even though it seems to be from a fan. To be safe I won�t be opening it and since there is no return address, I threw it away.", -1, 1, 0);
        stalkerEvents.Add("Suspicious gift", giftEvent);

        // Window figure event 4/5
        StalkerEvents windowEvent = new StalkerEvents("As I�m walking by my window is see a shadow figure looking through at me!", "Kitchen", 10, 5, 1, 0, 2, 0, false, null);
        windowEvent.AddChoice("Go outside and investigate", "I grab a flashlight and go outside to see if there is anything there. No one is there but I can see where someone stood to look through the window.", -3, -1, 0);
        windowEvent.AddChoice("Scream and call cops", "I screamed and grabbed my phone to call 911. The cops show up and tell me that no one was there but looks like something could have been there but that it wasn�t likely there had been.", -1, 0, 0);
        windowEvent.AddChoice("Look out window", "I kept calm and looked out the window. There was nothing there but decided to lock my door and windows and to keep my phone near me, just in case.", -1, 1, 0);
        stalkerEvents.Add("Window figure", windowEvent);

        // Fan game event 5/6
        StalkerEvents fanGameEvent = new StalkerEvents("While streaming I get a message from NoticeMeSenpaii asking me to play the game they made.", "Bedroom", 10, 6, 1, -1, 1, 0, false, Resources.Load<AudioClip>("Assets/Household Items SFX/Mobile/Mobile_Vibre (5).wav"));
        fanGameEvent.AddChoice("Download and play game", "I downloaded the game and played the game while streaming. It was a creepy game all about me and what I do in my daily life.", -3, -1, 0);
        fanGameEvent.AddChoice("Download but don�t play", "I decided to download it but decided not to play it. I told them that I might play it at a later time.", -1, 0, 0);
        fanGameEvent.AddChoice("Decline download and don't play", "I tell them that I will not be downloading the game for safety reasons and that if they wish for me to play it they should upload it to a secure gaming site.", -1, 1, 5);
        stalkerEvents.Add("Fan game", fanGameEvent);

        // Unknown call event 6/7
        StalkerEvents unknownCallEvent = new StalkerEvents("My phone rings. It�s a call from an unknown number.", "Any", 10, 7, 1, 0, 3, 0, false, Resources.Load<AudioClip>("Assets/Household Items SFX/Telephone/Telephone_Ringing (5) Loop.wav"));
        unknownCallEvent.AddChoice("Answer call", "I answered the phone and say \"Hello?\" No one responds but I can hear someone breathing heavily on the other end. I say \"Hello?\" again with no response. It freaks me out, so I hang up.", -3, -1, 0);
        unknownCallEvent.AddChoice("Decline call", "I don�t know the number so decline the call. I little while later I see that I have a voicemail. I check it but all I hear is heavy breathing and then a robotic voice saying they love me and only they can be with me. It freaks me out and I delete the voicemail.", -1, 0, 0);
        unknownCallEvent.AddChoice("Go to voicemail", "Since I don�t know the number, I let it go to voicemail. Then listened to the voicemail that was just heavy breathing and then a robotic voice saying they love me and only they can be with me. It freaks me out, but I should save the voicemail just in case and block this number.", -1, 1, 0);
        stalkerEvents.Add("Unknown call", unknownCallEvent);

        // Direct message event 7/8
        StalkerEvents dmEvent = new StalkerEvents("Looks like I got a new DM from an account called NoticeMeSenpaii.", "Computer", 10, 8, 1, 0, 2, 0, false, Resources.Load<AudioClip>("Assets/Household Items SFX/Mobile/Mobile_Vibre (5).wav"));
        dmEvent.AddChoice("Report DM", "I don�t know this person and the DM is creepy. I should report them.", -3, -1, 0);
        dmEvent.AddChoice("Reply to DM", "I don�t know this person and they seem a little creepy, but I should respond to them since they are one of my fans.", -1, 0, 0);
        dmEvent.AddChoice("Ignore DM", "I don�t know this person and the DM makes them seem a little creepy. I�m going to ignore them.", -1, 1, 0);
        stalkerEvents.Add("Direct message", dmEvent);

        // Banging on door event 8/9
        StalkerEvents bangingEvent = new StalkerEvents("BANG, BANG, BANG! I can hear someone banging on my front door.", "Any", 10, 9, 5, 0, 1, 0, false, null);
        bangingEvent.AddChoice("Open door", "I call out, \"I�m coming, I�m coming!\" When I open the door no one is there. I turn around and see a note nailed to my door saying, \"This is the last time! YOU ARE MINE! - NoticeMeSenpaii\" I sneer as I tear down the note and close the door.", -3, -1, 0);
        bangingEvent.AddChoice("Scream \"I�m calling the police!\"", "Knowing that I�ve been getting threats I scream out that I�m calling the police. When they get here, they say no one was there but they found a note nailed to the door saying, \"This is the last time! YOU ARE MINE! - NoticeMeSenpaii\"", -1, 0, 0);
        bangingEvent.AddChoice("Look through peephole", "I checked who is at the door through the peep hole. NO ONE is there. I cautiously open the door to look outside. I see that there is a note nailed to my door saying, \"This is the last time! YOU ARE MINE! - NoticeMeSenpaii\" I decided to call the police and give them the letter for evidence.", -1, 1, 0);
        stalkerEvents.Add("Banging on door", bangingEvent);

        // Trapped in bathroom event 9/10
        StalkerEvents bathroomEvent = new StalkerEvents("CRASH\"AWWWWW SOMEONE IS BREAKING IN! I NEED TO FIND A PLACE TO HIDE AND CALL THE COPS!", "Any", 0, 10, 0, 0, 1, 1, false, Resources.Load<AudioClip>("Assets/Household Items SFX/Breakable/Breakable_Glass (3).wav"));
        stalkerEvents.Add("Trapped in bathroom", bathroomEvent);

        eventKeys = new List<string>(stalkerEvents.Keys);
        eventHappening = stalkerEvents["Email"];

        playerChoices = new List<StalkerChoice>();
    }

    private void OnLocationChanged(GameObject oldLocation, GameObject newLocation)
    {

        // Check if there is a pending event
        if (pendingEvent != -1)
        {
            string eventKey = eventKeys[pendingEvent];
            StalkerEvents stalkerEvent = stalkerEvents[eventKey];
            int day = player.getDay();
            if (pendingEvent == 0 || pendingEvent == 7)
            {
                if (day != 3 && day != 5 && day != 9 && day != 13 && stalkerEvent.hasDisplayed == false)
                {
                    DefaultPlayerChoice(stalkerEvent);
                    pendingEvent = -1;
                }
            }
            string location = stalkerEvent.getEventLocation();
            if (newLocation.name == location && location != "Computer")
            {
                // Player reached the correct location, proceed with displaying choices
                DisplayChoices(stalkerEvent);
                pendingEvent = -1; // Clear the pending event
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int wellness = player.getWellness();
        int day = player.getDay();
        int time = player.getTime();

        if (isOn)
        {
            if (day == 3 && time >= 17 * 60 && eventCount == initialEventCount)
            {
                isOn = false;
                TriggerStalkerEvent(7);

                checkChitterRight.onClick.AddListener(() => SetMessageActive(currentStalkerDM, currentStalkerDMPreview, currentStalkerDMOptions));   
            }
            else if (day == 5 && time >= 10 * 60 && eventCount == 1)
            {
                currentStalkerDM = dm2;
                currentStalkerDMPreview = dm2Preview;
                currentStalkerDMOptions = dm2Options;
                isOn = false;
                TriggerStalkerEvent(7);

                checkChitterRight.onClick.AddListener(() => SetMessageActive(currentStalkerDM, currentStalkerDMPreview, currentStalkerDMOptions));
                if (time >= 15 * 60 && eventCount == 2)
                {
                    isOn = false;
                    TriggerStalkerEvent(0);

                    checkMessagesRight.onClick.AddListener(() => SetMessageActive(currentStalkerEmail, currentStalkerEmailPreview, currentStalkerEmailOptions));
                }
            }
            else if (day == 7 && time >= 13 * 60 && eventCount == 3)
            {
                isOn = false;
                TriggerStalkerEvent(2);
            }
            else if (day == 9 && time >= 13 * 60 && eventCount == 5)
            {
                currentStalkerEmail = email2;
                currentStalkerEmailPreview = email2Preview;
                currentStalkerEmailOptions = email2Options;
                isOn = false;
                TriggerStalkerEvent(0);

                checkMessages.GetComponent<Button>().onClick.AddListener(() => SetMessageActive(currentStalkerEmail, currentStalkerEmailPreview, currentStalkerEmailOptions));

                if (time >= 18 * 60 && eventCount == 6)
                {
                    isOn = false;
                    TriggerStalkerEvent(5);
                }
            }
            else if (day == 11 && time >= 12 * 60 && eventCount == 7)
            {
                isOn = false;
                TriggerStalkerEvent(2);
                
                if (time >= 16 * 60 && eventCount == 8)
                {
                    isOn = false;
                    TriggerStalkerEvent(6);

                    if (time >= 20 * 60 && eventCount == 9)
                    {
                        isOn = false;
                        TriggerStalkerEvent(4);
                    }
                }
            }
            else if (day == 12 && time >= 13 * 60 && eventCount == 10)
            {
                isOn = false;
                TriggerStalkerEvent(3);

                if (time >= 16.5 * 60 && eventCount == 11)
                {
                    isOn = false;
                    TriggerStalkerEvent(6);

                    if (time >= 21  * 60 && eventCount == 12)
                    {
                        isOn = false;
                        TriggerStalkerEvent(1);
                    }
                }
            }
            else if (day == 13 && time >= 13.5 * 60 && eventCount == 13)
            {
                isOn = false;
                TriggerStalkerEvent(6);

                if (time >= 16 * 60 && eventCount == 14)
                {
                    currentStalkerEmail = email3;
                    currentStalkerEmailPreview = email3Preview;
                    currentStalkerEmailOptions = email3Options;
                    isOn = false;
                    TriggerStalkerEvent(0);

                    checkMessagesRight.onClick.AddListener(() => SetMessageActive(currentStalkerEmail, currentStalkerEmailPreview, currentStalkerEmailOptions));

                    if (time >= 19 * 60 && eventCount == 15)
                    {
                        isOn = false;
                        TriggerStalkerEvent(8);

                        if (time >= 22.5 * 60 && eventCount == 16)
                        {
                            isOn = false;
                            TriggerStalkerEvent(4);
                        }
                    }
                }
            }
            else if (day == 14 && time >= 14 * 60 && eventCount == 17)
            {
                isOn = false;
                TriggerStalkerEvent(9);
            }
        }
        if (isEndingEvent && Input.anyKeyDown)
        {
            EndingEvent();
        }
        if (isGameEnding && Input.anyKeyDown)
        {
            EndGameEvent(StalkerEvents.finalEventNumber);
        }
        if (currentStalkerEmail.activeSelf || currentStalkerDM.activeSelf)
        {
            if (dm1.activeSelf && day < 3)
            {
                dm1.SetActive(false);
                dm1Preview.SetActive(false);
                dm1Options.SetActive(false);
            }
            if ((email1.activeSelf || dm2.activeSelf) && day < 5)
            {
                email1.SetActive(false);
                email1Preview.SetActive(false);
                email1Options.SetActive(false);
                dm2.SetActive(false);
                dm2Preview.SetActive(false);
                dm2Options.SetActive(false);
            }
            if (email2.activeSelf && day < 9)
            {
                email2.SetActive(false);
                email2Preview.SetActive(false);
                email2Options.SetActive(false);
            }
            if (email3.activeSelf && day < 13)
            {
                email3.SetActive(false);
                email3Preview.SetActive(false);
                email3Options.SetActive(false);
            }
        }
        if (isEnd && Time.timeSinceLevelLoad >= displayTime + displayStartTime)
        {
            ReturnToMenu();
        }
    }

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

    private void SetMessageActive(GameObject message, GameObject messagePreview, GameObject messageOptions)
    {

        message.SetActive(true);
        messagePreview.SetActive(true);
        messageOptions.SetActive(true);
        
    }

    public void TriggerStalkerEvent(int numEvent)
    {
        
        if (numEvent != eventKeys.Count - 1)
        {
            // Handle stalker event logic here.
            string eventKey = eventKeys[numEvent];
            StalkerEvents stalkerEvent = stalkerEvents[eventKey];
            if (stalkerEvent.CanOccur())
            {
                eventHappening = stalkerEvent;
                string eventMessage = stalkerEvent.getEventMessage();
                eventNum = numEvent;

                // Play the associated sound for event
                PlayEventSound(stalkerEvent.getEventSound());

                string location = stalkerEvent.getEventLocation();
                stalkerEvent.IncrementOccurrences();
                eventCount++;

                // Check if the player is in the required location
                if (IsPlayerInRequiredLocation(location) || location == "Any")
                {
                    if (location != "Computer")
                    {
                        DisplayChoices(stalkerEvent);
                    }
                    else
                    {
                        if (stalkerEvent.getEventNumber() == 1)
                        {
                            eventMessage = "I just got an Email in Messages. I should check it soon.";
                            currentStalkerEmail.GetComponent<Button>().onClick.AddListener(() => DisplayChoices(stalkerEvent));
                        }
                        else if (stalkerEvent.getEventNumber() == 8)
                        {
                            eventMessage = "I just got a DM on Chitter. I should check it soon.";
                            currentStalkerDM.GetComponent<Button>().onClick.AddListener(() => DisplayChoices(stalkerEvent));
                        }

                        if (notification != null)
                        {
                            notification.showNotification(eventMessage);
                        }
                    }
                }
                else
                {
                    if (stalkerEvent.getEventNumber() == 1 || stalkerEvent.getEventNumber() == 8)
                    {
                        if (stalkerEvent.getEventNumber() == 1)
                        {
                            eventMessage = "I just got an Email in Messages. I should check it on my computer soon.";
                            currentStalkerEmail.GetComponent<Button>().onClick.AddListener(() => DisplayChoices(stalkerEvent));
                        }
                        else
                        {
                            eventMessage = "I just got a DM on Chitter. I should check it on my computer soon.";
                            currentStalkerDM.GetComponent<Button>().onClick.AddListener(() => DisplayChoices(stalkerEvent));
                        }
                    }
                    else
                    {
                        eventMessage = "I hear something in the " + location + "...";
                    }
                    if (notification != null)
                    {
                        notification.showNotification(eventMessage);
                    }
                    pendingEvent = numEvent;
                }
                if (stalkerEvent.getMaxOccurrences() > 1)
                {
                    ChangeEventText(stalkerEvent);
                }
            }
        }
        else
        {
            string eventKey = eventKeys[numEvent];
            StalkerEvents stalkerEvent = stalkerEvents[eventKey];
            string eventMessage = stalkerEvent.getEventMessage();
            eventNum = numEvent;

            if (notification != null)
            {
                notification.showNotification(eventMessage);
            }
            isEndingEvent = true;
            move.goToBathroom();
        }
    }

    private void PlayEventSound(AudioClip eventSound)
    {
        if (eventSound != null)
        {
            AudioSource.PlayClipAtPoint(eventSound, transform.position);
        }
    }

    public StalkerEvents getStalkerEvent()
    {
        return eventHappening;
    }

    private bool IsPlayerInRequiredLocation(string requiredLocation)
    {
        string playerLocation = player.getLocation().name;

        // Special condition for eventNum equal to 1 and 4
        if (eventNum == 1 || eventNum == 4)
        {
            // Allow eventNum 1 and 4 only in "Bedroom" or "Kitchen"
            return playerLocation == "Bedroom" || playerLocation == "Kitchen";
        }

        return playerLocation == requiredLocation;
    }

    public bool ContainsChoice(string choiceText)
    {
        // Check if playerChoices list contains a choice with the specified text
        return playerChoices.Any(choice => choice.choiceText == choiceText);
    }

    public void ChangeEventText(StalkerEvents stalkerEvent)
    {
        if (stalkerEvent.getCurrentOccurrences() > 1)
        {
            if (stalkerEvent.getEventNumber() == 1)
            {
                HandleEvent(stalkerEvent, "I got an email from NoticeMeSenpaii about a game they made and wanted me to download and play it while I�m streaming.",
                "Since they sent me an email asking me to play the game, they made I should send a reply to them. It�s a little weird that they said the game is about me but they are a fan of mine so it should be fine.",
                "They keep sending me very creepy emails, DMs, and packages. I need to report them.",
                "I get these types of emails a lot and would normally reply to them, but they have been very creepy and freaks me out. I should ignore them since there is nothing worth reporting in this email.");

                stalkerEvent.setChoice2("Report email");

                stalkerEvent.setChoice3("Ignore email");
                stalkerEvent.hasDisplayed = false;
            }
            else if (stalkerEvent.getEventNumber() == 4)
            {
                HandleEvent(stalkerEvent, "I got a package, again with no return address.On the package, it says \"To my beloved.You belong to me only! Soon we will be together forever. -NoticeMeSenpaii.\"",
                "I open the package and see that it�s a picture of me with my friends. There is a knife stabbed through me with a note that says, \"You�ll regret being with anyone else!\"",
                "The package freaks me out. I decided to throw away the package without opening it.",
                "The package freaks me out enough that I call the police. When they get here, they open the package telling me there is a picture of me with my friends with a knife stabbed through me and a note that says, \"You�ll regret being with anyone else!\" They take the package away as evidence.");

                stalkerEvent.setChoice1("Open package");

                stalkerEvent.setChoice2("Throw package away");

                stalkerEvent.setChoice3("Don't open package and call police");
            }
            else if (stalkerEvent.getEventNumber() == 5)
            {
                StalkerChoice secondChoice = stalkerEvent.getChoice2();
                stalkerEvent.changeChoiceNotification(secondChoice, "I screamed and grabbed my phone to call 911. The cops show up and tell me that no one was there but looks like something could have been there. They tell me that I should be cautious and make sure to lock my doors and windows.");

                stalkerEvent.setChoice3("Take photo with phone");
                StalkerChoice thirdChoice = stalkerEvent.getChoice3();
                stalkerEvent.changeChoiceNotification(thirdChoice, "I pretend like I don�t see anyone as I�m on my phone. I sneakily take a photo of the figure. Once I walk away, I call the cops and they take the photo as evidence and tell me they will be on the lookout for the suspicious person.");
            }
            else if (stalkerEvent.getEventNumber() == 7)
            {
                HandleEvent(stalkerEvent, "My phone rings. It�s a call from an unknown number.",
                "I answered the phone and said \"Hello?\" A robotic voice responds saying, \"You can only be with me and only me!\"",
                "I don�t know the number so decline the call. I little while later I see that I have a voicemail. I check it but all I hear is heavy breathing and then a robotic voice saying, \"You can only be with me and only me!\" I deleted the voicemail and blocked the number.",
                "Since I don�t know the number, I let it go to voicemail. I then listened to the voicemail that was just heavy breathing and then a robotic voice saying, \"You can only be with me and only me!\" I call the cops and give them the phone number and the recording of the voicemail as evidence.");

            }
            else if (stalkerEvent.getEventNumber() == 8)
            {
                HandleEvent(stalkerEvent, "I got another DM from NoticeMeSenpaii.",
                "They are one of my fans even though they are being creepy, so I need to respond to them. It would be rude to not respond to them.",
                "The last DM they sent was kind of creepy and this one is super creepy. I should report them. It�s better to report someone like this.",
                "The last DM they are being creepy, and I would rather not respond to them. It�s better not to respond to someone who just wants a reaction from me by sending this DM.");

                stalkerEvent.setChoice1("Reply to DM");

                stalkerEvent.setChoice2("Report DM");

                stalkerEvent.setChoice3("Ignore DM");

                stalkerEvent.hasDisplayed = false;
            }

            if (stalkerEvent.getCurrentOccurrences() > 2)
            {
                if (stalkerEvent.getEventNumber() == 1)
                {
                    HandleEvent(stalkerEvent, "I got an aggressive email from NoticeMeSenpaii",
                    "The email is aggressive and says how I belong to them and how if they can�t have me no one can. I respond by saying how I don�t belong to anyone but myself and if they keep on harassing me, I will report them.",
                    "I ignore the aggressive email hoping that they will leave me alone if I don�t respond to them.",
                    "I have been getting harassed repeatedly and the email is even threatening me.I decided to call the cops and give the email as evidence of the harassment.");

                    stalkerEvent.setChoice2("Ignore email");

                    stalkerEvent.setChoice3("Report email to cops");

                    stalkerEvent.hasDisplayed = false;
                }
                else if (stalkerEvent.getEventNumber() == 7)
                {
                    HandleEvent(stalkerEvent, "My phone rings. It�s a call from an unknown number.",
                    "I answered the phone and said \"Hello?\" A robotic voice responds by yelling, \"YOU�RE MINE! NO ONE ELSE CAN HAVE YOU! YOU BELONG TO ME!\"",
                    "I don�t know the number so decline the call.I little while later I see that I have a voicemail. I check it but all I hear is a robotic voice responding by yelling, \"YOU�RE MINE!NO ONE ELSE CAN HAVE YOU! YOU BELONGE TO ME!\" I deleted the voicemail and blocked the number.",
                    "Since I don�t know the number, I let it go to voicemail. I then listen to the voicemail where a robotic voice responds by yelling, \"YOU�RE MINE! NO ONE ELSE CAN HAVE YOU! YOU BELONGE TO ME!\" I call the cops and give them the phone number and the recording of the voicemail as evidence.");

                }
            }
        }
    }

    private void HandleEvent(StalkerEvents stalkerEvent, string eventMessage, string choice1Notification, string choice2Notification, string choice3Notification)
    {
        stalkerEvent.setEventMessage(eventMessage);

        StalkerChoice firstChoice = stalkerEvent.getChoice1();
        stalkerEvent.changeChoiceNotification(firstChoice, choice1Notification);

        StalkerChoice secondChoice = stalkerEvent.getChoice2();
        stalkerEvent.changeChoiceNotification(secondChoice, choice2Notification);

        StalkerChoice thirdChoice = stalkerEvent.getChoice3();
        stalkerEvent.changeChoiceNotification(thirdChoice, choice3Notification);
    }

    private void DisplayChoices(StalkerEvents stalkerEvent)
    {
        if (!stalkerEvent.isDisplayed())
        {
            choiceText.text = stalkerEvent.getEventMessage();
            choice1Text = choice1.GetComponentInChildren<TMP_Text>();
            choice2Text = choice2.GetComponentInChildren<TMP_Text>();
            choice3Text = choice3.GetComponentInChildren<TMP_Text>();

            // Get choices from the event
            List<StalkerChoice> choices = stalkerEvent.GetChoices();

            // Option 1
            choice1Text.text = choices.Count > 0 ? choices[0].choiceText : "";
            choice1.GetComponent<Button>().onClick.AddListener(() => HandlePlayerChoice(choices.Count > 0 ? choices[0] : null, stalkerEvent));

            // Option 2
            choice2Text.text = choices.Count > 1 ? choices[1].choiceText : "";
            choice2.GetComponent<Button>().onClick.AddListener(() => HandlePlayerChoice(choices.Count > 1 ? choices[1] : null, stalkerEvent));

            // Option 3
            choice3Text.text = choices.Count > 2 ? choices[2].choiceText : "";
            choice3.GetComponent<Button>().onClick.AddListener(() => HandlePlayerChoice(choices.Count > 2 ? choices[2] : null, stalkerEvent));

            stalkerEventHandler.SetActive(true);
            stalkerEvent.hasDisplayed = true;
        }
    }

    public void HandlePlayerChoice(StalkerChoice choice, StalkerEvents stalkerEvent)
    {
        if (choice != null)
        {
            choice.wellnessChange *= stalkerEvent.getWellness();
            choice.endingChange *= stalkerEvent.getEnding();
            choice.reputationChange *= stalkerEvent.getReputation();
            // Update player stats
            player.updateWellness(choice.wellnessChange);
            player.updateEnding(choice.endingChange);
            player.updateReputation(choice.reputationChange);

            playerChoices.Add(choice);

            // Hide the stalker event handler
            stalkerEventHandler.SetActive(false);

            // Check the length of choice.choiceNotification in terms of words
            int maxWordsForSingleNotification = 20;
            int wordCount = choice.choiceNotification.Split(' ').Length;

            // Show notification
            if (notification != null)
            {
                if (wordCount > maxWordsForSingleNotification)
                {
                    // If it's a long message, use queLongNotification
                    queLongNotification(choice.choiceNotification);
                }
                else
                {
                    // If it's a short message, use showNotification
                    notification.showNotification(choice.choiceNotification);
                }
            }
        }
        else
        {
            Debug.Log("Invalid Choice");
        }
    }

    public void queLongNotification(string longNotification)
    {
        int maxWordsPerNotification = 20;

        // Split the long notification into words
        string[] words = longNotification.Split(' ');

        // Split the words into smaller parts
        List<string> notificationParts = new List<string>();

        for (int i = 0; i < words.Length; i += maxWordsPerNotification)
        {
            int length = Mathf.Min(maxWordsPerNotification, words.Length - i);
            string part = string.Join(" ", words, i, length);
            notificationParts.Add(part);
        }

        notification.showNotification(notificationParts[0]);

        for (int i = 1; i < notificationParts.Count; i++)
        {
            notification.queNotification(notificationParts[i]);
        }
    }

    public void DefaultPlayerChoice(StalkerEvents stalkerEvent)
    {
        List<StalkerChoice> choices = stalkerEvent.GetChoices();
        StalkerChoice choice = choices[0];

        choice.wellnessChange *= stalkerEvent.getWellness();
        choice.endingChange *= stalkerEvent.getEnding();
        choice.reputationChange *= stalkerEvent.getReputation();
        // Update player stats
        player.updateWellness(choice.wellnessChange);
        player.updateEnding(choice.endingChange);
        player.updateReputation(choice.reputationChange);

        playerChoices.Add(choice);
        stalkerEvent.hasDisplayed = false;
    }

    private void EndGameEvent(int eventNum)
    {
        if (eventNum == StalkerEvents.finalEventNumber)
        {
            move.goToCredits();
            stalkerEventHandler.SetActive(false);
            isEnd = true;

            // Record start time
            displayStartTime = Time.timeSinceLevelLoad;
        }
    }

    private void ReturnToMenu()
    {

        move.goToMainMenu();
    }

    private void EndingEvent()
    {
        splashScreenManager.openSplashScreen("Ending");
        isEndingEvent = false;
        stalkerEventHandler.SetActive(true);

        if (choice1 != null) Destroy(choice1);
        if (choice2 != null) Destroy(choice2);
        if (choice3 != null) Destroy(choice3);

        if (player.getEnding() < -5)
        {
            choiceText.text = "Yesterday it was found that a famous streamer's house was broken into, they were found murdered. The suspect is still at large and is considered armed and dangerous";
            isGameEnding = true;
        }
        else if (player.getEnding() > -5 && player.getEnding() < 5)
        {
            choiceText.text = "Sadly yesterday another streamer has made a video stating that they can no longer deal with being a streamer and has decided to stop and to find a different job to make a living.";
            isGameEnding = true;
        }
        else if (player.getEnding() > 5)
        {
            choiceText.text = "Yesterday a person known as NoticeMeSenpaii was arrested while trying to break into a famous streamers house. They will be prosecuted, and the streamer was not harmed in this incident. ";
            isGameEnding = true;
        }
    }
}