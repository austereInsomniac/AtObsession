using System.Collections.Generic;
using UnityEngine;

public class trash_spawner : MonoBehaviour
{
    List<SpriteRenderer> trash;
    List<SpriteRenderer> trashHidden;
    List<SpriteRenderer> trashVisible;

    game_state state;
    private int lastHour;

    void Awake()
    {
        trash = new List<SpriteRenderer>();
        trashHidden = new List<SpriteRenderer>();
        trashVisible = new List<SpriteRenderer>();

        // grab all the trash
        GameObject[] trashItems = GameObject.FindGameObjectsWithTag("trash");
        for (int i = 0; i < trashItems.Length; i++)
        {
            trash.Add(trashItems[i].GetComponent<SpriteRenderer>());
            trashHidden.Add(trashItems[i].GetComponent<SpriteRenderer>());
        }

        state = GetComponent<game_state>();
        lastHour = 8;
    }

    public void addTrash(int timeO, int timeN) 
    {
        int currentHour = timeN / 60;
        if(currentHour > lastHour && lastHour != 0) 
        {
            for (int i = 0; i < currentHour % lastHour; i++)
                {
                    if(trashHidden.Count > 0) 
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
        int rand = (int)(Random.value * (trashHidden.Count - 1));
        SpriteRenderer temp = trashHidden[rand];

        trashVisible.Add(trashHidden[rand]);
        trashHidden.Remove(trashHidden[rand]);
        return temp;
    }

    public void cleanTrash(string room)
    {
        state.updateTime(0);
        for(int i = 0; i < trashVisible.Count; i++)
        {
            if (trashVisible[i].name.Equals(room))
            {
                trashVisible[i].enabled = false;
                trashHidden.Add(trashVisible[i]);
                trashVisible.Remove(trashVisible[i]);
            }
        }
    }

    public bool isDirty(string room)
    {
        for (int i = 0; i < trashVisible.Count; i++)
        {
            if (trashVisible[i].name.Equals(room))
            {
                return true;
            }
        }
        return false;
    }
}
