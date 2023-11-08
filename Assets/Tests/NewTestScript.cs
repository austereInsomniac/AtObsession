using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    private GameObject player;
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        player = GameObject.Find("Player");

        player.GetComponent<video_making>().makeVideo(1);
        Assert.That(player.GetComponent<game_state>().getReputation() == 103);
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
