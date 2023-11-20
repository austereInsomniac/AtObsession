using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class ShopTesting
{

    public GameObject player;
    public game_state gameState;
    public shop shops;

    [SetUp]
    public void setUp()
    {
        SceneManager.LoadScene("Scenes/BaseScene");
    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShopSystemTesting()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        gameState = player.GetComponent<game_state>();
        shops = player.GetComponent<shop>();
        Assert.IsNotNull(player);
        Assert.IsNotNull(gameState);
        Assert.IsNotNull(shops);

        //Testing First item updates game state correctly
        shops.buyItem(0);
        //Verify every value is correct
        Assert.That(shops.getItem(0).getWellness(), Is.EqualTo(10));
        Assert.That(shops.getItem(0).getReputation(), Is.EqualTo(5));
        Assert.That(shops.getItem(0).getMoney(), Is.EqualTo(-50));
        Assert.That(shops.getItem(0).getDescription(), Is.EqualTo("Increase the sound quality of your videos"));
        Assert.That(shops.getItem(0).getName, Is.EqualTo("Microphone"));

        //Verify every value is changed correctly
        Assert.That(gameState.getWellness(), Is.EqualTo(80));//From 70
        Assert.That(gameState.getReputation(), Is.EqualTo(55));//From 50
        Assert.That(gameState.getMoney(), Is.EqualTo(50));//From 100

        //Testing that a purchase can not be made without enough money
        shops.buyItem(1);
        Assert.That(shops.getItem(1).getWellness(), Is.EqualTo(25));
        Assert.That(shops.getItem(1).getReputation(), Is.EqualTo(10));
        Assert.That(shops.getItem(1).getMoney(), Is.EqualTo(-100));
        Assert.That(shops.getItem(1).getDescription(), Is.EqualTo("Increase the preformance of your pc to record better gameplay"));
        Assert.That(shops.getItem(1).getName, Is.EqualTo("PC Upgrade"));

        Assert.That(gameState.getWellness(), Is.EqualTo(80));
        Assert.That(gameState.getReputation(), Is.EqualTo(55));
        Assert.That(gameState.getMoney(), Is.EqualTo(50));

        gameState.updateMoney(50); // set to 100
        gameState.updateWellness(-50);//Set to 30
        //testing item 1 with enough money
        shops.buyItem(1);
        Assert.That(gameState.getWellness(), Is.EqualTo(55)); //From 30
        Assert.That(gameState.getReputation(), Is.EqualTo(65)); //From 55
        Assert.That(gameState.getMoney(), Is.EqualTo(0));//From 100

        gameState.updateMoney(300);//Set to 300
        //Testing item 2
        shops.buyItem(2);
        Assert.That(shops.getItem(2).getWellness(), Is.EqualTo(15));
        Assert.That(shops.getItem(2).getReputation(), Is.EqualTo(7));
        Assert.That(shops.getItem(2).getMoney(), Is.EqualTo(-75));
        Assert.That(shops.getItem(2).getDescription(), Is.EqualTo("Upgrade your monitor to ive you more space to work on"));
        Assert.That(shops.getItem(2).getName, Is.EqualTo("Monitor"));

        Assert.That(gameState.getWellness(), Is.EqualTo(70));//From 55
        Assert.That(gameState.getReputation(), Is.EqualTo(72)); //From 65
        Assert.That(gameState.getMoney(), Is.EqualTo(225)); //From 300

        //Testing item 3
        shops.buyItem(3);
        Assert.That(shops.getItem(3).getWellness(), Is.EqualTo(20));
        Assert.That(shops.getItem(3).getReputation(), Is.EqualTo(5));
        Assert.That(shops.getItem(3).getMoney(), Is.EqualTo(-80));
        Assert.That(shops.getItem(3).getDescription(), Is.EqualTo("Buy a new chair to make working mor comfortable"));
        Assert.That(shops.getItem(3).getName, Is.EqualTo("Gamer Chair"));

        Assert.That(gameState.getWellness(), Is.EqualTo(90));//From 70
        Assert.That(gameState.getReputation(), Is.EqualTo(77));//From 72
        Assert.That(gameState.getMoney(), Is.EqualTo(145));//From 225


        gameState.updateWellness(-40); //Set to 50
        //Testing item 4
        shops.buyItem(4);
        Assert.That(shops.getItem(4).getWellness(), Is.EqualTo(10));
        Assert.That(shops.getItem(4).getReputation(), Is.EqualTo(5));
        Assert.That(shops.getItem(4).getMoney(), Is.EqualTo(-25));
        Assert.That(shops.getItem(4).getDescription(), Is.EqualTo("Upgrade the sound quality of the games you play"));
        Assert.That(shops.getItem(4).getName, Is.EqualTo("Headphones"));

        Assert.That(gameState.getWellness(), Is.EqualTo(60));//From 50
        Assert.That(gameState.getReputation(), Is.EqualTo(82)); //From 77
        Assert.That(gameState.getMoney(), Is.EqualTo(120));//From 145

        //Testing item 5
        shops.buyItem(5);
        Assert.That(shops.getItem(5).getWellness(), Is.EqualTo(10));
        Assert.That(shops.getItem(5).getReputation(), Is.EqualTo(5));
        Assert.That(shops.getItem(5).getMoney(), Is.EqualTo(-30));
        Assert.That(shops.getItem(5).getDescription(), Is.EqualTo("Update your wardrobe to impress your followers"));
        Assert.That(shops.getItem(5).getName, Is.EqualTo("New Outfit"));

        Assert.That(gameState.getWellness(), Is.EqualTo(70));//From 60
        Assert.That(gameState.getReputation(), Is.EqualTo(87));//From 82
        Assert.That(gameState.getMoney(), Is.EqualTo(90));//From 120

        //Testing item 6
        shops.buyItem(6);
        Assert.That(shops.getItem(6).getWellness(), Is.EqualTo(10));
        Assert.That(shops.getItem(6).getReputation(), Is.EqualTo(5));
        Assert.That(shops.getItem(6).getMoney(), Is.EqualTo(-40));
        Assert.That(shops.getItem(6).getDescription(), Is.EqualTo("Buy a new game to play on stream"));
        Assert.That(shops.getItem(6).getName, Is.EqualTo("New Game"));

        Assert.That(gameState.getWellness(), Is.EqualTo(80));//From 70
        Assert.That(gameState.getReputation(), Is.EqualTo(92));//From 87
        Assert.That(gameState.getMoney(), Is.EqualTo(50));//From 90


        //Want a way to make sure everything is purchased


        yield return null;
    }
}
