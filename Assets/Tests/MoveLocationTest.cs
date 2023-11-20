using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MoveLocationTest
{
    public GameObject player;
    public game_state gameState;
    public move_location loc;

    [SetUp]
    public void setUp()
    {
        SceneManager.LoadScene("Scenes/BaseScene");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MoveLocationTesting()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        gameState = player.GetComponent<game_state>();
        loc = player.GetComponent<move_location>();
        Assert.IsNotNull(player);
        Assert.IsNotNull(gameState);
        Assert.IsNotNull(loc);

        //Verify that the game starts on the main menu
        Assert.That(loc.getMainMenu().GetInstanceID(), Is.EqualTo(gameState.getLocation().GetInstanceID()));
        Assert.That(loc.getMainMenuCanvas().GetInstanceID(), Is.EqualTo(gameState.getLocationCanvas().GetInstanceID()));

        //Move to Bedroom and verify
        loc.goToBedroom();
        Assert.That(loc.getBedroom().GetInstanceID(), Is.EqualTo(gameState.getLocation().GetInstanceID()));
        Assert.That(loc.getBedroomCanvas().GetInstanceID(), Is.EqualTo(gameState.getLocationCanvas().GetInstanceID()));
        //Verify that you stay in the bedroom and nothing changes
        loc.goToBedroom();
        Assert.That(loc.getBedroom().GetInstanceID(), Is.EqualTo(gameState.getLocation().GetInstanceID()));
        Assert.That(loc.getBedroomCanvas().GetInstanceID(), Is.EqualTo(gameState.getLocationCanvas().GetInstanceID()));

        //Move to Bathroom and Verify
        loc.goToBathroom();
        Assert.That(loc.getBathroom().GetInstanceID(), Is.EqualTo(gameState.getLocation().GetInstanceID()));
        Assert.That(loc.getBathroomCanvas().GetInstanceID(), Is.EqualTo(gameState.getLocationCanvas().GetInstanceID()));

        //Move to Livingroom and Verify
        loc.goToLivingRoom();
        Assert.That(loc.getLivingRoom().GetInstanceID(), Is.EqualTo(gameState.getLocation().GetInstanceID()));
        Assert.That(loc.getLivingRoomCanvas().GetInstanceID(), Is.EqualTo(gameState.getLocationCanvas().GetInstanceID()));
        
        //Move to kitchen and verify
        loc.goToKitchen();
        Assert.That(loc.getKitchen().GetInstanceID(), Is.EqualTo(gameState.getLocation().GetInstanceID()));
        Assert.That(loc.getKitchenCanvas().GetInstanceID(), Is.EqualTo(gameState.getLocationCanvas().GetInstanceID()));

        //Move to game over screen and verify
        loc.goToGameOver();
        Assert.That(loc.getGameOver().GetInstanceID(), Is.EqualTo(gameState.getLocation().GetInstanceID()));
        Assert.That(loc.getGameOverCanvas().GetInstanceID(), Is.EqualTo(gameState.getLocationCanvas().GetInstanceID()));



        yield return null;
    }
}
