using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_filter_color : MonoBehaviour
{
    private void Start()
    {
        // add the update method to the wellness delegate
        GameObject player = GameObject.Find("Player");
        player.GetComponent<game_state>().addOnWellnessChange(changeFilterOpacity);
        player.GetComponent<game_state>().addOnWellnessChange(changeGradientOpacity);

        // run the filter when the game starts
        changeFilterOpacity(player.GetComponent<game_state>().getWellness(), 
                            player.GetComponent<game_state>().getWellness());
        changeGradientOpacity(player.GetComponent<game_state>().getWellness(),
                              player.GetComponent<game_state>().getWellness());
    }

    // this method is called every time wellness is updated
    // the method updates the alpha of the filter color to be stronger with a lower wellness
    public void changeFilterOpacity(int oldW, int newW)
    {
        // the division by two represents how strong the maximum strength filter could be (2 = 50% oppacity)
        Color color = this.GetComponent<SpriteRenderer>().color;
        color.a = (float)(50 - (newW / 2))/100;
        this.GetComponent<SpriteRenderer>().color = color;
    }

    public void changeGradientOpacity(int oldW, int newW)
    {
        
    }
}
