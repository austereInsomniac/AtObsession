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
    public void setUp() //Loads the scene
    {
        SceneManager.LoadScene("Scenes/BaseScene");
    }

    [UnityTest]
    public IEnumerator GameStateInitializeTesting()
    {
        //Find player and assign game state script
        player = GameObject.FindGameObjectWithTag("MainCamera");
        gameState = player.GetComponent<game_state>();
        //Verify player and gamestate exsist
        Assert.IsNotNull(player);
        Assert.IsNotNull(gameState);
        //Verify that the location and canvas are set as Main Menu by the Awake statement
        Assert.AreSame(gameState.getLocation(), GameObject.Find("Main Menu"));
        Assert.AreSame(gameState.getLocationCanvas(), GameObject.Find("Main Menu Canvas"));


        //Testing base assignments in Awake() statement of game_state
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
        //Find player and assign game state script
        player = GameObject.FindGameObjectWithTag("MainCamera");
        gameState = player.GetComponent<game_state>();
        //Verify player and game state exsist
        Assert.IsNotNull(player);
        Assert.IsNotNull(gameState);


        //instialized verifying
        gameState.updateWellness(0);
        Assert.That(gameState.getWellness(), Is.EqualTo(70)); //From 70

        //Testing time
        gameState.updateTime(0);//Test no change
        Assert.That(gameState.getTime(), Is.EqualTo(480));//From 480
        //Testing Day Advance
        gameState.updateTime(960);//From 480(8) to 0(1) From day 1 to day 2
        //Assert.That(gameState.getDay(), Is.EqualTo(2));
        Assert.That(gameState.getTime(), Is.EqualTo(0));

        gameState.updateTime(240);//from 0(1) to 240(4)
        Assert.That(gameState.getTime(), Is.EqualTo(240)); 

        //Testing Force Sleep
        gameState.updateTime(1);//from 240)(4) to 241 to trigger force sleep after 240
        //Assert.That(gameState.getDay(), Is.EqualTo(2));//Day still 2
        Assert.That(gameState.getTime(), Is.EqualTo(480));//Time now 480(8)

        //Testing hunger


        //Assert.AreSame(gameState)

        //Testing Wellness
        //instialized verifying
        gameState.updateWellness(60);//From 10 because of time advancement
        Assert.That(gameState.getWellness(), Is.EqualTo(70));
        gameState.updateWellness(0);
        Assert.That(gameState.getWellness(), Is.EqualTo(70)); //From 70
        gameState.updateWellness(100);//Test Wellness cant go over 100
        Assert.That(gameState.getWellness(), Is.EqualTo(100));//From 70
        gameState.updateWellness(-30);
        Assert.That(gameState.getWellness(), Is.EqualTo(70));//From 100
        gameState.updateWellness(30);
        Assert.That(gameState.getWellness(), Is.EqualTo(100));//From 70
        gameState.updateWellness(-50);
        Assert.That(gameState.getWellness(), Is.EqualTo(50));//From 100
        gameState.updateWellness(999);
        Assert.That(gameState.getWellness(), Is.EqualTo(100));//From 50
        gameState.updateWellness(-99);
        Assert.That(gameState.getWellness(), Is.EqualTo(1));//From 100

        gameState.updateWellness(-1);//Test you die when wellness = 0
        Assert.IsTrue(gameState.getHasDied());//Verify the game state registered your death
        

        //Testing Reputation
        //Verify Initialized reputation
        gameState.updateReputation(0);
        Assert.That(gameState.getReputation(), Is.EqualTo(50));//From 50

        gameState.updateReputation(100);//Test rep cant go above 100
        Assert.That(gameState.getReputation(), Is.EqualTo(100));//From 50

        gameState.updateReputation(-99);
        Assert.That(gameState.getReputation(), Is.EqualTo(1));//From 100

        gameState.updateReputation(999);
        Assert.That(gameState.getReputation(), Is.EqualTo(100));//From 1

        gameState.updateReputation(-30);
        Assert.That(gameState.getReputation(), Is.EqualTo(70));//From 100

        gameState.updateReputation(5);
        Assert.That(gameState.getReputation(), Is.EqualTo(75));//From 70

        //Test rep "death"


        //Testing Subscribers
        //Verify Initialized subscribers
        gameState.updateSubscribers(0);
        Assert.That(gameState.getSubscribers(), Is.EqualTo(1000));//From 1000

        gameState.updateSubscribers(-1);
        Assert.That(gameState.getSubscribers(), Is.EqualTo(999));//From 1000

        gameState.updateSubscribers(25);
        Assert.That(gameState.getSubscribers(), Is.EqualTo(1024));//From 999

        gameState.updateSubscribers(-1025);//Test subscribers cant go below 0
        Assert.That(gameState.getSubscribers(), Is.EqualTo(0));//From 999

        gameState.updateSubscribers(1000);
        Assert.That(gameState.getSubscribers(), Is.EqualTo(1000));//From 0

        gameState.updateSubscribers(9999);
        Assert.That(gameState.getSubscribers(), Is.EqualTo(10999));//From 1000




        yield return null;
    }



}
