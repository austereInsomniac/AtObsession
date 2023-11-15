using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class NewTestScript
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

        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(videoMaking);


        Assert.That(player.GetComponent<game_state>().getReputation() == 20);
        for (int i = 1; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            videoMaking.makeVideo(1);
            Assert.That(player.GetComponent<game_state>().getReputation(), Is.EqualTo(20 + (i * 3)));
        }

        player.GetComponent<game_state>().updateReputation(-27);

        for (int i = 1; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            videoMaking.makeVideo(2);
            Assert.That(player.GetComponent<game_state>().getReputation(), Is.EqualTo(20 + (i * 3)));
        }

        player.GetComponent<game_state>().updateReputation(-27);
        for (int i = 1; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            videoMaking.makeVideo(3);
            Assert.That(player.GetComponent<game_state>().getReputation(), Is.EqualTo(20 + (i * 3)));
        }

        player.GetComponent<game_state>().updateReputation(-27);
        for (int i = 1; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            videoMaking.makeVideo(4);
            Assert.That(player.GetComponent<game_state>().getReputation(), Is.EqualTo(20 + (i * 3)));
        }

        player.GetComponent<game_state>().updateReputation(-27);
        for (int i = 1; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            videoMaking.makeVideo(5);
            Assert.That(player.GetComponent<game_state>().getReputation(), Is.EqualTo(20 + (i * 3)));
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator VideoMakingSubscriberMathTest()
    {

        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        videoMaking = player.GetComponent<video_making>();
        int subscribers = player.GetComponent<game_state>().getSubscribers();

        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(videoMaking);



        for (int i = 0; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            int reputation = player.GetComponent<game_state>().getReputation();
            videoMaking.makeVideo(1);
            int newSubscribers = player.GetComponent<game_state>().getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 3) / 5)));
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 3) / 5)));
           subscribers = player.GetComponent<game_state>().getSubscribers();
        }

        for (int i = 0; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            int reputation = player.GetComponent<game_state>().getReputation();
            videoMaking.makeVideo(2);
            int newSubscribers = player.GetComponent<game_state>().getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 6) / 5)));
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 6) / 5)));
            subscribers = player.GetComponent<game_state>().getSubscribers();
        }

        for (int i = 0; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            int reputation = player.GetComponent<game_state>().getReputation();
            videoMaking.makeVideo(3);
            int newSubscribers = player.GetComponent<game_state>().getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 9) / 5)));
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 9) / 5)));
            subscribers = player.GetComponent<game_state>().getSubscribers();
        }

        for (int i = 0; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            int reputation = player.GetComponent<game_state>().getReputation();
            videoMaking.makeVideo(4);
            int newSubscribers = player.GetComponent<game_state>().getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 12) / 5)));
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 12) / 5)));
            subscribers = player.GetComponent<game_state>().getSubscribers();
        }

        for (int i = 0; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            int reputation = player.GetComponent<game_state>().getReputation();
            videoMaking.makeVideo(5);
            int newSubscribers = player.GetComponent<game_state>().getSubscribers();
            Assert.That(newSubscribers, Is.AtLeast(subscribers + ((reputation + 15) / 5)));
            Assert.That(newSubscribers, Is.AtMost(subscribers + ((reputation + 10 + 15) / 5)));
            subscribers = player.GetComponent<game_state>().getSubscribers();
        }




        yield return null;
    }


    [UnityTest]
    public IEnumerator VideoMakingMoneyGainTest()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        computerCanvas = GameObject.Find("Computer Canvas");
        videoMaking = player.GetComponent<video_making>();

        Assert.IsNotNull(player);
        Assert.IsNotNull(computerCanvas);
        Assert.IsNotNull(videoMaking);

        for(int i =0; i < 10;i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            double origMoney = player.GetComponent<game_state>().getMoney();
            videoMaking.makeVideo(1);
            int subscribers = player.GetComponent<game_state>().getSubscribers();
            double money = player.GetComponent<game_state>().getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers*0.02)) - 5+origMoney));
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20+origMoney));
        }

        for (int i = 0; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            double origMoney = player.GetComponent<game_state>().getMoney();
            videoMaking.makeVideo(2);
            int subscribers = player.GetComponent<game_state>().getSubscribers();
            double money = player.GetComponent<game_state>().getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers * 0.02)) - 5 + origMoney));
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20 + origMoney));
        }

        for (int i = 0; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            double origMoney = player.GetComponent<game_state>().getMoney();
            videoMaking.makeVideo(3);
            int subscribers = player.GetComponent<game_state>().getSubscribers();
            double money = player.GetComponent<game_state>().getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers * 0.02)) - 5 + origMoney));
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20 + origMoney));
        }

        for (int i = 0; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            double origMoney = player.GetComponent<game_state>().getMoney();
            videoMaking.makeVideo(4);
            int subscribers = player.GetComponent<game_state>().getSubscribers();
            double money = player.GetComponent<game_state>().getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers * 0.02)) - 5 + origMoney));
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20 + origMoney));
        }

        for (int i = 0; i < 10; i++)
        {
            player.GetComponent<game_state>().updateWellness(100);
            double origMoney = player.GetComponent<game_state>().getMoney();
            videoMaking.makeVideo(5);
            int subscribers = player.GetComponent<game_state>().getSubscribers();
            double money = player.GetComponent<game_state>().getMoney();
            Assert.That(money, Is.AtLeast(((int)(subscribers * 0.02)) - 5 + origMoney));
            Assert.That(money, Is.AtMost(((int)(subscribers * 0.02)) + 20 + origMoney));
        }

        yield return null;
    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.

}
