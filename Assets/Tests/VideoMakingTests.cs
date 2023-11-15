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

        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        videoMaking = player.GetComponent<video_making>();
        game_state gameState = player.GetComponent<game_state>();

        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(videoMaking);


        Assert.That(gameState.getReputation() == 20);
        for (int i = 1; i < 10; i++)
        {
            gameState.updateWellness(100);
            videoMaking.makeVideo(1);
            Assert.That(gameState.getReputation(), Is.EqualTo(20 + (i * 3)));
        }

        gameState.updateReputation(-27);

        for (int i = 1; i < 10; i++)
        {
            gameState.updateWellness(100);
            videoMaking.makeVideo(2);
            Assert.That(gameState.getReputation(), Is.EqualTo(20 + (i * 3)));
        }

        gameState.updateReputation(-27);
        for (int i = 1; i < 10; i++)
        {
            gameState.updateWellness(100);
            videoMaking.makeVideo(3);
            Assert.That(gameState.getReputation(), Is.EqualTo(20 + (i * 3)));
        }

        gameState.updateReputation(-27);
        for (int i = 1; i < 10; i++)
        {
            gameState.updateWellness(100);
            videoMaking.makeVideo(4);
            Assert.That(gameState.getReputation(), Is.EqualTo(20 + (i * 3)));
        }

        gameState.updateReputation(-27);
        for (int i = 1; i < 10; i++)
        {
            gameState.updateWellness(100);
            videoMaking.makeVideo(5);
            Assert.That(gameState.getReputation(), Is.EqualTo(20 + (i * 3)));
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator VideoMakingSubscriberMathTest()
    {

        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        videoMaking = player.GetComponent<video_making>();
        game_state gameState = player.GetComponent<game_state>();
        int subscribers = gameState.getSubscribers();

        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(videoMaking);



        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);
            int reputation = gameState.getReputation();
            videoMaking.makeVideo(1);
            int newSubscribers = gameState.getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 3) / 5)));
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 3) / 5)));
           subscribers = gameState.getSubscribers();
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);
            int reputation = gameState.getReputation();
            videoMaking.makeVideo(2);
            int newSubscribers = gameState.getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 6) / 5)));
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 6) / 5)));
            subscribers = gameState.getSubscribers();
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);
            int reputation = gameState.getReputation();
            videoMaking.makeVideo(3);
            int newSubscribers = gameState.getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 9) / 5)));
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 9) / 5)));
            subscribers = gameState.getSubscribers();
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);
            int reputation = gameState.getReputation();
            videoMaking.makeVideo(4);
            int newSubscribers = gameState.getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 12) / 5)));
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 12) / 5)));
            subscribers = gameState.getSubscribers();
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);
            int reputation = gameState.getReputation();
            videoMaking.makeVideo(5);
            int newSubscribers = gameState.getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 15) / 5)));
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 15) / 5)));
            subscribers = gameState.getSubscribers();
        }




        yield return null;
    }


    [UnityTest]
    public IEnumerator VideoMakingMoneyGainTest()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        videoMaking = player.GetComponent<video_making>();
        game_state gameState = player.GetComponent<game_state>();


        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(videoMaking);

        for(int i =0; i < 10;i++)
        {
            gameState.updateWellness(100);
            double origMoney = gameState.getMoney();
            videoMaking.makeVideo(1);
            int subscribers = gameState.getSubscribers();
            double money = gameState.getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers*0.02)) - 5+origMoney));
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20+origMoney));
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);
            double origMoney = gameState.getMoney();
            videoMaking.makeVideo(2);
            int subscribers = gameState.getSubscribers();
            double money = gameState.getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers * 0.02)) - 5 + origMoney));
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20 + origMoney));
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);
            double origMoney = gameState.getMoney();
            videoMaking.makeVideo(3);
            int subscribers = gameState.getSubscribers();
            double money = gameState.getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers * 0.02)) - 5 + origMoney));
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20 + origMoney));
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);
            double origMoney = gameState.getMoney();
            videoMaking.makeVideo(4);
            int subscribers = gameState.getSubscribers();
            double money = gameState.getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers * 0.02)) - 5 + origMoney));
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20 + origMoney));
        }

        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);
            double origMoney = gameState.getMoney();
            videoMaking.makeVideo(5);
            int subscribers = gameState.getSubscribers();
            double money = gameState.getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers * 0.02)) - 5 + origMoney));
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20 + origMoney));
        }

        yield return null;
    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.

    [UnityTest]
    public IEnumerator VideoMakingWellnessLossTest()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        videoMaking = player.GetComponent<video_making>();
        game_state gameState = player.GetComponent<game_state>();
        player.GetComponent<stalker_prototype_script>().enabled = false;
        gameState.testingVideoWellness = true;
        int wellness = gameState.getWellness();


        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(videoMaking);

        for(int i = 0; i< 10;i++)
        {
            gameState.updateWellness(100);

            videoMaking.makeVideo(1);
             wellness = gameState.getWellness();
            Assert.That(wellness, Is.EqualTo((100 - (3))));
        }
        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);
            videoMaking.makeVideo(2);
            
            wellness = gameState.getWellness();
            Debug.Log(wellness);
            Assert.That(wellness, Is.EqualTo(100 - (6)));
        }
        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);
            videoMaking.makeVideo(3);
            wellness = gameState.getWellness();
            Assert.That(wellness, Is.EqualTo(100 - (3 * 3)));
        }
        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);
            videoMaking.makeVideo(4);
            wellness = gameState.getWellness();
            Assert.That(wellness, Is.EqualTo(100 - (3 * 4)));
        }
        for (int i = 0; i < 10; i++)
        {
            gameState.updateWellness(100);
            videoMaking.makeVideo(5);
            wellness = gameState.getWellness();
            Assert.That(wellness, Is.EqualTo(100 - (3 * 5)));
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator VideoMakingTimeTest()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        videoMaking = player.GetComponent<video_making>();
        game_state gameState = player.GetComponent<game_state>();
        player.GetComponent<stalker_prototype_script>().enabled = false;
        gameState.testingVideoWellness = true;

        for (int i = 0; i < 3; i++)
        {
            gameState.resetDay();
            gameState.updateWellness(100);
            int origTime = gameState.getTime();
            videoMaking.makeVideo(1);
            int time = gameState.getTime();
            Assert.That(time, Is.EqualTo(origTime+60));
        }
        for (int i = 0; i < 3; i++)
        {
            gameState.resetDay();
            gameState.updateWellness(100);
            int origTime = gameState.getTime();
            videoMaking.makeVideo(2);
            int time = gameState.getTime();
            Assert.That(time, Is.EqualTo(origTime + (60*2)));
        }

        for (int i = 0; i < 3; i++)
        {
            gameState.resetDay();
            gameState.updateWellness(100);
            int origTime = gameState.getTime();
            videoMaking.makeVideo(3);
            int time = gameState.getTime();
            Assert.That(time, Is.EqualTo(origTime + (60*3)));
        }
        
        for (int i = 0; i < 3; i++)
        {
            gameState.resetDay();
            gameState.updateWellness(100);
            int origTime = gameState.getTime();
            videoMaking.makeVideo(4);
            int time = gameState.getTime();
            Assert.That(time, Is.EqualTo(origTime + (60*4)));
        }
        for (int i = 0; i < 3; i++)
        {
            gameState.resetDay();
            gameState.updateWellness(100);
            int origTime = gameState.getTime();
            videoMaking.makeVideo(5);
            int time = gameState.getTime();
            Assert.That(time, Is.EqualTo(origTime + (60*5)));
        }

        yield return null;
    }

}
