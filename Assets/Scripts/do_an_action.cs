using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class do_an_action : MonoBehaviour
{
    // increase in each statistic
    [SerializeField]
    private int changeWellness;
    [SerializeField]
    private int changeTime;
    [SerializeField]
    private int changeRep;
    [SerializeField]
    private int changeSubs;
    [SerializeField]
    private int changeEnd;
    [SerializeField]
    private double changeMoney;

    private game_state player;

    void Awake()
    {
        // "Player" is the name of the Game Object with the game_state script
        player = GameObject.Find("Player").GetComponent<game_state>();
    }
     
    public void doAnAction()
    {
        // update each statistic
        player.updateWellness(changeWellness);
        player.updateTime(changeTime);
        player.updateReputation(changeRep);
        player.updateSubscribers(changeSubs);
        player.updateMoney(changeMoney);
        player.updateEnding(changeEnd);
    }
}
