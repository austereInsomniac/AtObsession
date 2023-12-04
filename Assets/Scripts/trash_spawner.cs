using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trash_spawner : MonoBehaviour
{
    List<SpriteRenderer> trash;
    List<SpriteRenderer> trashOpen;
    List<SpriteRenderer> trashClose;

    game_state state;
    private int lastHour;

    void Awake()
    {
        // grab all the trash
        GameObject[] trashItems = GameObject.FindGameObjectsWithTag("trash");
        trash = new List<SpriteRenderer>();

        for (int i = 0; i < trashItems.Length; i++)
        {
            trash.Add(trashItems[i].GetComponent<SpriteRenderer>());
        }

        trashOpen = trash;
        trashClose = new List<SpriteRenderer>();

        state = GetComponent<game_state>();
        state.addOnTimeChange(addTrash);

        lastHour = 14;
    }

    private void addTrash(int timeO, int timeN) 
    {
        int currentHour = timeN / 60;
        if(currentHour > lastHour && lastHour != 0) 
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

        trashClose.Add(trashOpen[rand]);
        trashOpen.Remove(trashOpen[rand]);
        return temp;

    }

    public void cleanTrash(string room)
    {
        for(int i = 0; i < trash.Count; i++)
        {
            if (trash[i].name.Equals(room))
            {
                trashOpen.Add(trash[i]);
                trashClose.Remove(trash[i]);
                trash[i].enabled = false;
            }
        }
    }

    public bool isDirty(string room)
    {
        for (int i = 0; i < trashClose.Count; i++)
        {
            if (trashClose[i].name.Equals(room))
            {
                return true;
            }
        }

        return false;
    }
}
