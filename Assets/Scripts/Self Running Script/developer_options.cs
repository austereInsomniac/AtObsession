using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class developer_options : MonoBehaviour
{

    //Stalker Options
    [SerializeField]
    private bool stalkerOff;
    [SerializeField]
    private int stalkerNum;


    //Stat manipulating
    [SerializeField]
    private bool infiniteMoney;
    [SerializeField]
    private bool infiniteSubscribers;
    [SerializeField]
    private bool infiniteReputation;

    //Wellness Setting
    [SerializeField]
    private bool infiniteWellness;
    [SerializeField]
    private bool noWellness;

    //Day and Time options
    [SerializeField]
    private int dayStart;
    [SerializeField]
    private int dayLimit; //Dont know how to implement yet

    GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }


    // Start is called before the first frame update
    void Start()
    {
        if (stalkerOff)
        {
            player.GetComponent<stalker_prototype_script>().enabled = false;
        }

        player.GetComponent<stalker_prototype_script>().setEventNum(stalkerNum);

        if (infiniteMoney)
        {
            player.GetComponent<game_state>().updateMoney(10000);
        }

        if (infiniteSubscribers) { 
            player.GetComponent<game_state>().updateSubscribers(10000);
        }

        if (infiniteReputation)
        {
            player.GetComponent<game_state>().updateReputation(10000);
        }

        if(dayStart != 0)
        {
            for (int i = 0; i < dayStart-1; i++){
                player.GetComponent<game_state>().updateTime(1440);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (infiniteWellness)
        {
            player.GetComponent<game_state>().updateWellness(100);
        }
        else if(noWellness)
        {
            player.GetComponent<game_state>().updateWellness(-100);
        }
    }
}
