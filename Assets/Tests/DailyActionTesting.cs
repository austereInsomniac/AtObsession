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


        ////--------------Do chores------------------------------
        //Assert.That(daily.getActionVariable("Do chores").getWellness(), Is.EqualTo(8));
        //Assert.That(daily.getActionVariable("Do chores").getTime(), Is.EqualTo(15));
        //Assert.That(daily.getActionVariable("Do chores").getMoney(), Is.EqualTo(0.00));
        //Assert.That(daily.getActionVariable("Do chores").getGroup(), Is.EqualTo("chores"));
        //Assert.That(daily.getActionVariable("Do chores").getText(), Is.EqualTo("You spent some time doing some chores around the house."));

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

    [UnityTest]
    public IEnumerator DailyActionTestingRandomized()
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

        //-15 low and 15 high for wellness
        //Random time big is between (60/5)*5 and (120/5) *5
        //Random time small is between (30/5)*5 and (60/5)*5

        //--------------Got to the gym------------------------------
        Assert.That(daily.getActionVariable("Go to the gym").getWellness(), Is.EqualTo(8));
        Assert.That(daily.getActionVariable("Go to the gym").getTime(), Is.AtLeast((60 / 5) * 5));
        Assert.That(daily.getActionVariable("Go to the gym").getTime(), Is.AtMost((120 / 5) * 5));
        Assert.That(daily.getActionVariable("Go to the gym").getMoney(), Is.EqualTo(-15.00));
        Assert.That(daily.getActionVariable("Go to the gym").getGroup(), Is.EqualTo("exercise"));
        Assert.That(daily.getActionVariable("Go to the gym").getText(), Is.EqualTo("You spent $15 to work out at your local gym."));
        //--------------Visit Friends------------------------------
        Assert.That(daily.getActionVariable("Visit friends").getWellness(), Is.AtLeast(-15));
        Assert.That(daily.getActionVariable("Visit friends").getWellness(), Is.AtMost(15));
        Assert.That(daily.getActionVariable("Visit friends").getTime(), Is.AtLeast((60 / 5) * 5));
        Assert.That(daily.getActionVariable("Visit friends").getTime(), Is.AtMost((120 / 5) * 5));
        Assert.That(daily.getActionVariable("Visit friends").getMoney(), Is.EqualTo(0.00));
        Assert.That(daily.getActionVariable("Visit friends").getGroup(), Is.EqualTo("friends"));
        Assert.That(daily.getActionVariable("Visit friends").getText(), Is.EqualTo(" You went out and spent some time with your friend."));
        //--------------Watch Tv------------------------------
        Assert.That(daily.getActionVariable("Watch TV").getWellness(), Is.EqualTo(8));
        Assert.That(daily.getActionVariable("Watch TV").getTime(), Is.AtLeast((30 / 5) * 5));
        Assert.That(daily.getActionVariable("Watch TV").getTime(), Is.AtMost((60 / 5) * 5));
        Assert.That(daily.getActionVariable("Watch TV").getMoney(), Is.EqualTo(0.00));
        Assert.That(daily.getActionVariable("Watch TV").getGroup(), Is.EqualTo("entertainment"));
        Assert.That(daily.getActionVariable("Watch TV").getText(), Is.EqualTo("You’ve watched an episode of your favorite show.\r\n"));





        yield return null;
    }


    [UnityTest]
    public IEnumerator doActionTest()
    {
        //Assign Scripts and objects that are important

        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        daily = player.GetComponent<daily_action_storage>();
        gameState = player.GetComponent<game_state>();
        gameState.testingVideoWellness = true;
        //Verify that all are assigned properly
        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(daily);
        Assert.IsNotNull(gameState);
        int time = gameState.getTime();
        //daily.doAction("Do chores");
        //Assert.That(gameState.getWellness(), Is.EqualTo(78));//From 70
        //Assert.That(gameState.getTime(), Is.EqualTo(855));//From 840
        //Assert.That(gameState.getMoney(), Is.EqualTo(100));//From 100

        daily.doAction("Go for a walk");
        Assert.That(gameState.getWellness(), Is.EqualTo(80));//From 70
        Assert.That(gameState.getTime(), Is.EqualTo(865));//From 840
        Assert.That(gameState.getMoney(), Is.EqualTo(100));//From 100

        daily.doAction("Warm up");
        Assert.That(gameState.getWellness(), Is.EqualTo(88));//From 80
        Assert.That(gameState.getTime(), Is.EqualTo(895));//From 865
        Assert.That(gameState.getMoney(), Is.EqualTo(100));//From 100

        gameState.updateWellness(-18);//to 70

        daily.doAction("Eat at a restaurant");
        Assert.That(gameState.getWellness(), Is.EqualTo(80));//From 70
        Assert.That(gameState.getTime(), Is.EqualTo(955));//From 895
        Assert.That(gameState.getMoney(), Is.EqualTo(75));//From 100

        daily.doAction("Cook food");
        Assert.That(gameState.getWellness(), Is.EqualTo(90));//From 80
        Assert.That(gameState.getTime(), Is.EqualTo(985));//From 955
        Assert.That(gameState.getMoney(), Is.EqualTo(70));//From 75

        gameState.updateWellness(-20);//to 70

        daily.doAction("Eat a snack");
        Assert.That(gameState.getWellness(), Is.EqualTo(80));//From 70
        Assert.That(gameState.getTime(), Is.EqualTo(990));//From 985
        Assert.That(gameState.getMoney(), Is.EqualTo(70));//From 70

        //gameState.updateWellness(-30);//to 50
        //daily.doAction("Go to sleep");
        //Assert.That(gameState.getWellness(), Is.EqualTo(70));//From 50
        //Assert.That(gameState.getTime(), Is.EqualTo(32*60 - time));//From 990 to 930 
        //Assert.That(gameState.getMoney(), Is.EqualTo(70));//From 100

        gameState.updateWellness(-10);//to 70

        daily.doAction("Take a nap");
        Assert.That(gameState.getWellness(), Is.EqualTo(90));//From 70
        Assert.That(gameState.getTime(), Is.EqualTo(1110));//From 990
        Assert.That(gameState.getMoney(), Is.EqualTo(70));//From 70

        gameState.updateWellness(-20);//to 70
        daily.doAction("Freshen up");
        Assert.That(gameState.getWellness(), Is.EqualTo(73));//From 70
        Assert.That(gameState.getTime(), Is.EqualTo(1115));//From 1110
        Assert.That(gameState.getMoney(), Is.EqualTo(70));//From 70


        daily.doAction("Shower");
        Assert.That(gameState.getWellness(), Is.EqualTo(81));//From 73
        Assert.That(gameState.getTime(), Is.EqualTo(1135));//From 1115
        Assert.That(gameState.getMoney(), Is.EqualTo(70));//From 70

        gameState.updateWellness(-11);//to 70
        gameState.updateTime(1440);
        daily.doAction("Bubble bath");
        Assert.That(gameState.getWellness(), Is.EqualTo(82));//From 70
        Assert.That(gameState.getTime(), Is.EqualTo(1175));//From 1135
        Assert.That(gameState.getMoney(), Is.EqualTo(70));//From 70
        //daily.doAction("Light workout");
        //Assert.That(gameState.getWellness(), Is.EqualTo(84));//From 70
        //Assert.That(gameState.getTime(), Is.EqualTo(970));//From 910
        //Assert.That(gameState.getMoney(), Is.EqualTo(100));//From 100


        //daily.doAction("Intense workout");
        //Assert.That(gameState.getWellness(), Is.EqualTo(121));//From 110
        //Assert.That(gameState.getTime(), Is.EqualTo(1110));//From 985
        //Assert.That(gameState.getMoney(), Is.EqualTo(100));//From 100
        yield return null;
    }
}
