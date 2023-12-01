using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trash_spawner : MonoBehaviour
{
    List<UnityEngine.UI.Image> trash;
    List<UnityEngine.UI.Image> trashOpen;
    game_state state;
    private int lastHour;

    void Start()
    {
        // grab all the trash
        GameObject[] trashItems = GameObject.FindGameObjectsWithTag("trash");

        for (int i = 0; i < trashItems.Length; i++)
        {
            trash.Add(trashItems[i].GetComponent<UnityEngine.UI.Image>());
        }
        trashOpen = trash;

        state = GameObject.Find("Player").GetComponent<game_state>();
        state.addOnTimeChange(addTrash);

        lastHour = 14;
    }

    private void addTrash(int timeO, int timeN) 
    {
        int currentHour = timeN % 60;
        if(currentHour > lastHour) { 
            for(int i = 0; i <currentHour/ lastHour; i++)
                {
                    selectTrash().enabled = true;
                }
        }

    }

    // select a random available trash
    private UnityEngine.UI.Image selectTrash()
    {
        int rand = (int)(Random.value * trashOpen.Count);
        trashOpen.Remove(trashOpen[rand]);
        return trashOpen[rand];
    }
}
