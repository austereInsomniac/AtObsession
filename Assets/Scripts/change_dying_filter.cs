using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class change_dying_filter : MonoBehaviour
{
    private void Start()
    {
        // add the update method to the wellness delegate
        GameObject player = GameObject.Find("Player");
        player.GetComponent<game_state>().addOnWellnessChange(changeFilterOpacity);

        // run the filter when the game starts
        changeFilterOpacity(player.GetComponent<game_state>().getWellness(),
                            player.GetComponent<game_state>().getWellness());

    }

    // this method is called every time wellness is updated
    // the method updates the alpha of the filter color to be stronger with a lower wellness
    public void changeFilterOpacity(int oldW, int newW)
    {
            // ceate a color and set to transparent
            UnityEngine.Color color = this.GetComponent<SpriteRenderer>().color;
            color.a = 0;

            if (newW <= 30)
            {
                // make the color not transparent when wellness is low
                color.a = (float)(50 - (newW / 2)) / 100;
            }

            // chage color every update
            this.GetComponent<SpriteRenderer>().color = color;
        }
}
