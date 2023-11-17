using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameStateTesting
{

    public GameObject player;
    public game_state gameState;

    [SetUp]
    public void setUp()
    {
        SceneManager.LoadScene("Scenes/BaseScene");
    }


    [UnityTest]
    public IEnumerator GameStateInitializeTesting()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        gameState = player.GetComponent<game_state>();

        Assert.IsNotNull(player);
        Assert.IsNotNull(gameState);

        Assert.AreSame(gameState.getLocation(), GameObject.Find("Main Menu"));
        Assert.AreSame(gameState.getLocationCanvas(), GameObject.Find("Main Menu Canvas"));

        Assert.AreEqual(gameState.getWellness(), 70);
        Assert.AreEqual(gameState.getDay(), 1);
        //Assert.AreEqual(gameState.getHunger() , );
        Assert.AreEqual(gameState.getReputation() ,25);
        Assert.AreEqual(gameState.getSubscribers() , 1000);
        Assert.AreEqual(gameState.getEnding() , 0);
        Assert.AreEqual(gameState.getMoney() , 100.00);
        yield return null;
    }

    [UnityTest]
    public IEnumerator GameStateUpdateTesting()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        gameState = player.GetComponent<game_state>();
        Assert.IsNotNull(player);
        Assert.IsNotNull(gameState);


        //Testing time
        gameState.updateTime(0);
        Assert.That(gameState.getTime(), Is.EqualTo(480));
        //Testing Day Advance
        gameState.updateTime(960);
        Assert.That(gameState.getDay(), Is.EqualTo(2));
        Assert.That(gameState.getTime(), Is.EqualTo(0));

        gameState.updateTime(240);
        Assert.That(gameState.getTime(), Is.EqualTo(240));

        //Testing Force Sleep
        gameState.updateTime(1);
        Assert.That(gameState.getDay(), Is.EqualTo(2));
        Assert.That(gameState.getTime(), Is.EqualTo(480));

        //Testing hunger
        gameState.updateTime(360);
        //Assert.AreSame(gameState)

        yield return null;
    }
}
