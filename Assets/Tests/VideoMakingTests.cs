using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class VideoMakingTests
{

    public GameObject player;
    public GameObject computerCanvas;
    private video_making videoMaking;
    // A Test behaves as an ordinary method

    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Scenes/BaseScene");

    }

    [UnityTest]
    public IEnumerator VideoMakingTestReputation()
    {
        //Assign Scripts and objects that are important
        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        videoMaking = player.GetComponent<video_making>();
        game_state gameState = player.GetComponent<game_state>();
        //Verify that all are assigned properly
        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(videoMaking);
        Assert.IsNotNull(gameState);


        Assert.That(gameState.getReputation() == 50);
        gameState.updateReputation(-25);//Set Rep to 25
        for (int i = 1; i < 6; i++)
        {
            gameState.updateWellness(100);//Keep wellness at 100 to prevent death (1*)
            videoMaking.makeVideo(1);//Make a 1 star video
            Assert.That(gameState.getReputation(), Is.EqualTo(25 + (i* 1 * 3))); //Test that the rep was updated to 25 + (amount of loops)*(star)*(3)
        }

        gameState.updateReputation(-15);

        for (int i = 1; i < 6; i++)
        {
            gameState.updateWellness(100);//1*
            videoMaking.makeVideo(2);
            Assert.That(gameState.getReputation(), Is.EqualTo(25 + (i* 2 * 3))); //Test that the rep was updated to 25 + (amount of loops)*(star)*(3)
        }

        gameState.updateReputation(-30);
        for (int i = 1; i < 6; i++)
        {
            gameState.updateWellness(100);//1*
            videoMaking.makeVideo(3);
            Assert.That(gameState.getReputation(), Is.EqualTo(25 + (i* 3 * 3)));//Test that the rep was updated to 25 + (amount of loops)*(star)*(3)
        }

        gameState.updateReputation(-45);
        for (int i = 1; i < 6; i++)
        {
            gameState.updateWellness(100);//1*
            videoMaking.makeVideo(4);
            Assert.That(gameState.getReputation(), Is.EqualTo(25 + (i * 4 * 3)));//Test that the rep was updated to 25 + (amount of loops)*(star)*(3)
        }

        gameState.updateReputation(-60);
        for (int i = 1; i < 6; i++)
        {
            gameState.updateWellness(100);//1*
            videoMaking.makeVideo(5);

            Assert.That(gameState.getReputation(), Is.EqualTo(25 + (i * 5 * 3)));//Test that the rep was updated to 25 + (amount of loops)*(star)*(3)
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator VideoMakingSubscriberMathTest()
    {
        //Assign Scripts and objects that are important
        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        videoMaking = player.GetComponent<video_making>();
        game_state gameState = player.GetComponent<game_state>();
        int subscribers = gameState.getSubscribers();
        //Verify that all are assigned properly
        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(videoMaking);



        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);//1*
            int reputation = gameState.getReputation();

            videoMaking.makeVideo(1);

            int newSubscribers = gameState.getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 3) / 5)));//Test that the subs was updated to at least (previous subscriber count) + ((reputation+(star*3))/5)
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 3) / 5)));//or at most (previous subs) +(((repuation+10+(star*3))/5)
           subscribers = gameState.getSubscribers();//Assign new prev subs
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);//1*
            int reputation = gameState.getReputation();

            videoMaking.makeVideo(2);

            int newSubscribers = gameState.getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 6) / 5))); //Same formula 
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 6) / 5)));//Same formula 
            subscribers = gameState.getSubscribers();//Assign new prev subs
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);//1*
            int reputation = gameState.getReputation();

            videoMaking.makeVideo(3);

            int newSubscribers = gameState.getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 9) / 5)));//Same formula 
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 9) / 5)));//Same formula 
            subscribers = gameState.getSubscribers();//Assign new prev subs
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);//1*
            int reputation = gameState.getReputation();

            videoMaking.makeVideo(4);

            int newSubscribers = gameState.getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 12) / 5)));//Same formula 
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 12) / 5)));//Same formula 
            subscribers = gameState.getSubscribers();//Assign new prev subs
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);//1*
            int reputation = gameState.getReputation();

            videoMaking.makeVideo(5);

            int newSubscribers = gameState.getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 15) / 5)));//Same formula 
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 15) / 5)));//Same formula 
            subscribers = gameState.getSubscribers();//Assign new prev subs
        }




        yield return null;
    }


    [UnityTest]
    public IEnumerator VideoMakingMoneyGainTest()
    {
        //Assign Scripts and objects that are important
        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        videoMaking = player.GetComponent<video_making>();
        game_state gameState = player.GetComponent<game_state>();
        //Verify that all are assigned properly
        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(videoMaking);

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);//1*
            double origMoney = gameState.getMoney(); //Assign previous money count

            videoMaking.makeVideo(1);

            int subscribers = gameState.getSubscribers();
            double money = gameState.getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers * 0.02)) - 5 + origMoney));//At least (new subscribers total*0.02) - (5 + prevous money total)
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20 + origMoney));//At most (new subscribers total*0.02) -(20+ previous money total)
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);//1*
            double origMoney = gameState.getMoney();//Assign previous money count

            videoMaking.makeVideo(2);

            int subscribers = gameState.getSubscribers();
            double money = gameState.getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers * 0.02)) - 5 + origMoney));//Same Formula
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20 + origMoney));//Same Formula
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);//1*
            double origMoney = gameState.getMoney();//Assign previous money count

            videoMaking.makeVideo(3);

            int subscribers = gameState.getSubscribers();
            double money = gameState.getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers * 0.02)) - 5 + origMoney));//Same Formula
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20 + origMoney));//Same Formula
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);//1*
            double origMoney = gameState.getMoney();//Assign previous money count

            videoMaking.makeVideo(4);

            int subscribers = gameState.getSubscribers();
            double money = gameState.getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers * 0.02)) - 5 + origMoney));//Same Formula
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20 + origMoney));//Same Formula
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.testingVideoWellness = true;
            gameState.updateWellness(100);//1*
            double origMoney = gameState.getMoney();//Assign previous money count
            int subscribers = gameState.getSubscribers();
            videoMaking.makeVideo(5);

            subscribers = gameState.getSubscribers();

            double money = gameState.getMoney();

            Assert.That(money, Is.AtLeast(((int)(subscribers * 0.02)) - 5 + origMoney));//Same Formula
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20 + origMoney));//Same Formula
        }

        yield return null;
    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.

    [UnityTest]
    public IEnumerator VideoMakingWellnessLossTest()
    {
        //Assign Scripts and objects that are important
        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        videoMaking = player.GetComponent<video_making>();
        game_state gameState = player.GetComponent<game_state>();
        player.GetComponent<stalker_prototype_script>().enabled = false;
        gameState.testingVideoWellness = true;
        int wellness = gameState.getWellness();

        //Verify that all are assigned properly
        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(videoMaking);

        for(int i = 0; i< 10;i++)
        {
            gameState.updateWellness(100);//1*

            videoMaking.makeVideo(1);
             wellness = gameState.getWellness();
            Assert.That(wellness, Is.EqualTo((100 - (3)))); // Original Wellness - (3*star)
        }
        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);//1*
            videoMaking.makeVideo(2);
            
            wellness = gameState.getWellness();
            Assert.That(wellness, Is.EqualTo(100 - (6)));// Original Wellness - (3*star)
        }
        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);//1*
            videoMaking.makeVideo(3);

            wellness = gameState.getWellness();
            Assert.That(wellness, Is.EqualTo(100 - (3 * 3)));// Original Wellness - (3*star)
        }
        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);//1*
            videoMaking.makeVideo(4);

            wellness = gameState.getWellness();
            Assert.That(wellness, Is.EqualTo(100 - (3 * 4)));// Original Wellness - (3*star)
        }
        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);//1*
            videoMaking.makeVideo(5);

            wellness = gameState.getWellness();
            Assert.That(wellness, Is.EqualTo(100 - (3 * 5)));// Original Wellness - (3*star)
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator VideoMakingTimeTest()
    {
        //Assign Scripts and objects that are important
        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        videoMaking = player.GetComponent<video_making>();
        game_state gameState = player.GetComponent<game_state>();
        player.GetComponent<stalker_prototype_script>().enabled = false; //Disable stalker
        gameState.testingVideoWellness = true; //disable force sleep


        //Verify that all are assigned properly
        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(videoMaking);

        for (int i = 0; i < 3; i++)
        {
            gameState.resetDay();//Sets day to base stats everyloop
            gameState.updateWellness(100);//1*
            int origTime = gameState.getTime();//Get previous time

            videoMaking.makeVideo(1);

            int time = gameState.getTime();//get new current time
            Assert.That(time, Is.EqualTo(origTime+60)); //Original time + (star)*(60)
        }

        for (int i = 0; i < 3; i++)
        {
            gameState.resetDay();//Sets day to base stats everyloop
            gameState.updateWellness(100);//1*
            int origTime = gameState.getTime();//Get previous time

            videoMaking.makeVideo(2);

            int time = gameState.getTime();//get new current time
            Assert.That(time, Is.EqualTo(origTime + (60*2)));//Original time + (star)*(60)
        }

        for (int i = 0; i < 3; i++)
        {
            gameState.resetDay(); //Sets day to base stats everyloop
            gameState.updateWellness(100);//1*
            int origTime = gameState.getTime();//Get previous time

            videoMaking.makeVideo(3);

            int time = gameState.getTime();//get new current time
            Assert.That(time, Is.EqualTo(origTime + (60*3)));//Original time + (star)*(60)
        }
        
        for (int i = 0; i < 3; i++)
        {
            gameState.resetDay();//Sets day to base stats everyloop
            gameState.updateWellness(100);//1*
            int origTime = gameState.getTime();//Get previous time

            videoMaking.makeVideo(4);

            int time = gameState.getTime();//get new current time
            Assert.That(time, Is.EqualTo(origTime + (60*4)));//Original time + (star)*(60)
        }

        for (int i = 0; i < 3; i++)
        {
            gameState.resetDay();//Sets day to base stats everyloop
            gameState.updateWellness(100);//1*
            int origTime = gameState.getTime();//Get previous time

            videoMaking.makeVideo(5);

            int time = gameState.getTime();//get new current time
            Assert.That(time, Is.EqualTo(origTime + (60*5)));//Original time + (star)*(60)
        }

        yield return null;
    }

}
