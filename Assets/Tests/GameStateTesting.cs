using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
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
}
