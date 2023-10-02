using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class update_text : MonoBehaviour
{
    // get text object
    [SerializeField]
    private TMP_Text text;

    // get player
    GameObject player;

    void Awake() {
        // Player in "" is the name of the Game Object with the game_state script
        player = GameObject.Find("Player");
    }

    void Update()
    {
        // update the text
        string updateText = "" + player.GetComponent<game_state>().getWellness();
        text.SetText(updateText);
    }

}
