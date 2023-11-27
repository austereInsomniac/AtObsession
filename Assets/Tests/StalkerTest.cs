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
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("I got a weird email..."));
        Assert.That(stalker.getStalkerEvent().getChoice1(),Is.EqualTo("Interact"));
        Assert.That(stalker.getStalkerEvent().getChoice2(), Is.EqualTo("Ignore"));
        Assert.That(stalker.getStalkerEvent().getChoice3(), Is.EqualTo("Report"));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Bedroom"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(0));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(5));

        //stalker.setEventNum(1);
        stalker.TriggerStalkerEvent(1);
        //Test Initialization to "Email" Stalker event
        Assert.That(stalker.getStalkerEvent().getEventNumber(), Is.EqualTo(2));
        Assert.That(stalker.getStalkerEvent().getEventMessage(), Is.EqualTo("There's something at the window!"));
        Assert.That(stalker.getStalkerEvent().getChoice1(), Is.EqualTo("Look"));
        Assert.That(stalker.getStalkerEvent().getChoice2(), Is.EqualTo("Ignore"));
        Assert.That(stalker.getStalkerEvent().getChoice3(), Is.EqualTo("Call 911"));
        Assert.That(stalker.getStalkerEvent().getEventLocation(), Is.EqualTo("Bedroom"));
        Assert.That(stalker.getStalkerEvent().getWellness(), Is.EqualTo(10));
        Assert.That(stalker.getStalkerEvent().getEnding(), Is.EqualTo(1));
        Assert.That(stalker.getStalkerEvent().getReputation(), Is.EqualTo(0));

        yield return null;
    }
}
