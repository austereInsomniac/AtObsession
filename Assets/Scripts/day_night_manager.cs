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
        state.addOnTimeChange(updateFilter);

        updateFilter(state.getTime(), state.getTime());
    }

    private void updateFilter(int oldT, int newT)
    {
        UnityEngine.Color color = this.GetComponent<SpriteRenderer>().color;

        if (newT >= 19 * 60 || newT < 8 * 60)
        {
            
            color.a = 50f/255;
            Debug.Log(color.a);
        }
        else if(newT > 17 * 60)
        {
            color.a = (float)((newT - (17*60)) / 612f);
            Debug.Log(color.a);
        }
        else
        {
            color.a = 0;
        }

        this.GetComponent<SpriteRenderer>().color = color;
    }
}
