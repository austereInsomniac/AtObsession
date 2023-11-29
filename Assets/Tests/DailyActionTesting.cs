using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class DailyActionTesting
{

    public GameObject player;
    public GameObject computerCanvas;
    private daily_action_storage daily;
    public game_state gameState;
    // A Test behaves as an ordinary method

    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Scenes/BaseScene");

    }

    [UnityTest]
    public IEnumerator DailyActionTestingLivingRoom()
    {
        //Assign Scripts and objects that are important
        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        daily = player.GetComponent<daily_action_storage>();
        gameState = player.GetComponent<game_state>();
        //Verify that all are assigned properly
        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(daily);
        Assert.IsNotNull(gameState);


        //--------------Do chores------------------------------
        Assert.That(daily.getActionVariable("Do chores").getWellness(), Is.EqualTo(8));
        Assert.That(daily.getActionVariable("Do chores").getTime(), Is.EqualTo(15));
        Assert.That(daily.getActionVariable("Do chores").getMoney(), Is.EqualTo(0.00));
        Assert.That(daily.getActionVariable("Do chores").getGroup(), Is.EqualTo("chores"));
        Assert.That(daily.getActionVariable("Do chores").getText(), Is.EqualTo("You spent some time doing some chores around the house."));

        //--------------Go for a walk------------------------------
        Assert.That(daily.getActionVariable("Go for a walk").getWellness(), Is.EqualTo(10));
        Assert.That(daily.getActionVariable("Go for a walk").getTime(), Is.EqualTo(25));
        Assert.That(daily.getActionVariable("Go for a walk").getMoney(), Is.EqualTo(0));
        Assert.That(daily.getActionVariable("Go for a walk").getGroup(), Is.EqualTo("walk"));
        Assert.That(daily.getActionVariable("Go for a walk").getText(), Is.EqualTo("You went for a short walk at your local park."));

        //--------------Warm up------------------------------
        Assert.That(daily.getActionVariable("Warm up").getWellness(), Is.EqualTo(8));
        Assert.That(daily.getActionVariable("Warm up").getTime(), Is.EqualTo(30));
        Assert.That(daily.getActionVariable("Warm up").getMoney(), Is.EqualTo(0.00));
        Assert.That(daily.getActionVariable("Warm up").getGroup(), Is.EqualTo("exercise"));
        Assert.That(daily.getActionVariable("Warm up").getText(), Is.EqualTo("You decided to do a light warm up."));

        //--------------Light Workout------------------------------
        Assert.That(daily.getActionVariable("Light workout").getWellness(), Is.EqualTo(14));
        Assert.That(daily.getActionVariable("Light workout").getTime(), Is.EqualTo(75));
        Assert.That(daily.getActionVariable("Light workout").getMoney(), Is.EqualTo(0.00));
        Assert.That(daily.getActionVariable("Light workout").getGroup(), Is.EqualTo("exercise"));
        Assert.That(daily.getActionVariable("Light workout").getText(), Is.EqualTo("You chose to do a light workout."));

        //--------------Intense workout------------------------------
        Assert.That(daily.getActionVariable("Intense workout").getWellness(), Is.EqualTo(25));
        Assert.That(daily.getActionVariable("Intense workout").getTime(), Is.EqualTo(120));
        Assert.That(daily.getActionVariable("Intense workout").getMoney(), Is.EqualTo(0.00));
        Assert.That(daily.getActionVariable("Intense workout").getGroup(), Is.EqualTo("exercise"));
        Assert.That(daily.getActionVariable("Intense workout").getText(), Is.EqualTo("You committed to an intense workout."));

        //--------------Eat at a restaurant------------------------------
        Assert.That(daily.getActionVariable("Eat at a restaurant").getWellness(), Is.EqualTo(10));
        Assert.That(daily.getActionVariable("Eat at a restaurant").getTime(), Is.EqualTo(60));
        Assert.That(daily.getActionVariable("Eat at a restaurant").getMoney(), Is.EqualTo(-25));
        Assert.That(daily.getActionVariable("Eat at a restaurant").getGroup(), Is.EqualTo("food"));
        Assert.That(daily.getActionVariable("Eat at a restaurant").getText(), Is.EqualTo("You spent $25 to eat out."));


        yield return null;
    }


    [UnityTest]
    public IEnumerator DailyActionTestingOtherRooms()
    {
        //Assign Scripts and objects that are important
        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        daily = player.GetComponent<daily_action_storage>();
        gameState = player.GetComponent<game_state>();
        //Verify that all are assigned properly
        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(daily);
        Assert.IsNotNull(gameState);

        //--------------Cook food------------------------------
        Assert.That(daily.getActionVariable("Cook food").getWellness(), Is.EqualTo(10));
        Assert.That(daily.getActionVariable("Cook food").getTime(), Is.EqualTo(30));
        Assert.That(daily.getActionVariable("Cook food").getMoney(), Is.EqualTo(-5.00));
        Assert.That(daily.getActionVariable("Cook food").getGroup(), Is.EqualTo("food"));
        Assert.That(daily.getActionVariable("Cook food").getText(), Is.EqualTo("You spent $5 on groceries to cook food at home."));

        //--------------Eat a snack------------------------------
        Assert.That(daily.getActionVariable("Eat a snack").getWellness(), Is.EqualTo(10));
        Assert.That(daily.getActionVariable("Eat a snack").getTime(), Is.EqualTo(5));
        Assert.That(daily.getActionVariable("Eat a snack").getMoney(), Is.EqualTo(0.00));
        Assert.That(daily.getActionVariable("Eat a snack").getGroup(), Is.EqualTo("snack"));
        Assert.That(daily.getActionVariable("Eat a snack").getText(), Is.EqualTo("You ate a small snack."));

        //--------------Go to sleep------------------------------
        Assert.That(daily.getActionVariable("Go to sleep").getWellness(), Is.EqualTo(30));
        Assert.That(daily.getActionVariable("Go to sleep").getTime(), Is.EqualTo((32 * 60) - (gameState.getTime())));
        Assert.That(daily.getActionVariable("Go to sleep").getMoney(), Is.EqualTo(0.00));
        Assert.That(daily.getActionVariable("Go to sleep").getGroup(), Is.EqualTo("sleep"));
        Assert.That(daily.getActionVariable("Go to sleep").getText(), Is.EqualTo("You had a night of restful sleep."));

        //--------------Take a nap------------------------------
        Assert.That(daily.getActionVariable("Take a nap").getWellness(), Is.EqualTo(20));
        Assert.That(daily.getActionVariable("Take a nap").getTime(), Is.EqualTo(120));
        Assert.That(daily.getActionVariable("Take a nap").getMoney(), Is.EqualTo(0.00));
        Assert.That(daily.getActionVariable("Take a nap").getGroup(), Is.EqualTo("nap"));
        Assert.That(daily.getActionVariable("Take a nap").getText(), Is.EqualTo("You took a short power nap."));

        //--------------Freshen up------------------------------
        Assert.That(daily.getActionVariable("Freshen up").getWellness(), Is.EqualTo(3));
        Assert.That(daily.getActionVariable("Freshen up").getTime(), Is.EqualTo(5));
        Assert.That(daily.getActionVariable("Freshen up").getMoney(), Is.EqualTo(0.00));
        Assert.That(daily.getActionVariable("Freshen up").getGroup(), Is.EqualTo("Freshen up"));
        Assert.That(daily.getActionVariable("Freshen up").getText(), Is.EqualTo("You quickly freshened up."));

        //--------------Shower------------------------------
        Assert.That(daily.getActionVariable("Shower").getWellness(), Is.EqualTo(8));
        Assert.That(daily.getActionVariable("Shower").getTime(), Is.EqualTo(20));
        Assert.That(daily.getActionVariable("Shower").getMoney(), Is.EqualTo(0.00));
        Assert.That(daily.getActionVariable("Shower").getGroup(), Is.EqualTo("shower"));
        Assert.That(daily.getActionVariable("Shower").getText(), Is.EqualTo("You took a quick shower."));

        //--------------Bubble bath------------------------------
        Assert.That(daily.getActionVariable("Bubble bath").getWellness(), Is.EqualTo(12));
        Assert.That(daily.getActionVariable("Bubble bath").getTime(), Is.EqualTo(45));
        Assert.That(daily.getActionVariable("Bubble bath").getMoney(), Is.EqualTo(0.00));
        Assert.That(daily.getActionVariable("Bubble bath").getGroup(), Is.EqualTo("bath"));
        Assert.That(daily.getActionVariable("Bubble bath").getText(), Is.EqualTo("You took a long relaxing bubble bath."));


        yield return null;
    }
}
