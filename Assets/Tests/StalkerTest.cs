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

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(1);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(2));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("There's something at the window!"));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Bedroom"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(2);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(3));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("I got a weird gift in the mail..."));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Living room"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(3);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(4));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("Something's outside the window!"));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Kitchen"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(4);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(5));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("A fan asked me to play a game they sent!"));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Bedroom"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(5));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(5);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(6));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("The phone is ringing! It's an unknown number..."));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Any"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));

        stalker.TriggerStalkerEvent(6);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(7));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("Someone left a comment that mentioned a private conversation I had."));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Any"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));

        stalker.TriggerStalkerEvent(7);
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(8));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("There's banging on the door!"));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Living room"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(5));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));


        yield return null;
    }
}
