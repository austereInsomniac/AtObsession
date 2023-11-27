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
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(1)); //Event number
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("I got a weird email..."));//Event text
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Bedroom"));//Event location
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(0));//Wellness change
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1)); // Ending change
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(5));//Rep change

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Interact"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I clicked on the email and clicked a weird link"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Ignore"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("I ignored the email"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Report"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("I reported the email as it seemed suspicious"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(5));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(1);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(2));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("There's something at the window!"));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Bedroom"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Look"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I didn't see anything but some rustling bushes"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Scream"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("Whatever it was it's gone"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Ignore"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("It's probably nothing"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(0));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(2);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(3));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("I got a weird gift in the mail..."));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Living room"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Open it"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I opened the gift and found something weird"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Leave it"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("I left the gift untouched"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Send back"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("I sent the gift back"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(0));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(3);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(4));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("Something's outside the window!"));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Kitchen"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Look out the window"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I looked outside"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Scream"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("I screamed and they ran away"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Avoid"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("I looked away"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(0));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(4);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(5));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("A fan asked me to play a game they sent!"));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Bedroom"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(5));

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Play the game"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I played the fan's game and ended up getting a virus on my computer"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Ignore them"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("I ignored the fan's request and kept with my regular schedule"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Decline"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("I declined to play"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(5));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(5);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(6));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("The phone is ringing! It's an unknown number..."));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Any"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Answer the call"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I answered the unknown call and heard someone breathing heavily"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Ignore"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("I ignored the call and nothing happened"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Decline"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("I declined the call"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(0));

        stalker.TriggerStalkerEvent(6);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(7));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("Someone left a comment that mentioned a private conversation I had."));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Any"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Delete the comment"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I deleted the uncomfortable comment"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Ignore"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("I ignored the comment"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Install cameras"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("I installed security cameras to make sure I'm not being watched"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(0));

        stalker.TriggerStalkerEvent(7);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(8));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("There's banging on the door!"));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Living room"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(5));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));

        //---------------------------------Choice 1---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice1().getText(), Is.EqualTo("Call the cops"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getNotification(), Is.EqualTo("I called the police"));
        Assert.That(stalker.getStalkerEvent().getChoice1().getWellnessChange(), Is.EqualTo(-3));
        Assert.That(stalker.getStalkerEvent().getChoice1().getEndingChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice1().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 2---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice2().getText(), Is.EqualTo("Try to ignore"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getNotification(), Is.EqualTo("I tried to ignore the banging"));
        Assert.That(stalker.getStalkerEvent().getChoice2().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice2().getEndingChange(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getChoice2().getReputationChange(), Is.EqualTo(0));
        //---------------------------------Choice 3---------------------------
        Assert.That(stalker.getStalkerEvent().getChoice3().getText(), Is.EqualTo("Check outside"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getNotification(), Is.EqualTo("I checked outside and I heard some noises coming from the bushes"));
        Assert.That(stalker.getStalkerEvent().getChoice3().getWellnessChange(), Is.EqualTo(-1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getEndingChange(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getChoice3().getReputationChange(), Is.EqualTo(0));

        yield return null;
    }
}
