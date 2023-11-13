using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Mackenzie 

public class statistic_slider : MonoBehaviour
{
    [SerializeField]
    bool wellness;

    private UnityEngine.UI.Slider slider;

    // Start is called before the first frame update
    public void Start()
    {
        slider = GetComponent<Slider>();
        game_state player = GameObject.Find("Player").GetComponent<game_state>();

        if(wellness)
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
        // move the image left or right
    }
}
