using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class day_night_manager : MonoBehaviour
{
    game_state state;

    // Start is called before the first frame update
    void Start()
    {
     state = GameObject.Find("Player").GetComponent<game_state>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
