using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class developer_options : MonoBehaviour
{
    [SerializeField]
    private bool stalkerOff;
    [SerializeField]
    private int stalkerNum;


    GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }


    // Start is called before the first frame update
    void Start()
    {
        //if (stalkerOff)
        //{
        //    player.GetComponent<stalker_prototype_script>().TurnOff();
        //}

        player.GetComponent<stalker_prototype_script>().setEventNum(stalkerNum);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
