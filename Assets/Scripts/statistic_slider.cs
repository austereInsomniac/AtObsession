using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Mackenzie 

public class statistic_slider : MonoBehaviour
{
    private UnityEngine.UI.Slider slider;

    // Start is called before the first frame update
    public void Start()
    {
        // grab the slider
        slider = GetComponent<Slider>();
        game_state player = GameObject.Find("Player").GetComponent<game_state>();

        // wellness slider vs reputation slider
        // grab the correct stat
        if (name.Equals("Wellness Slider"))
        {
            player.addOnWellnessChange(updateSlider);
        }
        else
        {
            player.addOnReputationChange(updateSlider);
        }
    }

    public void updateSlider(int oldS, int newS)
    {
        // update the slider position
        slider.value = newS;
    }
}
