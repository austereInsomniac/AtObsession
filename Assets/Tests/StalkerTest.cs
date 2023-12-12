using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class StalkerTest
{
    public GameObject player;
    public game_state gameState;
    public stalker_prototype_script stalker;

    [SetUp]
    public void setUp() //Loads the scene
    {
        SceneManager.LoadScene("Scenes/BaseScene");
    }


    [UnityTest]
    public IEnumerator StalkerTesting()
    {
        //Find player and assign game state script
        player = GameObject.FindGameObjectWithTag("MainCamera");
        gameState = player.GetComponent<game_state>();
        stalker = player.GetComponent<stalker_prototype_script>();
        //Verify player and gamestate exsist
        Assert.IsNotNull(player);
        Assert.IsNotNull(gameState);
        Assert.IsNotNull(stalker);



        //Test Initialization to "Email" Stalker event
    
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("An unknown account has now sent me an email."));//Event text
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Computer"));//Event location
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(0));//Wellness change
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(1)); //Event number
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1)); // Ending change
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(-1));//Rep change
        Assert.That(stalker.getStalkerEvent().getMaxOccurrences(), Is.EqualTo(4));//Max times
        Assert.That(stalker.getStalkerEvent().getCurrentOccurrences, Is.EqualTo(0));//Current times

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Reply to the email"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I should replay to the email. They may be acting a little creepy, but they are one of my fans, they aren’t going to hurt me."));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Ignore the email"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("They keep sending me creepy DMs and now they sent me an email. I should just ignore them and not interact with them at all."));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Report the email"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("This person keeps on sending me really creepy DMs and now they are sending me a creepy email. I need to report them before this gets any worse."));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(5));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(1);
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("As I’m near the window a hear a knocking on the window. Almost like someone is trying to get my attention."));//Event text
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Bedroom"));//Event location
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));//Wellness change
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(2)); //Event number
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1)); // Ending change
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));//Rep change
        Assert.That(stalker.getStalkerEvent().getMaxOccurrences(), Is.EqualTo(1));//Max times
        Assert.That(stalker.getStalkerEvent().getCurrentOccurrences, Is.EqualTo(1));//Current times

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Grab a flashlight and go outside to investigate it"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I grab a flashlight and go outside to see if there is anything there. No one is there but I can see where someone stood to look through the window."));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Scream and call the cops"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("I screamed and grabbed my phone to call 911. The cops show up and tell me that no one was there but looks like something could have been there but that it wasn’t likely there had been."));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Take a photo with your phone"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("I pretend like I don’t hear anything as I’m on my phone. I act like I got a phone call and hold my phone up to my ear and take a recording as I walk past the window. Once I walk look at the video and see that there is someone in my window. I call the cops and they take the video as evidence and tell me they will do what they can."));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(0));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(2);
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("Someone is knocking on the front door"));//Event text
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Any"));//Event location
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));//Wellness change
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(3)); //Event number
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1)); // Ending change
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));//Rep change
        Assert.That(stalker.getStalkerEvent().getMaxOccurrences(), Is.EqualTo(1));//Max times
        Assert.That(stalker.getStalkerEvent().getCurrentOccurrences, Is.EqualTo(1));//Current times

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Open the door"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I open the door but there is no one there. I shrug and close the door."));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Hesitate but open the door slightly"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("I hesitate to open the door. Someone knocks again so I decide to open the door slightly and look outside. NO ONE is there! I slam the door closed and lock it!"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Look through the peephole"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("I decide to look through the peep hole of the door. No one is there. It concerns me a little bit so I make sure that my door is locked."));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(0));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(3);
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("I got a package with no return address but on the package it says “From your biggest fan - ?????”"));//Event text
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Living room"));//Event location
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));//Wellness change
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(4)); //Event number
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1)); // Ending change
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));//Rep change
        Assert.That(stalker.getStalkerEvent().getMaxOccurrences(), Is.EqualTo(2));//Max times
        Assert.That(stalker.getStalkerEvent().getCurrentOccurrences, Is.EqualTo(1));//Current times

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Open the package and keep the gift"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I got a package without an address. When I opened the package there was a note and a gift. The note was a little creepy, but they say they are one of my biggest fans."));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Open the package but throw away the gift"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("There is no return address but decided to open it to check the contents. There is a gift and a creepy note saying how much they love me and that we should be together. I decided to throw them away."));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Don’t open the package and throw it away"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("There is no return address and even though it seems to be from a fan. To be safe I won’t be opening it and since there is not return address, I threw it away."));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(0));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(4);
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("As I’m walking by my window I see a shadow figure looking through at me!"));//Event text
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Kitchen"));//Event location
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));//Wellness change
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(5)); //Event number
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1)); // Ending change
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));//Rep change
        Assert.That(stalker.getStalkerEvent().getMaxOccurrences(), Is.EqualTo(2));//Max times
        Assert.That(stalker.getStalkerEvent().getCurrentOccurrences, Is.EqualTo(1));//Current times

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Grab a flashlight and go outside to investigate it"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I grab a flashlight and go outside to see if there is anything there. No one is there but I can see where someone stood to look through the window."));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Scream and call the cops"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("I screamed and grabbed my phone to call 911. The cops show up and tell me that no one was there but looks like something could have been there but that it wasn’t likely there had been."));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Look out the window"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("I kept calm and looked out the window. There was nothing there but decided to lock my door and windows and to keep my phone near me, just in case."));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(0));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(5);
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("While streaming I get a message from the unknown account asking me to play the game they made."));//Event text
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Bedroom"));//Event location
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));//Wellness change
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(6)); //Event number
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1)); // Ending change
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(-1));//Rep change
        Assert.That(stalker.getStalkerEvent().getMaxOccurrences(), Is.EqualTo(1));//Max times
        Assert.That(stalker.getStalkerEvent().getCurrentOccurrences, Is.EqualTo(1));//Current times

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Download and play the game"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I downloaded the game and played the game while streaming. It was a creepy game all about me and what I do in my daily life."));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Download the game but don’t play it"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("I decided to download it but decided not to play it. I told them that I might play it at a later time."));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Decline to download and play the game"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("I tell them that I will not be downloading the game for safety reasons and that if they wish for me to play it they should upload it to a secure gaming site."));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(5));

        stalker.TriggerStalkerEvent(6);
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("My phone rings. It’s a call from an unknown number."));//Event text
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Any"));//Event location
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));//Wellness change
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(7)); //Event number
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1)); // Ending change
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));//Rep change
        Assert.That(stalker.getStalkerEvent().getMaxOccurrences(), Is.EqualTo(3));//Max times
        Assert.That(stalker.getStalkerEvent().getCurrentOccurrences, Is.EqualTo(1));//Current times

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Answer the call"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I answered the phone and say “Hello?” No one responds but I can hear someone breathing heavily on the other end. I say “Hello?” again with no response. It freaks me out, so I hang up."));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Decline the call"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("I don’t know the number so decline the call. I little while later I see that I have a voicemail. I check it but all I hear is heavy breathing and then a robotic voice saying they love me and only they can be with me. It freaks me out and I delete the voicemail."));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Let it go to voicemail"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("Since I don’t know the number, I let it go to voicemail. I then listen to the voicemail that just heavy breathing and then a robotic voice saying they love me and only they can be with me. It freaks me out, but I should save the voicemail just in case and block this number."));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(0));

        stalker.TriggerStalkerEvent(7);
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("Looks like I got a new DM from an unknown account"));//Event text
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Bedroom"));//Event location
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));//Wellness change
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(8)); //Event number
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1)); // Ending change
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));//Rep change
        Assert.That(stalker.getStalkerEvent().getMaxOccurrences(), Is.EqualTo(2));//Max times
        Assert.That(stalker.getStalkerEvent().getCurrentOccurrences, Is.EqualTo(1));//Current times

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Report the DM"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I don’t know this person and the DM is creepy. I should report them."));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Reply to the DM"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("I don’t know this person and they seem a little creepy, but I should respond to them since they are one of my fans."));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Ignore the DM"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("I don’t know this person and the DM makes them seem a little creepy. I’m going to ignore them."));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(0));

        stalker.TriggerStalkerEvent(8);
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("BANG, BANG, BANG! I can hear someone banging on my front door."));//Event text
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Any"));//Event location
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));//Wellness change
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(9)); //Event number
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(5)); // Ending change
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));//Rep change
        Assert.That(stalker.getStalkerEvent().getMaxOccurrences(), Is.EqualTo(1));//Max times
        Assert.That(stalker.getStalkerEvent().getCurrentOccurrences, Is.EqualTo(1));//Current times

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Open the door"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I call out, “I’m coming, I’m coming!” When I open the door no one is there. I turn around and see a note nailed to my door saying, “This is the last time! YOU ARE MINE!” I sneer as I tear down the note and close the door."));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Scream out \"I’m calling the police!\""));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("Knowing that I’ve been getting threats I scream out that I’m calling the police. When they get here, they say no one was there but they found a note nailed to the door saying, \"This is the last time! YOU ARE MINE!\""));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Look through the peep hole"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("I checked who is at the door through the peep hole. NO ONE is there. I cautiously open the door to look outside. I see that there is a note nailed to my door saying, \"This is the last time! YOU ARE MINE!\" I decide to call the police and give them the letter for evidence."));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(0));

        yield return null;
    }

    [UnityTest]
    public IEnumerator StalkerChoiceTesting() { 
        //Find player and assign game state script
        player = GameObject.FindGameObjectWithTag("MainCamera");
        gameState = player.GetComponent<game_state>();
        stalker = player.GetComponent<stalker_prototype_script>();
        //Verify player and gamestate exsist
        Assert.IsNotNull(player);
        Assert.IsNotNull(gameState);
        Assert.IsNotNull(stalker);
        gameState.testingVideoWellness = true;

        stalker.TriggerStalkerEvent(0);
        //---------------------------------Choice 1---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice1(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(70));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 2---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice2(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(69));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 3---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice3(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(65));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));
        Assert.That(gameState.getReputation(), Is.EqualTo(55));

        //STAT RESET TO NORMAL
        gameState.updateReputation(-5);
        gameState.updateWellness(5);
        Assert.That(gameState.getWellness(), Is.EqualTo(70));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));

        //EVENT 2
        stalker.TriggerStalkerEvent(1);
        //---------------------------------Choice 1---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice1(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(67));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 2---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice2(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(66));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 3---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice3(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(65));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));

        //STAT RESET TO NORMAL
        //gameState.updateReputation(-5);
        gameState.updateWellness(5);
        Assert.That(gameState.getWellness(), Is.EqualTo(70));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));

        //EVENT 3
        stalker.TriggerStalkerEvent(2);
        //---------------------------------Choice 1---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice1(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(67));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 2---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice2(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(66));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 3---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice3(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(65));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));

        //STAT RESET TO NORMAL
        //gameState.updateReputation(-5);
        gameState.updateWellness(5);
        Assert.That(gameState.getWellness(), Is.EqualTo(70));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));

        //EVENT 4
        stalker.TriggerStalkerEvent(3);
        //---------------------------------Choice 1---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice1(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(67));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 2---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice2(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(66));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 3---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice3(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(65));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));

        //STAT RESET TO NORMAL
        //gameState.updateReputation(-5);
        gameState.updateWellness(5);
        Assert.That(gameState.getWellness(), Is.EqualTo(70));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));

        //EVENT 5
        stalker.TriggerStalkerEvent(4);
        //---------------------------------Choice 1---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice1(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(67));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 2---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice2(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(66));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 3---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice3(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(65));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));
        Assert.That(gameState.getReputation(), Is.EqualTo(55));

        //STAT RESET TO NORMAL
        gameState.updateReputation(-5);
        gameState.updateWellness(5);
        Assert.That(gameState.getWellness(), Is.EqualTo(70));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));

        //EVENT 6
        stalker.TriggerStalkerEvent(5);
        //---------------------------------Choice 1---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice1(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(67));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 2---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice2(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(66));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 3---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice3(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(65));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));

        //STAT RESET TO NORMAL
        //gameState.updateReputation(-5);
        gameState.updateWellness(5);
        Assert.That(gameState.getWellness(), Is.EqualTo(70));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));

        //EVENT 7
        stalker.TriggerStalkerEvent(6);
        //---------------------------------Choice 1---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice1(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(67));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 2---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice2(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(66));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 3---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice3(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(65));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));

        //STAT RESET TO NORMAL
        //gameState.updateReputation(-5);
        gameState.updateWellness(5);
        Assert.That(gameState.getWellness(), Is.EqualTo(70));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));

        //EVENT 8
        stalker.TriggerStalkerEvent(7);
        //---------------------------------Choice 1---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice1(),stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(67));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 2---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice2(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(66));
        Assert.That(gameState.getEnding(), Is.EqualTo(-1));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        //---------------------------------Choice 3---------------------------
        stalker.HandlePlayerChoice(stalker.getStalkerEvent().getChoice3(), stalker.getStalkerEvent());
        Assert.That(gameState.getWellness(), Is.EqualTo(65));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));

        //STAT RESET TO NORMAL
        //gameState.updateReputation(-5);
        gameState.updateWellness(5);
        Assert.That(gameState.getWellness(), Is.EqualTo(70));
        Assert.That(gameState.getReputation(), Is.EqualTo(50));
        Assert.That(gameState.getEnding(), Is.EqualTo(0));

        yield return null;
    }
}
