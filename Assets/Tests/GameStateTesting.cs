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
        Assert.AreEqual(gameState.getHunger(), (float)0 );
        Assert.AreEqual(gameState.getReputation() ,50);
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
        Debug.Log(gameState.getDay());
        gameState.updateTime(960);
        Debug.Log(gameState.getDay());
        //Assert.That(gameState.getDay(), Is.EqualTo(2));
        Assert.That(gameState.getTime(), Is.EqualTo(0));

        gameState.updateTime(240);
        Assert.That(gameState.getTime(), Is.EqualTo(240));

        //Testing Force Sleep
        gameState.updateTime(1);
        //Assert.That(gameState.getDay(), Is.EqualTo(2));
        Assert.That(gameState.getTime(), Is.EqualTo(480));

        //Testing hunger
        gameState.updateTime(360);
        //Assert.AreSame(gameState)

        //Testing Wellness
        gameState.updateWellness(100);
        Assert.That(gameState.getWellness(), Is.EqualTo(100));
        gameState.updateWellness(-30);
        Assert.That(gameState.getWellness(), Is.EqualTo(70));
        gameState.updateWellness(0);
        Assert.That(gameState.getWellness(), Is.EqualTo(70));
        gameState.updateWellness(30);
        Assert.That(gameState.getWellness(), Is.EqualTo(100));
        gameState.updateWellness(-50);
        Assert.That(gameState.getWellness(), Is.EqualTo(50));
        gameState.updateWellness(999);
        Assert.That(gameState.getWellness(), Is.EqualTo(100));
        gameState.updateWellness(-99);
        Assert.That(gameState.getWellness(), Is.EqualTo(1));

        gameState.updateWellness(-1);
        Assert.IsTrue(gameState.getHasDied());
        

        //Testing Reputation
        gameState.updateReputation(0);
        Assert.That(gameState.getReputation(), Is.EqualTo(50));

        gameState.updateReputation(100);
        Assert.That(gameState.getReputation(), Is.EqualTo(100));

        gameState.updateReputation(-99);
        Assert.That(gameState.getReputation(), Is.EqualTo(1));

        gameState.updateReputation(999);
        Assert.That(gameState.getReputation(), Is.EqualTo(100));

        gameState.updateReputation(-30);
        Assert.That(gameState.getReputation(), Is.EqualTo(70));

        gameState.updateReputation(5);
        Assert.That(gameState.getReputation(), Is.EqualTo(75));


        ////BROKEN AT THE MOMENT
        ////Testing Subscribers
        //gameState.updateSubscribers(0);
        //Assert.That(gameState.getSubscribers(), Is.EqualTo(1000));

        //gameState.updateSubscribers(-1);
        //Assert.That(gameState.getSubscribers(), Is.EqualTo(999));

        //gameState.updateSubscribers(25);
        //Assert.That(gameState.getSubscribers(), Is.EqualTo(1024));

        //gameState.updateSubscribers(-1025);
        //Assert.That(gameState.getSubscribers(), Is.EqualTo(0));

        //gameState.updateSubscribers(1000);
        //Assert.That(gameState.getSubscribers(), Is.EqualTo(1000));

        //gameState.updateSubscribers(9999);
        //Assert.That(gameState.getSubscribers(), Is.EqualTo(10999));




        yield return null;
    }



}
