using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class day_night_manager : MonoBehaviour
{
    game_state state;
    GameObject[] dayObjects;
    GameObject[] nightObjects;

    // Start is called before the first frame update
    void Start()
    {
        state = GameObject.Find("Player").GetComponent<game_state>();
        state.addOnTimeChange(updateFilter);
        state.addOnTimeChange(updateBackground);

        dayObjects = GameObject.FindGameObjectsWithTag("dayTime");
        nightObjects = GameObject.FindGameObjectsWithTag("nightTime");

        updateFilter(state.getTime(), state.getTime());
        updateBackground(state.getTime(), state.getTime());
    }

    private void updateFilter(int oldT, int newT)
    {
        UnityEngine.Color color = this.GetComponent<SpriteRenderer>().color;

        if (newT >= 19 * 60 || newT < 8 * 60)
        {
            color.a = 50f/255;
        }
        else if(newT > 17 * 60)
        {
            color.a = (float)((newT - (17*60)) / 612f);
        }
        else
        {
            color.a = 0;
        }

        this.GetComponent<SpriteRenderer>().color = color;
    }

    private void updateBackground(int oldT, int newT)
    {
        if (newT > 17 * 60 || newT < 8 * 60)
        {
            foreach (GameObject obj in nightObjects)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in dayObjects)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject obj in nightObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in dayObjects)
            {
                obj.SetActive(true);
            }
        }
    }
}
