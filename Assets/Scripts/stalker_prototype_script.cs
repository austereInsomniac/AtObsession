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

    int maxOccurrences;
    int currentOccurrences;

    public StalkerEvents(string changeMessage, string location, int changeWellness, int newEventNumber, int changeEnding, int changeRep, int maxTimes, int currentTimes)
    {
        wellness = changeWellness;
        eventNumber = newEventNumber;
        ending = changeEnding;
        reputation = changeRep;
        eventMessage = changeMessage;
        eventLocation = location;
        choices = new List<StalkerChoice>();

        maxOccurrences = maxTimes;
        currentOccurrences = currentTimes;
    }

    public int getWellness() { return wellness; }
    public int getEventNumber() { return eventNumber; }
    public int getEnding() { return ending; }
    public int getReputation() { return reputation; }
    public string getEventMessage() { return eventMessage; }
    public string getEventLocation() {  return eventLocation; }
    public int getCurrentOccurrences() { return currentOccurrences; }
    public int getMaxOccurrences() { return maxOccurrences;}

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
    private bool isEndingEvent = false;
    List<string> eventKeys;
    Dictionary<string, StalkerEvents> stalkerEvents;
    List<StalkerChoice> playerChoices;
    private StalkerEvents eventHappening;

    private int initialEventCount = 0;
    private int eventNum;
    private int pendingEvent = -1; // Set to -1 to indicate no pending event
    private int eventCount;

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
    public GameObject checkChitterRight;
    private Button checkChitterRightButton;
    private GameObject checkMessages;
    public GameObject checkMessagesRight;
    private Button checkMessagesRightButton;
    private  GameObject currentStalkerEmail;
    private GameObject currentStalkerDM;

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
        //checkChitterRightButton = checkChitterRight.GetComponent<Button>();
        checkMessages = GameObject.Find("Check_Email");
        //checkMessagesRightButton = checkMessagesRight.GetComponent<Button>();
        currentStalkerEmail = GameObject.Find("Unread_Email_Six");
        currentStalkerDM = GameObject.Find("Unread_DM_Six");

        stalkerEvents = new Dictionary<string, StalkerEvents>();

        // Email event 0/1
        StalkerEvents emailEvent = new StalkerEvents("An unknown account has now sent me an email.", "Computer", 0, 1, 1, -1, 4, 0);
        emailEvent.AddChoice("Reply to the email", "I should replay to the email. They may be acting a little creepy, but they are one of my fans, they aren’t going to hurt me.", -3, -1, 0);
        emailEvent.AddChoice("Ignore the email", "They keep sending me creepy DMs and now they sent me an email. I should just ignore them and not interact with them at all.", -1, 0, 0);
        emailEvent.AddChoice("Report the email", "This person keeps on sending me really creepy DMs and now they are sending me a creepy email. I need to report them before this gets any worse.", -1, 1, 5);
        stalkerEvents.Add("Email", emailEvent);

        // Knocking on window event 1/2
        StalkerEvents knockingEvent = new StalkerEvents("As I’m near the window a hear a knocking on the window. Almost like someone is trying to get my attention.", "Bedroom", 10, 2, 1, 0, 1, 0);
        knockingEvent.AddChoice("Grab a flashlight and go outside to investigate it", "I grab a flashlight and go outside to see if there is anything there. No one is there but I can see where someone stood to look through the window.", -3, -1, 0);
        knockingEvent.AddChoice("Scream and call the cops", "I screamed and grabbed my phone to call 911. The cops show up and tell me that no one was there but looks like something could have been there but that it wasn’t likely there had been.", -1, 0, 0);
        knockingEvent.AddChoice("Take a photo with your phone", "I pretend like I don’t hear anything as I’m on my phone. I act like I got a phone call and hold my phone up to my ear and take a recording as I walk past the window. Once I walk look at the video and see that there is someone in my window. I call the cops and they take the video as evidence and tell me they will do what they can.", -1, 1, 0);
        stalkerEvents.Add("Knocking on window", knockingEvent);

        // Knocking on door event 2/3
        StalkerEvents doorEvent = new StalkerEvents("Someone is knocking on the front door", "Any", 10, 3, 1, 0, 1, 0);
        doorEvent.AddChoice("Open the door", "I open the door but there is no one there. I shrug and close the door.", -3, -1, 0);
        doorEvent.AddChoice("Hesitate but open the door slightly", "I hesitate to open the door. Someone knocks again so I decide to open the door slightly and look outside. NO ONE is there! I slam the door closed and lock it!", -1, 0, 0);
        doorEvent.AddChoice("Look through the peephole", "I decide to look through the peep hole of the door. No one is there. It concerns me a little bit so I make sure that my door is locked.", -1, 1, 0);
        stalkerEvents.Add("Knocking on door", doorEvent);

        // Suspicious gift event 3/4
        StalkerEvents giftEvent = new StalkerEvents("I got a package with no return address but on the package it says “From your biggest fan - ?????”", "Living room", 10, 4, 1, 0, 2, 0);
        giftEvent.AddChoice("Open the package and keep the gift", "I got a package without an address. When I opened the package there was a note and a gift. The note was a little creepy, but they say they are one of my biggest fans.", -3, -1, 0);
        giftEvent.AddChoice("Open the package but throw away the gift", "There is no return address but decided to open it to check the contents. There is a gift and a creepy note saying how much they love me and that we should be together. I decided to throw them away.", -1, 0, 0);
        giftEvent.AddChoice("Don’t open the package and throw it away", "There is no return address and even though it seems to be from a fan. To be safe I won’t be opening it and since there is not return address, I threw it away.", -1, 1, 0);
        stalkerEvents.Add("Suspicious gift", giftEvent);

        // Window figure event 4/5
        StalkerEvents windowEvent = new StalkerEvents("As I’m walking by my window I see a shadow figure looking through at me!", "Kitchen", 10, 5, 1, 0, 2, 0);
        windowEvent.AddChoice("Grab a flashlight and go outside to investigate it", "I grab a flashlight and go outside to see if there is anything there. No one is there but I can see where someone stood to look through the window.", -3, -1, 0);
        windowEvent.AddChoice("Scream and call the cops", "I screamed and grabbed my phone to call 911. The cops show up and tell me that no one was there but looks like something could have been there but that it wasn’t likely there had been.", -1, 0, 0);
        windowEvent.AddChoice("Look out the window", "I kept calm and looked out the window. There was nothing there but decided to lock my door and windows and to keep my phone near me, just in case.", -1, 1, 0);
        stalkerEvents.Add("Window figure", windowEvent);

        // Fan game event 5/6
        StalkerEvents fanGameEvent = new StalkerEvents("While streaming I get a message from the unknown account asking me to play the game they made.", "Bedroom", 10, 6, 1, -1, 1, 0);
        fanGameEvent.AddChoice("Download and play the game", "I downloaded the game and played the game while streaming. It was a creepy game all about me and what I do in my daily life.", -3, -1, 0);
        fanGameEvent.AddChoice("Download the game but don’t play it", "I decided to download it but decided not to play it. I told them that I might play it at a later time.", -1, 0, 0);
        fanGameEvent.AddChoice("Decline to download and play the game", "I tell them that I will not be downloading the game for safety reasons and that if they wish for me to play it they should upload it to a secure gaming site.", -1, 1, 5);
        stalkerEvents.Add("Fan game", fanGameEvent);

        // Unknown call event 6/7
        StalkerEvents unknownCallEvent = new StalkerEvents("My phone rings. It’s a call from an unknown number.", "Any", 10, 7, 1, 0, 3, 0);
        unknownCallEvent.AddChoice("Answer the call", "I answered the phone and say “Hello?” No one responds but I can hear someone breathing heavily on the other end. I say “Hello?” again with no response. It freaks me out, so I hang up.", -3, -1, 0);
        unknownCallEvent.AddChoice("Decline the call", "I don’t know the number so decline the call. I little while later I see that I have a voicemail. I check it but all I hear is heavy breathing and then a robotic voice saying they love me and only they can be with me. It freaks me out and I delete the voicemail.", -1, 0, 0);
        unknownCallEvent.AddChoice("Let it go to voicemail", "Since I don’t know the number, I let it go to voicemail. I then listen to the voicemail that just heavy breathing and then a robotic voice saying they love me and only they can be with me. It freaks me out, but I should save the voicemail just in case and block this number.", -1, 1, 0);
        stalkerEvents.Add("Unknown call", unknownCallEvent);

        // Direct message event 7/8
        StalkerEvents dmEvent = new StalkerEvents("Looks like I got a new DM from a user named NoticeMeSenpaiii", "Computer", 10, 8, 1, 0, 2, 0);
        dmEvent.AddChoice("Report the DM", "I don’t know this person and the DM is creepy. I should report them.", -3, -1, 0);
        dmEvent.AddChoice("Reply to the DM", "I don’t know this person and they seem a little creepy, but I should respond to them since they are one of my fans.", -1, 0, 0);
        dmEvent.AddChoice("Ignore the DM", "I don’t know this person and the DM makes them seem a little creepy. I’m going to ignore them.", -1, 1, 0);
        stalkerEvents.Add("Direct message", dmEvent);

        // Banging on door event 8/9
        StalkerEvents bangingEvent = new StalkerEvents("BANG, BANG, BANG! I can hear someone banging on my front door.", "Any", 10, 9, 5, 0, 1, 0);
        bangingEvent.AddChoice("Open the door", "I call out, “I’m coming, I’m coming!” When I open the door no one is there. I turn around and see a note nailed to my door saying, “This is the last time! YOU ARE MINE!” I sneer as I tear down the note and close the door.", -3, -1, 0);
        bangingEvent.AddChoice("Scream out \"I’m calling the police!\"", "Knowing that I’ve been getting threats I scream out that I’m calling the police. When they get here, they say no one was there but they found a note nailed to the door saying, \"This is the last time! YOU ARE MINE!\"", -1, 0, 0);
        bangingEvent.AddChoice("Look through the peep hole", "I checked who is at the door through the peep hole. NO ONE is there. I cautiously open the door to look outside. I see that there is a note nailed to my door saying, \"This is the last time! YOU ARE MINE!\" I decide to call the police and give them the letter for evidence.", -1, 1, 0);
        stalkerEvents.Add("Banging on door", bangingEvent);

        // Trapped in bathroom event 9/10
        StalkerEvents bathroomEvent = new StalkerEvents("The bathroom door is locked!", "Any", 0, 10, 0, 0, 1, 1);
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
             
            }
            string location = stalkerEvent.getEventLocation();
            if (newLocation.name == location)
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

                checkChitterRightButton.onClick.AddListener(() => SetMessageActive(currentStalkerDM));
                
            }
            else if (day == 5 && time >= 10 * 60 && eventCount == 1)
            {
                currentStalkerDM = GameObject.Find("Unread_DM_Four");
                isOn = false;
                TriggerStalkerEvent(7);

                checkChitterRightButton.onClick.AddListener(() => SetMessageActive(currentStalkerDM));
                if (time >= 15 * 60 && eventCount == 2)
                {
                    isOn = false;
                    TriggerStalkerEvent(0);

                    checkMessagesRightButton.onClick.AddListener(() => SetMessageActive(currentStalkerEmail));
                }
            }
            else if (day == 7 && time >= 13 * 60 && eventCount == 3)
            {
                isOn = false;
                TriggerStalkerEvent(2);
            }
            else if (day == 9 && time >= 13 * 60 && eventCount == 5)
            {
                currentStalkerEmail = GameObject.Find("Unread_Email_Three");
                isOn = false;
                TriggerStalkerEvent(0);

                checkMessagesRightButton.onClick.AddListener(() => SetMessageActive(currentStalkerEmail));

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
                    currentStalkerEmail = GameObject.Find("Unread_Email_Four");
                    isOn = false;
                    TriggerStalkerEvent(0);

                    checkMessagesRightButton.onClick.AddListener(() => SetMessageActive(currentStalkerEmail));

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
            EndGameEvent(StalkerEvents.finalEventNumber);
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

    private void SetMessageActive(GameObject message)
    {
        message.SetActive(true);
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
                Debug.Log(eventMessage);
                eventNum = numEvent;

                string location = stalkerEvent.getEventLocation();
                stalkerEvent.IncrementOccurrences();

                // Check if the player is in the required location
                if (IsPlayerInRequiredLocation(location) || location == "Any")
                {
                    if (location != "Computer")
                    {
                        DisplayChoices(stalkerEvent);
                        eventCount++;
                    }
                    else
                    {
                        if (stalkerEvent.getEventNumber() == 1)
                        {              
                            currentStalkerEmail.GetComponent<Button>().onClick.AddListener(() => DisplayChoices(stalkerEvent));
                        }
                        else if (stalkerEvent.getEventNumber() == 7)
                        {
                            currentStalkerDM.GetComponent<Button>().onClick.AddListener(() => DisplayChoices(stalkerEvent));
                        }
                    }
                }
                else
                {
                    if (numEvent == 0 || numEvent == 7)
                    {
                        if (numEvent == 0)
                        {
                            eventMessage = "I just got an Email in Messages. I should check it on my computer soon.";
                        }
                        else
                        {
                            eventMessage = "I just got a DM on Chitter. I should check it on my computer soon.";
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

            EndingEvent(stalkerEvent);
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
                HandleEvent(stalkerEvent, "I got an email from an unknown account that contains a game they made and wanted me to download and play it while I’m streaming.",
                "Since they sent me an email asking me to play the game, they made I should send a reply to them. It’s a little weird that they said the game is about me but they are a fan of mine so it should be fine.",
                "They keep sending me very creepy emails, DMs, and package. I need to report them.",
                "I get these types of emails a lot and would normally reply to them, but they have been very creepy and freaks me out. I should ignore them since there is nothing worth reporting in this email.");

                stalkerEvent.setChoice2("Report the email");

                stalkerEvent.setChoice3("Ignore the email");
            }
            else if (stalkerEvent.getEventNumber() == 4)
            {
                HandleEvent(stalkerEvent, "I got a package, again with no return address. On the package it says \"To my beloved. You belong to me only! Soon we will be together forever.\"",
                "I open the package and see that it’s a picture of me with my friends. There is a knife stabbed through me with a note that says, \"You’ll regret being with anyone else!\"",
                "The package freaks me out. I decided to throw away the package without opening it.",
                "The package freaks me out enough that I call the police. When they get here they open the package telling me there is a picture of me with my friends with a knife stabbed through me and a note that saying, \"You’ll regret being with anyone else!\" They take the package away as evidence.");

                stalkerEvent.setChoice1("Open the package");

                stalkerEvent.setChoice2("Throw the package away");

                stalkerEvent.setChoice3("Don't open the package and call the police");
            }
            else if (stalkerEvent.getEventNumber() == 5)
            {
                StalkerChoice secondChoice = stalkerEvent.getChoice2();
                stalkerEvent.changeChoiceNotification(secondChoice, "I screamed and grabbed my phone to call 911. The cops show up and tell me that no one was there but looks like something could have been there. They tell me that I should be cautious and make sure to lock my doors and windows.");

                stalkerEvent.setChoice3("Take a photo with your phone");
                StalkerChoice thirdChoice = stalkerEvent.getChoice3();
                stalkerEvent.changeChoiceNotification(thirdChoice, "I pretend like I don’t see anyone as I’m on my phone. I sneakily take a photo of the figure. Once I walk away, I call the cops and they take the photo as evidence and tell me they will be on the lookout for the suspicious person.");
            }
            else if (stalkerEvent.getEventNumber() == 7)
            {
                HandleEvent(stalkerEvent, "My phone rings. It’s a call from an unknown number.",
                "I answered the phone and say \"Hello?\" A robotic voice responds saying, \"You can only be with me and only me!\"",
                "I don’t know the number so decline the call. I little while later I see that I have a voicemail. I check it but all I hear is heavy breathing and then a robotic voice saying, \"You can only be with me and only me!\" I delete the voicemail and block the number.",
                "Since I don’t know the number, I let it go to voicemail. I then listen to the voicemail that just heavy breathing and then a robotic voice saying, \"You can only be with me and only me!\" I call the cops and give them the phone number and the recording of the voicemail as evidence.");

            }
            else if (stalkerEvent.getEventNumber() == 8)
            {
                HandleEvent(stalkerEvent, "I got another DM from NoticeMeSenpaiii.",
                "They are one of my fans even though they are being creepy, so I need to respond to them. It would be rude to not respond to them.",
                "The last DM they sent was kinda creepy and this one is super creepy. I should totally report them. It’s better to report someone like this.",
                "The last DM they are being creepy, and I would rather not respond to them. It’s better not to respond to someone who just wants a reaction from me by sending this DM.");

                stalkerEvent.setChoice1("Reply to the DM");

                stalkerEvent.setChoice2("Report the DM");

                stalkerEvent.setChoice3("Ignore the DM");
            }

            if (stalkerEvent.getCurrentOccurrences() > 2)
            {
                if (stalkerEvent.getEventNumber() == 1)
                {
                    HandleEvent(stalkerEvent, "I got an aggressive email from the unknown account.",
                    "The email is aggressive and says how I belong to them and how if they can’t have me no one can. I respond saying how I don’t belong to anyone but myself and if they keep on harassing me I will report them.",
                    "I ignore the aggressive email hoping that they will leave me alone if I don’t respond to them.",
                    "I have been getting harassed repeatedly and the email is even threatening me. I decided to call the cops and give the email as evidence for the harassment.");

                    stalkerEvent.setChoice2("Ignore the email");

                    stalkerEvent.setChoice3("Report the email to the cops");
                }
                else if (stalkerEvent.getEventNumber() == 7)
                {
                    HandleEvent(stalkerEvent, "My phone rings. It’s a call from an unknown number.",
                    "I answered the phone and say \"Hello?\" A robotic voice responds by yelling, \"YOU\'RE MINE! NO ONE ELSE CAN HAVE YOU! YOU BELONG TO ME!\"",
                    "I don’t know the number so decline the call. I little while later I see that I have a voicemail. I check it but all I hear is a robotic voice responds by yelling, \"YOU\'RE MINE! NO ONE ELSE CAN HAVE YOU! YOU BELONG TO ME!\" I delete the voicemail and block the number.",
                    "Since I don’t know the number, I let it go to voicemail. I then listen to the voicemail where a robotic voice responds by yelling, \"YOU\'RE MINE! NO ONE ELSE CAN HAVE YOU! YOU BELONG TO ME!\" I call the cops and give them the phone number and the recording of the voicemail as evidence.");

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

            // Show notification
            if (notification != null)
            {
                notification.showNotification(choice.choiceNotification);
            }
        }
        else
        {
            Debug.Log("Invalid Choice");
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
    }

    private void EndGameEvent(int eventNum)
    {
        if (eventNum == StalkerEvents.finalEventNumber)
        {
            splashScreenManager.openSplashScreen("Game over");
            move.goToGameOver();
            stalkerEventHandler.SetActive(false);
        }
    }

    private void EndingEvent(StalkerEvents stalkerEvent)
    {
        move.moveLocation(GameObject.Find("Bathroom"), GameObject.Find("Bathroom Canvas"), player.getLocation(), player.getLocationCanvas());
        stalkerEventHandler.SetActive(true);

        if (choice1 != null) Destroy(choice1);
        if (choice2 != null) Destroy(choice2);
        if (choice3 != null) Destroy(choice3);

        if (player.getEnding() < -5)
        {
            choiceText.text = stalkerEvent.getEventMessage() + "\nBad Ending- The Stalker broke in and found you";
            isEndingEvent = true;
        }
        else if (player.getEnding() > -5 && player.getEnding() < 5)
        {
            choiceText.text = "\nNeutral Ending- You called 911, the cops find nothing and arrest you for repeated false 911 calls";
        }
        else if (player.getEnding() > 5)
        {
            choiceText.text = stalkerEvent.getEventMessage() + "\nGood Ending- You called 911 and the stalker was arrested";
            isEndingEvent = true;
        }
    }
}