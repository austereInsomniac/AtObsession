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

        //Assert.That(loc.getMainMenu().GetInstanceID, Is.EqualTo(loc.getThisO().GetInstanceID()));

        Assert.AreSame(gameState.getLocation(), loc.getThisO());

        yield return null;
    }
}
