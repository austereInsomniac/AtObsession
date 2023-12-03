using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trash_spawner : MonoBehaviour
{
    List<SpriteRenderer> trash;
    List<SpriteRenderer> trashOpen;
    game_state state;
    private int lastHour;

    void Start()
    {
        // grab all the trash
        GameObject[] trashItems = GameObject.FindGameObjectsWithTag("trash");
        trash = new List<SpriteRenderer>();

        for (int i = 0; i < trashItems.Length; i++)
        {
            trash.Add(trashItems[i].GetComponent<SpriteRenderer>());
        }

        // set available trash
        trashOpen = trash;

        state = GetComponent<game_state>();
        state.addOnTimeChange(addTrash);

        lastHour = 14;
    }

    private void addTrash(int timeO, int timeN) 
    {
        int currentHour = timeN / 60;
        if(currentHour > lastHour) 
        {
            for (int i = 0; i < currentHour % lastHour; i++)
                {
                    if(trashOpen.Count > 0) 
                    {
                        selectTrash().enabled = true;
                    }  
                }
        }
        lastHour = currentHour;
    }

    // select a random available trash
    private SpriteRenderer selectTrash()
    {
        int rand = (int)(Random.value * (trashOpen.Count - 1));
        Debug.Log(trashOpen.Count + " " + rand + " " + trashOpen[rand].name);
        SpriteRenderer temp = trashOpen[rand];
        trashOpen.Remove(trashOpen[rand]);
        return temp;

    }
}
